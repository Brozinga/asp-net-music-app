using System.Threading.Tasks;
using MusicApp.Domain.Interfaces.Repositories;
using MusicApp.Domain.Models;
using MusicApp.Domain.ViewModels;
using MusicApp.Domain.ViewModels.Responses;
using MusicApp.Services.Interfaces.Requests;
using MusicApp.Services.Responses;

namespace MusicApp.Services.Handlers
{
    public class UserHandle : IHandleBase<UserCreateViewModel, BasicResponse<BasicObject>>
    {
        private readonly IUserRepository userRepository;
        private BasicObject objResponse = new BasicObject();

        public UserHandle(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        public async Task<BasicResponse<BasicObject>> Execute(UserCreateViewModel viewModel)
        {
            if (viewModel.Invalid)
            {
                objResponse.Title = "Validation Erro";
                objResponse.Message = viewModel.Notifications;
                return new BasicResponse<BasicObject>(objResponse, 400, true);
            }

            var user = new User
            {
                UserName = viewModel.Name,
                Email = viewModel.Email
            };

            var result = await userRepository.Add(user, viewModel.Password);

            objResponse.Title = "Usuário Criado";
            objResponse.Message = result;

            return new BasicResponse<BasicObject>(objResponse);
        }
    }
}