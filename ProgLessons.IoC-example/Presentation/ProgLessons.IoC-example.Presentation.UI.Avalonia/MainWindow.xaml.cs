using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ProgLessons.IoC_example.Presentation.UI.Avalonia
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}