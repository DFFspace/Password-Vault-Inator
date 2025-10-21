using Microsoft.UI.Xaml;
using System.Diagnostics;
using System.Reflection;

namespace Password.Vault.Inator
{
    public partial class App : Application
    {
        public static string AppVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
        private Window? _window;

        public App()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _window = new MainWindow();
            _window.Activate();
        }
    }
}
