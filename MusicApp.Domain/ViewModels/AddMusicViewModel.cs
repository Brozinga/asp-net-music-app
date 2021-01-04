using System.Text.Json.Serialization;
using Flunt.Notifications;
using Flunt.Validations;
using MusicApp.Domain.Interfaces.Models;

namespace MusicApp.Domain.ViewModels
{
    public class AddMusicViewModel : Notifiable, IViewModelBase
    {
        public string Name { get; set; }
        public string Artist { get; set; }
    
        [JsonIgnore]
        public object Identity { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Name, "Name", "O Nome é obrigatório")
                .IsNotNullOrEmpty(Artist, "Artist", "O Artista é obrigatório"));
        }
    }
}