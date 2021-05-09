using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LMVirtualGallery.WebMVC.Startup))]
namespace LMVirtualGallery.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
