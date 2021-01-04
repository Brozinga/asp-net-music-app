using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MusicApp.Domain.Models;
using MusicApp.Domain.ViewModels;
using MusicApp.Domain.ViewModels.Responses;

namespace MusicApp.Api.Mapper
{
    public class AutoMapperConfig
    {
        public static void Configuration(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<User, UserBasicResponse>()
                    .ForMember(entity => entity.Musics, dto => dto.MapFrom(x => x.MusicsToUsers))
                    .ReverseMap();
               
                config.CreateMap<Music, MusicResponse>().ReverseMap();
                config.CreateMap<Music, AddMusicViewModel>().ReverseMap();

                config.CreateMap<MusicsToUsers, MusicManyResponse>().ReverseMap()
                    .ForMember(entity => entity.Music, dto => dto.MapFrom(x => x.Music));
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}