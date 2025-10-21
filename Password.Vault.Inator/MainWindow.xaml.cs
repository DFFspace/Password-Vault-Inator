using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Password.Vault.Inator.Pages;
using System;
using System.Linq;
using Windows.UI.ApplicationSettings;
using WinRT.Interop;
using WinUIEx;

namespace Password.Vault.Inator;

public sealed partial class MainWindow : WindowEx
{
    private readonly AppWindow appWindow;

    public MainWindow()
    {
        InitializeComponent();
        appWindow = GetAppWindowForCurrentWindow();
        appWindow.Resize(new Windows.Graphics.SizeInt32(1200, 600));
        this.CenterOnScreen();
        ExtendsContentIntoTitleBar = true;
        ApplicationTitleBar.Subtitle = App.AppVersion;

        NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems.OfType<NavigationViewItem>().First();
        ContentFrame.Navigate(
                   typeof(HomePage),
                   null,
                   new Microsoft.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo()
                   );
    }

    private void NavigationViewControl_ItemInvoked(NavigationView sender,
          NavigationViewItemInvokedEventArgs args)
    {
        if (args.IsSettingsInvoked == true)
        {
            ContentFrame.Navigate(typeof(SettingsPage), null, args.RecommendedNavigationTransitionInfo);
        }
        else if (args.InvokedItemContainer != null && (args.InvokedItemContainer.Tag != null))
        {
            Type newPage = Type.GetType(args.InvokedItemContainer.Tag.ToString());
            ContentFrame.Navigate(
                   newPage,
                   null,
                   args.RecommendedNavigationTransitionInfo
                   );
        }
    }

    private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
    {
        NavigationViewControl.IsBackEnabled = ContentFrame.CanGoBack;

        if (ContentFrame.SourcePageType == typeof(SettingsPage))
        {
            NavigationViewControl.SelectedItem = NavigationViewControl.FooterMenuItems
                .OfType<NavigationViewItem>()
                .First(n => n.Tag.Equals(ContentFrame.SourcePageType.FullName.ToString()));
        }
        else if (ContentFrame.SourcePageType != null)
        {
            NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems
                .OfType<NavigationViewItem>()
                .First(n => n.Tag.Equals(ContentFrame.SourcePageType.FullName.ToString()));
        }

        NavigationViewControl.Header = ((NavigationViewItem)NavigationViewControl.SelectedItem)?.Content?.ToString();
    }

    private void TitleBar_BackRequested(Microsoft.UI.Xaml.Controls.TitleBar sender, object args)
    {
        if (ContentFrame.CanGoBack)
        {
            ContentFrame.GoBack();
        }
    }

    private AppWindow GetAppWindowForCurrentWindow()
    {
        IntPtr hWnd = WindowNative.GetWindowHandle(this);
        WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
        return AppWindow.GetFromWindowId(myWndId);
    }
}
