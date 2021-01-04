using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MusicApp.Domain.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            MusicsToUsers = new List<MusicsToUsers>();
        }
        public virtual IList<MusicsToUsers> MusicsToUsers { get; set; }
    }
}