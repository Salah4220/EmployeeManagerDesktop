using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EmployeeManager.Desktop.Models;
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
        private string statusMessage;


        public SignInViewModel()
        {
            Username = string.Empty;
            Password = string.Empty;
            StatusMessage = string.Empty;

        }

        [RelayCommand]
        private async Task SignInAsync()
        {
            using var client = new HttpClient();
            var data = new { Username, Password };
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
                        StatusMessage ="Inscription réussie ✅";
                    }
                    else
                    {
                        StatusMessage = result?.Message ?? "Identifiants incorrects";
                    }
                }
                else
                {
                    StatusMessage = $"Erreur serveur : {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Erreur serveur : {ex.Message}";
            }
        }
    }
}
