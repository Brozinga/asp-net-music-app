using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Flunt.Notifications;
using MusicApp.Domain.Interfaces.Models;

namespace MusicApp.Domain.ViewModels
{
    public class LoginViewModel : Notifiable, IViewModelBase
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "O email é obrigatório!")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "O Formato não corresponde a um email")]
        public string Email { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "A senha é obrigatória!")]
        [MinLength(4, ErrorMessage = "O Tamanho mínimo é de {1} caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        public void Validate()
        {
          if (string.IsNullOrEmpty(Email))
                AddNotification("Username", "Usuário não pode ser vazio ou nulo");

          if (string.IsNullOrEmpty(Password))
                AddNotification("Password", "Senha não pode ser vazio ou nulo");
        }
    }
}