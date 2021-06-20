using System.Collections.Generic;
using Newtonsoft.Json;

namespace music_app.Models.ViewModels
{
    public class MusicsResponseViewModel
    {
        [JsonProperty("response")]
        public MusicsListResponse Response { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("error")]
        public bool Error { get; set; }
    }

    public class MusicsListResponse
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("message")]
        public MusicInformationViewModel Message { get; set; }
    }

    public class MusicInformationViewModel
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("musicsTotal")]
        public int MusicsTotal { get; set; }

        [JsonProperty("musics")]
        public IList<MusicViewModel> Musics { get; set; }
    }
}
