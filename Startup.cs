using Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using System.Web.Http;

namespace PrintHook
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}");
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);

            var defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(defaultFilesOptions);

            var staticFilesOptions = new StaticFileOptions()
            {
                FileSystem = new PhysicalFileSystem(@".\wwwroot\printhook\www"),
            };
            app.UseStaticFiles(staticFilesOptions);
        }
    }
}
