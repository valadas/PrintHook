using PrintHook.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Threading.Tasks;

namespace PrintHook.Controllers
{
    public class PrintController : ApiController
    {
        private readonly PrintService printService;
        private readonly SettingsService settingsService;

        public PrintController()
        {
            this.printService = new PrintService();
            this.settingsService = new SettingsService();
        }

        [HttpPost]
        [Route("Print")]
        public async Task<IHttpActionResult> Print()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            var data = new Dictionary<string, string>();
            foreach (var content in provider.Contents)
            {
                var key = content.Headers.ContentDisposition.Name.Trim('"');
                var value = await content.ReadAsStringAsync();
                data[key] = value;
            }

            var settings = this.settingsService.GetSettings();

            this.printService.PrintLabel(settings, data);

            return this.Ok();
        }
    }
}
