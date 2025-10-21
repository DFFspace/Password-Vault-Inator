using Microsoft.UI.Xaml.Controls;
using System;
using Windows.Security.Credentials;

namespace Password.Vault.Inator.Classes;

public sealed partial class SecretManager
{
    public static void AddPasswordToCredVault(string resource, string username, string password)
    {
        MainWindow.Instance.AddCredsToVaultInfoBar.IsOpen = true;
        MainWindow.Instance.AddCredsToVaultProgress.IsActive = true;

        try
        {
            var vault = new PasswordVault();
            vault.Add(new PasswordCredential(resource, username, password));
            
            MainWindow.Instance.AddCredsToVaultProgress.IsActive = false;
            MainWindow.Instance.AddCredsToVaultInfoBar.Severity = InfoBarSeverity.Success;
            MainWindow.Instance.AddCredsToVaultInfoBar.Message = $"{resource} has been added to the Windows Credential Manager!";
        }
        catch (Exception ex)
        {
            MainWindow.Instance.AddCredsToVaultProgress.IsActive = false;
            MainWindow.Instance.AddCredsToVaultInfoBar.Severity = InfoBarSeverity.Error;
            MainWindow.Instance.AddCredsToVaultInfoBar.Message = $"Error while adding {resource}: {ex.Message}";
            return;
        }
    }
}
