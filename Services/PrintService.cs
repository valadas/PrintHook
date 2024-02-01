using DymoSDK.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrintHook.Services
{
    internal class PrintService
    {
        public PrintService()
        {
            DymoSDK.App.Init();
        }

        public void PrintLabel(Settings settings, Dictionary<string, string> data)
        {
            var printer = DymoPrinter.Instance.GetPrinters().Result.Single(p => p.Name == settings.PrinterName);
            if (printer != null)
            {
                var dymoLabel = DymoLabel.Instance;
                dymoLabel.LoadLabelFromFilePath(settings.LabelFilePath);

                var objects = dymoLabel.GetLabelObjects().ToList();
                foreach (var obj in objects)
                {
                    if (data.TryGetValue(obj.Name, out var value))
                    {
                        dymoLabel.UpdateLabelObject(obj, value);
                    }
                }

                DymoPrinter.Instance.PrintLabel(dymoLabel, printer.Name);
            }
            Console.WriteLine("Working");
        }

        internal IReadOnlyCollection<string> GetPrinters()
        {
            var printers = DymoPrinter.Instance.GetPrinters();
            return printers.Result.Select(p => p.Name).ToList();
        }
    }
}
