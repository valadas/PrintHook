namespace PrintHook.Services
{
    public class Settings
    {
        /// <summary>
        /// Gets or sets the label file path.
        /// </summary>
        public string LabelFilePath { get; set; }

        /// <summary>
        /// Gets or sets the name of the printer.
        /// </summary>
        public string PrinterName { get; set; }

        /// <summary>
        /// Gets or sets the port on which the webservice will run on.
        /// </summary>
        public int Port { get; set; }
    }
}