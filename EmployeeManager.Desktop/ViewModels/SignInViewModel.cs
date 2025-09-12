using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EmployeeManager.Desktop.Models;
using EmployeeManager.Shared.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Desktop.ViewModels
{
    public partial class SignInViewModel : ObservableObject
   
    {
        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string confirmPassword;

        [ObservableProperty]
        private string role;

        private readonly IMessageService _messageService;

        public SignInViewModel(IMessageService messageService)
        {
            Username = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            role =string.Empty;
            _messageService = messageService;
          

        }

        [RelayCommand]
        private async Task SignInAsync()
        {
            if (Password != ConfirmPassword)
            {
                _messageService.ShowMessage("❌ Les mots de passe ne correspondent pas.", "Erreur");
                return;
            }
            using var client = new HttpClient();
            var data = new { Username, Password,Role };
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync("http://localhost:5269/api/users/register", content);
                if (response.IsSuccessStatusCode)
                {
                    var resultJson = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<SignInResult>(resultJson);
                    if (result != null && result.Success)
                    {
                        _messageService.ShowMessage("✅ Inscription réussie", "Succès");
                    }
                    else
                    {
                        _messageService.ShowMessage(result?.Message ?? "Identifiants incorrects", "Erreur");
                        
                    }
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();

                    _messageService.ShowMessage(errorContent, "Erreur");

                   
                }
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage($"Erreur serveur : {ex.Message}", "Erreur");
            }
        }
    }
}
