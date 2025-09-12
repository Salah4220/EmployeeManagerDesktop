using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EmployeeManager.Desktop.Models;
using EmployeeManager.Shared.Interfaces;
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

        private readonly IMessageService _messageService;



        public LoginViewModel(IMessageService messageService)
        {
            Username = string.Empty;
            Password = string.Empty;
            _messageService = messageService;
        }

        [RelayCommand]
        private async Task LoginAsync()
        {
            

            using var client = new HttpClient();
            var data = new { Username, Password }; 
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
             
                var response = await client.PostAsync("http://localhost:5269/api/users/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var resultJson = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<LoginResult>(resultJson);

                    if (result != null && result.Success)
                        _messageService.ShowMessage("Connexion réussie ✅", "Succès");
                
                    else
                        _messageService.ShowMessage(result?.Message ?? "Identifiants incorrects", "Erreur");

                }
                else
                {
                    _messageService.ShowMessage($"Erreur serveur : {response.StatusCode}", "Erreur");
                   
                }
            }
            catch (HttpRequestException ex)
            {
                _messageService.ShowMessage($"Erreur serveur : {ex.Message}", "Erreur");
            }
            finally
            {
              
            }
        }
    }
}