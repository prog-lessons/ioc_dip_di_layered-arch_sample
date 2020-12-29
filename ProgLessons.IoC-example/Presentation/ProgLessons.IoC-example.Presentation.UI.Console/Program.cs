using ProgLessons.IoC_example.App.Services.Core;

namespace ProgLessons.IoC_example.Presentation.UI.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new AppManager(typeof(ConsoleManager));
            app.Run();
        }
    }
}