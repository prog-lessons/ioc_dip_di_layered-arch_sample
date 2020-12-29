using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ProgLessons.IoC_example.App.Services.Core;

namespace ProgLessons.IoC_example.Presentation.UI.Avalonia
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
                AvaloniaManager.Window = desktop.MainWindow;
                var app = new AppManager(typeof(AvaloniaManager));
                app.Run();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }

}