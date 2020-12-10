using System;
using System.Collections.Generic;

namespace MusicApp.Domain.Models
{
    public class MusicsToUsers
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public long MusicId { get; set; }
        public virtual Music Music { get; set; }

    }
}