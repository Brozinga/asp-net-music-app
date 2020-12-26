using System.Threading.Tasks;
using MusicApp.Domain.Interfaces.Repositories;
using MusicApp.Domain.ViewModels;
using MusicApp.Domain.ViewModels.Responses;
using MusicApp.Infrastructure.Contexts;
using MusicApp.Services.Interfaces.Requests;
using MusicApp.Services.Responses;

namespace MusicApp.Services.Handlers
{
    public class LoginHandle : IHandleBase<LoginViewModel, BasicResponse<BasicObject>>
    {
        private readonly SqliteContext _context;
        private readonly IUserRepository _userRepository;
        private readonly ILoginRepository _loginRepository;
        private BasicObject _response = null;

        public LoginHandle(IUserRepository userRepository, SqliteContext context, ILoginRepository loginRepository)
        {
            _userRepository = userRepository;
            _context = context;
            _loginRepository = loginRepository;
        }

        public async Task<BasicResponse<BasicObject>> Execute(LoginViewModel viewModel)
        {
            viewModel.Validate();

            if (viewModel.Invalid)
            {
                _response = new BasicObject("Validation Erro", viewModel.Notifications);
                return new BasicResponse<BasicObject>(_response, 404, true);
            }

            return new BasicResponse<BasicObject>(_response);
        }
    }
}