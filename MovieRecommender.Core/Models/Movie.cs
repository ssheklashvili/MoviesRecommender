using System.Collections;
using System.Collections.Generic;

namespace MovieRecommender.Core.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public int TmdbID { get; set; }

        public ICollection<UserRate> UserRates { get; set; }
    }
}