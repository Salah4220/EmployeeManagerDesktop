using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EmployeeManager.Desktop.Services;
using EmployeeManager.Shared;
using EmployeeManager.Shared.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace EmployeeManager.Desktop.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {

        private readonly IMessageService _messageService;

        private readonly HttpClient _client;

        [ObservableProperty]
        private ObservableCollection<TaskDto> tasks;

        public DashboardViewModel(IMessageService messageService)
        {
            
            _messageService = messageService;
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:5269/");

            if (!string.IsNullOrEmpty(AuthService.Token))
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AuthService.Token);

            Tasks = new ObservableCollection<TaskDto>();
            LoadTasksCommand = new AsyncRelayCommand(LoadTasksAsync);
        }
        public IAsyncRelayCommand LoadTasksCommand { get; }

        private async System.Threading.Tasks.Task LoadTasksAsync()
        {
            try
            {
                var response = await _client.GetAsync("api/tasks");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var list = JsonConvert.DeserializeObject<List<TaskDto>>(json);

                    Tasks.Clear();
                    foreach (var t in list)
                        Tasks.Add(t);
                }
                else
                {
                  
                    _messageService.ShowMessage($"Erreur : {response.StatusCode}", "Erreur");
                    
                }
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage($"Erreur lors du chargement des tâches : {ex.Message}", "Erreur");
              
            }
        }
    }


}
