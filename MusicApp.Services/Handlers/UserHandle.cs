using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MusicApp.Domain.Interfaces.Repositories;
using MusicApp.Domain.Models;
using MusicApp.Domain.ViewModels;
using MusicApp.Domain.ViewModels.Responses;
using MusicApp.Services.Interfaces.Requests;
using MusicApp.Services.Responses;
using MusicApp.Services.Tokens;

namespace MusicApp.Services.Handlers
{
    public class UserHandle : IHandleBase<UserCreateViewModel, BasicResponse<BasicObject>>, IHandleBase<LoginViewModel, BasicResponse<BasicObject>>
    {
        private readonly IUserRepository userRepository;
        private readonly ILoginRepository loginRepository;
        private BasicObject objResponse = null;
        private readonly IConfiguration _config;

        public UserHandle(IUserRepository userRepository, ILoginRepository loginRepository, IConfiguration config)
        {
            this.userRepository = userRepository;
            this.loginRepository = loginRepository;
            _config = config;
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

        public async Task<BasicResponse<BasicObject>> Execute(LoginViewModel viewModel)
        {
            viewModel.Validate();

            if (viewModel.Invalid)
            {
                objResponse = new BasicObject("Validation Erro", viewModel.Notifications);
                return new BasicResponse<BasicObject>(objResponse, 400, true);
            }

            var usuario = await loginRepository.FindUserByEmail(viewModel.Email);

            if (usuario == null)
            {
                objResponse = new BasicObject("Usuario não encontrado", "Usuario não encontrado");
                return new BasicResponse<BasicObject>(objResponse, 404, true);
            }

            var result = await loginRepository.Login(usuario, viewModel.Password);

            if (result.Succeeded)
            {
                var roles = await userRepository.GetRoleOfUser(usuario);
                var token = Jwt.GenerateToken(usuario, roles, _config.GetValue<string>("Secret"));


                objResponse = new BasicObject("Logado com sucesso!", new
                {
                    email = usuario.Email,
                    token
                });
                return new BasicResponse<BasicObject>(objResponse);
            }

            objResponse = new BasicObject("Ops Usuário ou senha não corresponde!", result);
            return new BasicResponse<BasicObject>(objResponse, 401, true);
        }
    }
}