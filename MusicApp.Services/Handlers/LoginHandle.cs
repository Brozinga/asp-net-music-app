using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MusicApp.Domain.Interfaces.Repositories;
using MusicApp.Domain.ViewModels;
using MusicApp.Domain.HttpResponses;
using MusicApp.Services.Interfaces.Requests;
using MusicApp.Services.Responses;
using MusicApp.Services.Tokens;

namespace MusicApp.Services.Handlers
{
    public class LoginHandle : IHandleBase<LoginViewModel, BasicResponse<BasicObject>>
    {
        private BasicObject objResponse = null;
        private readonly IUserRepository userRepository;
        private readonly ILoginRepository loginRepository;
        private readonly IConfiguration _config;
        // Log da microsoft funciona também com o Graylog.
        // using Microsoft.Extensions.Logging;
        
        //E também posso usar o log do Serilog com o Graylog
        //using Serilog;
        //using Serilog.Extensions.Logging; 
        // Onde a Interface Logger funciona nas duas.
        private readonly ILogger logger;

        public LoginHandle(ILoginRepository loginRepository, IConfiguration config, IUserRepository userRepository, ILogger<LoginHandle> logger)
        {
            this.loginRepository = loginRepository;
            _config = config;
            this.userRepository = userRepository;
            this.logger = logger;
        }

        public async Task<BasicResponse<BasicObject>> Execute(LoginViewModel viewModel)
        {
            // Log da microsoft funciona também com o serilog.
            logger.LogDebug("Executando Handler de Login!");
            
            //Usando o Serilog.
            //logger.Debug("Executando Handler de Login!");
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