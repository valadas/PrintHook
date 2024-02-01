using System.IO;
using System;
using System.Web.Script.Serialization;

namespace PrintHook.Services
{
    internal class SettingsService
    {
        private readonly string filePath;

        public SettingsService()
        {
            this.filePath = this.TryCreateDefaultSettings();
        }

        public void SaveSettings(Settings settings)
        {
            this.SaveSettingsInternal(settings, this.filePath);
        }

        public Settings GetSettings()
        {
            var serializer = new JavaScriptSerializer();
            string json = File.ReadAllText(this.filePath);
            return serializer.Deserialize<Settings>(json);
        }

        private string TryCreateDefaultSettings()
        {
            string commonDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string serviceDataPath = Path.Combine(commonDataPath, "PrintHook");

            if (!Directory.Exists(serviceDataPath))
            {
                Directory.CreateDirectory(serviceDataPath);
            }

            var filePath = Path.Combine(serviceDataPath, "settings.json");

            if (!File.Exists(filePath))
            {
                var defaultSettings = new Settings
                {
                    LabelFilePath = string.Empty,
                    Port = 9000,
                    PrinterName = string.Empty,
                };
                this.SaveSettingsInternal(defaultSettings, filePath);
            }

            return filePath;
        }

        private void SaveSettingsInternal(Settings settings, string filePath)
        {
            var serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(settings);

            File.WriteAllText(filePath, json);
        }
    }
}
