using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EmployeeManager.Desktop.Services;
using EmployeeManager.Shared;
using EmployeeManager.Shared.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeManager.Desktop.ViewModels
{
public partial class NewTaskViewModel : ObservableObject
    {
        private readonly IMessageService _messageService;
        [ObservableProperty]
        private TaskDto task;

        public NewTaskViewModel(TaskDto task, IMessageService messageService)
        {
            Task = task ?? new TaskDto();

            if (string.IsNullOrWhiteSpace(Task.State))
                Task.State = "To_do";
            _messageService = messageService;
        }
        [RelayCommand]
        private async System.Threading.Tasks.Task SaveAsync()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AuthService.Token);

            var json = JsonConvert.SerializeObject(Task);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"http://localhost:5269/api/tasks", content)  ;
            if (response.IsSuccessStatusCode)
            {

                _messageService.ShowMessage("✅ Tâche mise à jour avec succès !");
                CloseWindow();
            }
            else
            {
                _messageService.ShowMessage($"Erreur : {response.StatusCode}");
            }
        }
        private void CloseWindow()
        {
            // Fermer la fenêtre liée à ce ViewModel
            Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.DataContext == this)?
                .Close();
        }

    }
}
