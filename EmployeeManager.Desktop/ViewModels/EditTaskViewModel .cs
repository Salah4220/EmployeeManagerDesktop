using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EmployeeManager.Desktop.Services;
using EmployeeManager.Shared;
using EmployeeManager.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;

namespace EmployeeManager.Desktop.ViewModels
{
    public partial class EditTaskViewModel : ObservableObject
    {
        private readonly IMessageService _messageService;

        [ObservableProperty]
        private TaskDto task;

        public EditTaskViewModel(TaskDto task, IMessageService messageService)
        {
            Task = task;
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

            var response = await client.PutAsync($"http://localhost:5269/api/tasks/{Task.Id}", content);
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

        [RelayCommand]
        private async System.Threading.Tasks.Task DeleteAsync()
        {
                var result = MessageBox.Show(
                    "Êtes-vous sûr de vouloir supprimer cette tâche ?",
                    "Confirmation de suppression",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

            if (result != MessageBoxResult.Yes)
                return;

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AuthService.Token);

            var response = await client.DeleteAsync($"http://localhost:5269/api/tasks/{Task.Id}");
            if (response.IsSuccessStatusCode)
            {
                _messageService.ShowMessage("✅ Tâche supprimée avec succès !");
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
