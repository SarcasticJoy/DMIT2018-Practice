using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PracticeforQuiz1.Startup))]
namespace PracticeforQuiz1
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
