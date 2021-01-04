using System.Collections.Generic;
using System.Net.Mime;

namespace MusicApp.Domain.Models
{
    public class Music
    {
        public Music()
        {
            MusicsToUsers = new List<MusicsToUsers>();
        }

        public long Id { get; set; }
        private string _Name;

        public string Name
        {
            get => _Name;
            set => _Name = value.ToUpper().Trim();
        }

        private string _Artitst;

        public string Artist {
            get => _Artitst;
            set => _Artitst = value.ToUpper().Trim();
        }
        public virtual IList<MusicsToUsers> MusicsToUsers { get; set; }
    }
}
