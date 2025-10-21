using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Password.Vault.Inator.Classes;
using System;
using WinRT.Interop;
using WinUIEx;

namespace Password.Vault.Inator
{
    public sealed partial class MainWindow : WindowEx
    {
        private readonly AppWindow appWindow;
        public static MainWindow Instance { get; private set; }

        public MainWindow()
        {
            Instance = this;
            InitializeComponent();
            appWindow = GetAppWindowForCurrentWindow();
            appWindow.Resize(new Windows.Graphics.SizeInt32(1200, 600));
            this.CenterOnScreen();
            ExtendsContentIntoTitleBar = true;
            ApplicationTitleBar.Subtitle = App.AppVersion;
        }

        private AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(myWndId);
        }

        private void BtnAddCredsToVault_Click(object sender, RoutedEventArgs e)
        {
            SecretManager.AddPasswordToCredVault(
                TextBoxResourceName.Text,
                TextBoxUsername.Text,
                TextBoxPassword.Password
            );
        }
    }
}
