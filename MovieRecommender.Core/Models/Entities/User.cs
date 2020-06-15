using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<UserGenre> UserGenres { get; set; }
        public ICollection<UserArtist> UserArtists { get; set; }
        public ICollection<UserDirector> UserDirectors { get; set; }
        public ICollection<UserRate> UserRates { get; set; }
    }
}
