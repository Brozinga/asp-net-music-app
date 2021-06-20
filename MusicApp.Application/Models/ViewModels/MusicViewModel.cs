using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace music_app.Models.ViewModels
{
    public class MusicViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")] 
        public string Name { get; set; }

        [JsonProperty("artist")]
        public string Artist { get; set; }
    }
}
