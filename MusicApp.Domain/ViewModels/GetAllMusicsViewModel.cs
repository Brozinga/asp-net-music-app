using Flunt.Notifications;
using MusicApp.Domain.Interfaces.Models;

namespace MusicApp.Domain.ViewModels
{
    public class GetAllMusicsViewModel : Notifiable, IViewModelBase
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public object Identify { get; set; }

        public void Validate()
        {
           //AddNotifications(new Contract()
           //    .
           //);
        }
    }
}