using System.Collections.Generic;

namespace MusicApp.Domain.ViewModels.Responses
{
    public class UserBasicResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public long MusicsTotal { get; set; }
        public IList<MusicManyResponse> Musics { get; set; }
    }
}