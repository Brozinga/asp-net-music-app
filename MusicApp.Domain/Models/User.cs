using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MusicApp.Domain.Models
{
    public class User : IdentityUser
    {
        public virtual IList<MusicsToUsers> MusicsToUsers { get; set; }
    }
}