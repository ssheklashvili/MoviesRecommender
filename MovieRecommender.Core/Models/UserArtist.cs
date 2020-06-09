using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Models
{
    public class UserArtist
    {
        public int UserId  { get; set; }
        public int ArtistId { get; set; }

        public User User { get; set; }
        public Dictionary Artist { get; set; }
    }
}
