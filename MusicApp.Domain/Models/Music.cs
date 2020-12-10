using System.Collections.Generic;

namespace MusicApp.Domain.Models
{
    public class Music
    {
        public long Id { get; set; }
        public string Name
        {
            get => Name;
            set => Name = value.ToUpper();
        }
        public string Artist {
            get => Artist;
            set => Artist = value.ToUpper();
        }
        public virtual IList<MusicsToUsers> MusicsToUsers { get; set; }
    }
}
