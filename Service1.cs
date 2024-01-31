using DymoSDK.Implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace PrintHook
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            DymoSDK.App.Init();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }

        internal void OnDebug()
        {
            var printers = DymoPrinter.Instance.GetPrinters().Result;
            var printer = printers.FirstOrDefault();
            if (printer != null)
            {
                var dymoLabel = DymoLabel.Instance;

                dymoLabel.LoadLabelFromFilePath("D:\\Users\\Daniel Valadas\\Downloads\\test-label.dymo");

                var objects = dymoLabel.GetLabelObjects().ToList();
                foreach ( var obj in objects )
                {
                    dymoLabel.UpdateLabelObject(obj, printer.Name);
                }

                DymoPrinter.Instance.PrintLabel(dymoLabel, printer.Name);
            }
            Console.WriteLine("Working");
        }
    }
}
