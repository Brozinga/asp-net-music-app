using System.Threading.Tasks;
using MusicApp.Domain.Interfaces.Models;
using MusicApp.Services.Interfaces.Responses;

namespace MusicApp.Services.Interfaces.Requests
{
    internal interface IHandleBase<in TViewModel, TResponse> where TViewModel : IViewModelBase
                                                                 where TResponse : IHandleResponse
    {
        Task<TResponse> Execute(TViewModel viewModel);
    }
}