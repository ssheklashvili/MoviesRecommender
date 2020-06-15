using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Models
{
    public class UserGenre
    {
        public int UserId { get; set; }
        public int GenreId { get; set; }

        public User User { get; set; }
        public Dictionary Genre { get; set; }
    }
}
