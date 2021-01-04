using System.Collections.Generic;
using System.Text.Json.Serialization;
using Flunt.Notifications;
using Flunt.Validations;
using MusicApp.Domain.Interfaces.Models;

namespace MusicApp.Domain.ViewModels
{
    public class AddRangeMusicViewModel : Notifiable, IViewModelBase
    {
        public AddRangeMusicViewModel()
        {
                Musics = new List<AddMusicViewModel>();
        }

        public IList<AddMusicViewModel> Musics { get; set; }

        [JsonIgnore]
        public object Identity { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNull(Musics, "Musics", "As musicas são obrigatórias")
                .IsFalse(Musics.Count <= 0, "Musics", "É necessário ao menos uma Musica" )
            );
        }
    }
}