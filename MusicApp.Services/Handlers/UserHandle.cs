using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MusicApp.Domain.HttpResponses;
using MusicApp.Domain.Interfaces.Repositories;
using MusicApp.Domain.Models;
using MusicApp.Domain.ViewModels;
using MusicApp.Services.Interfaces.Requests;
using MusicApp.Services.Responses;

namespace MusicApp.Services.Handlers
{
    public class UserHandle : IHandleBase<UserCreateViewModel, BasicResponse<BasicObject>>
    {
        private BasicObject objResponse = null;
        private readonly IUserRepository userRepository;

        public UserHandle(IUserRepository userRepository, ILoginRepository loginRepository, IConfiguration config)
        {
            this.userRepository = userRepository;
        }


        public async Task<BasicResponse<BasicObject>> Execute(UserCreateViewModel viewModel)
        {
            if (viewModel.Invalid)
            {
                objResponse = new BasicObject("Validation Erro", viewModel.Notifications);
                return new BasicResponse<BasicObject>(objResponse, 400, true);
            }

            var user = new User
            {
                UserName = viewModel.Name,
                Email = viewModel.Email
            };

            var result = await userRepository.Add(user, viewModel.Password);
            objResponse = new BasicObject("Usuário Criado", result);

            return new BasicResponse<BasicObject>(objResponse);
        }

    }
}