using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Password.Vault.Inator.Classes;

namespace Password.Vault.Inator.Pages;

public sealed partial class HomePage : Page
{
    public static HomePage Instance { get; private set; }

    public HomePage()
    {
        Instance = this;
        InitializeComponent();
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
