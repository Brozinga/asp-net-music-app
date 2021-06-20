using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using MusicApp.Domain.HttpResponses;
using MusicApp.Domain.Interfaces.Repositories;
using MusicApp.Domain.Models;
using MusicApp.Domain.ViewModels;
using MusicApp.Domain.ViewModels.Responses;
using MusicApp.Infrastructure.Contexts;
using MusicApp.Services.Interfaces.Requests;
using MusicApp.Services.Responses;

namespace MusicApp.Services.Handlers
{
    public class MusicHandle : IHandleBase<AddMusicViewModel, BasicResponse<BasicObject>>,
        IHandleBase<AddRangeMusicViewModel, BasicResponse<BasicObject>>,
        IHandleBase<GetAllMusicsViewModel, BasicResponse<BasicObject>>
    {
        private BasicObject _objResponse = null;
        private readonly IMusicRepository _musicRepository;
        private readonly IUserRepository _userRepository;
        private readonly SqliteContext _sqliteContext;
        private readonly IMapper _mapper;

        public MusicHandle(IMusicRepository musicRepository, SqliteContext sqliteContext, IUserRepository userRepository, IMapper mapper)
        {
            _musicRepository = musicRepository;
            _userRepository = userRepository;
            this._mapper = mapper;
            this._sqliteContext = sqliteContext;
        }

        public async Task<BasicResponse<BasicObject>> Execute(AddMusicViewModel viewModel)
        {
            viewModel.Validate();

            try
            {
                if (viewModel.Invalid)
                {
                    _objResponse = new BasicObject("Ops! Dados enviados são incorretos", viewModel.Notifications);
                    return new BasicResponse<BasicObject>(_objResponse, 404);
                }

                var musica = new Music()
                {
                    Name = viewModel.Name,
                    Artist = viewModel.Artist,
                };

                var identity = (ClaimsIdentity) viewModel.Identity;
                IEnumerable<Claim> claim = identity.Claims;

                var userEmail = claim
                    .FirstOrDefault(x => x.Type == ClaimTypes.Email);


                var user = await _userRepository.FindUserByEmail(userEmail?.Value);
                
                musica.MusicsToUsers.Add(new MusicsToUsers
                {
                    User = user
                });

                await _musicRepository.Add(musica);


                var IsSaved = await _sqliteContext.SaveChangesAsync();

                if (IsSaved > 0)
                {
                    _objResponse = new BasicObject("Musica Adicionada", null);
                    return new BasicResponse<BasicObject>(_objResponse, 301);
                }

                _objResponse = new BasicObject("Ops Ocorreu um erro ao tentar adicionar a musica", null);
                return new BasicResponse<BasicObject>(_objResponse, 500);
            }
            catch (Exception e)
            {
                _objResponse = new BasicObject("Erro interno", e.Message);
                return new BasicResponse<BasicObject>(_objResponse, 500);
            }
        }

        public async Task<BasicResponse<BasicObject>> Execute(AddRangeMusicViewModel viewModel)
        {
            viewModel.Validate();

            try
            {
                if (viewModel.Invalid)
                {
                    _objResponse = new BasicObject("Ops! Dados enviados são incorretos", viewModel.Notifications);
                    return new BasicResponse<BasicObject>(_objResponse, 404);
                }


                var musicas = _mapper.Map<IList<Music>>(viewModel.Musics);

                var identity = (ClaimsIdentity)viewModel.Identity;
                IEnumerable<Claim> claim = identity.Claims;

                var userEmail = claim
                    .FirstOrDefault(x => x.Type == ClaimTypes.Email);

                var user = await _userRepository.FindUserByEmail(userEmail?.Value);


                foreach (var musica in musicas)
                {
                    musica.MusicsToUsers.Add(new MusicsToUsers
                    {
                        User = user
                    });
                }

                _musicRepository.AddRange(musicas);


                var IsSaved = await _sqliteContext.SaveChangesAsync();

                if (IsSaved > 0)
                {
                    _objResponse = new BasicObject("Musicas Adicionadas", null);
                    return new BasicResponse<BasicObject>(_objResponse, 301);
                }

                _objResponse = new BasicObject("Ops Ocorreu um erro ao tentar adicionar a musica", null);
                return new BasicResponse<BasicObject>(_objResponse, 500);
            }
            catch (Exception e)
            {
                _objResponse = new BasicObject("Erro interno", e.Message);
                return new BasicResponse<BasicObject>(_objResponse, 500);
            }
        }

        public async Task<BasicResponse<BasicObject>> Execute(GetAllMusicsViewModel viewModel)
        {
            try
            {
                var identity = (ClaimsIdentity) viewModel.Identify;

                IEnumerable<Claim> claim = identity.Claims;

                var UserId = claim
                    .FirstOrDefault(x => x.Type == ClaimTypes.Sid);

                var musics = await _userRepository.GetAllMusicsWhereUser(UserId?.Value, viewModel.Skip, viewModel.Take);

                var response = _mapper.Map<UserBasicResponse>(musics);

                response.MusicsTotal = musics.MusicsToUsers.Count;
                response.Musics = response.Musics.Skip(viewModel.Skip).Take(viewModel.Take).ToList();


                _objResponse = new BasicObject("Lista de musicas", response);

                return new BasicResponse<BasicObject>(_objResponse);
            }
            catch (Exception e)
            {
                _objResponse = new BasicObject("Erro interno", e.Message);
                return new BasicResponse<BasicObject>(_objResponse, 500);
            }

        }
    }
}