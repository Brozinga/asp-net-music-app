using System.ComponentModel.DataAnnotations;
using Flunt.Notifications;
using MusicApp.Domain.Interfaces.Models;

namespace MusicApp.Domain.ViewModels
{
    public class UserCreateViewModel : Notifiable ,IViewModelBase
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "O email é obrigatório!")]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "O Formato não corresponde a um email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O nome é obrigatório!")]
        [Display(Name = "Nome")]
        [MinLength(4, ErrorMessage = "O Tamanho mínimo para o nome é de {1} caracteres")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A senha é obrigatória!")]
        [Display(Name = "Password")]
        [MinLength(4, ErrorMessage = "O Tamanho mínimo para a senha é de {1} caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A confirmação de senha é obrigatória!")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "A senha e a confirmação de senha são diferentes!")]
        public string ConfirmPassword { get; set; }

        public void Validate()
        {
            if (Password != ConfirmPassword)
                AddNotification("ConfirmPassword", "A senha e a confirmação precisam ser iguais!");
        }
    }
}