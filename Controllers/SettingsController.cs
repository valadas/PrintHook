using PrintHook.Services;
using System.Web.Http;

namespace PrintHook.Controllers
{
    public class SettingsController : ApiController
    {
        private SettingsService settingsService;
        private PrintService printService;

        public SettingsController()
        {
            this.settingsService = new SettingsService();
            this.printService = new PrintService();
        }

        [HttpGet]
        [Route("GetSettings")]
        public IHttpActionResult GetSettings()
        {
            var settings = this.settingsService.GetSettings();
            return this.Ok(settings);
        }

        [HttpGet]
        [Route("GetPrinters")]
        public IHttpActionResult GetPrinters()
        {
            var printers = this.printService.GetPrinters();
            return this.Ok(printers);
        }

        [HttpPost]
        [Route("SaveSettings")]
        public IHttpActionResult SaveSettings(Settings settings)
        {
            this.settingsService.SaveSettings(settings);
            return this.Ok();
        }
    }
}
