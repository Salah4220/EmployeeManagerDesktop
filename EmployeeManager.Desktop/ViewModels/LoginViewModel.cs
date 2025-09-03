using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EmployeeManager.Desktop.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Desktop.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private string statusMessage;

        public LoginViewModel()
        {
            Username = string.Empty;
            Password = string.Empty;
            StatusMessage = string.Empty;
        }

        [RelayCommand]
        private async Task LoginAsync()
        {
            IsLoading = true;
            StatusMessage = "Connexion en cours...";

            using var client = new HttpClient();
            var data = new { Username, Password }; // ⚠️ utiliser les propriétés, pas les champs privés
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                // Exemple : remplacer par l’URL réelle de ton API
                var response = await client.PostAsync("https://exemple.com/api/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var resultJson = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<LoginResult>(resultJson);

                    if (result != null && result.Success)
                        StatusMessage = "Connexion réussie ✅";
                    else
                        StatusMessage = result?.Message ?? "Identifiants incorrects";
                }
                else
                {
                    StatusMessage = $"Erreur serveur : {response.StatusCode}";
                }
            }
            catch (HttpRequestException ex)
            {
                StatusMessage = $"Erreur réseau : {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}