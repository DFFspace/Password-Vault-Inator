using Microsoft.UI.Xaml.Controls;
using Password.Vault.Inator.Pages;
using System;
using Windows.Security.Credentials;

namespace Password.Vault.Inator.Classes;

public sealed partial class SecretManager
{
    public static void AddPasswordToCredVault(string resource, string username, string password)
    {
        HomePage.Instance.AddCredsToVaultInfoBar.IsOpen = true;
        HomePage.Instance.AddCredsToVaultProgress.IsActive = true;

        try
        {
            var vault = new PasswordVault();
            vault.Add(new PasswordCredential(resource, username, password));

            HomePage.Instance.AddCredsToVaultProgress.IsActive = false;
            HomePage.Instance.AddCredsToVaultInfoBar.Severity = InfoBarSeverity.Success;
            HomePage.Instance.AddCredsToVaultInfoBar.Message = $"{resource} has been added to the Windows Credential Manager!";
        }
        catch (Exception ex)
        {
            HomePage.Instance.AddCredsToVaultProgress.IsActive = false;
            HomePage.Instance.AddCredsToVaultInfoBar.Severity = InfoBarSeverity.Error;
            HomePage.Instance.AddCredsToVaultInfoBar.Message = $"Error while adding {resource}: {ex.Message}";
            return;
        }
    }
}
