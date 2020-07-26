using MovieRecommender.Core.Models.Entities;
using System.Collections;
using System.Collections.Generic;

namespace MovieRecommender.Core.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public int ImdbID { get; set; }
        public int TmdbID { get; set; }
        public string Name { get; set; }

        public ICollection<UserRate> UserRates { get; set; }

        public ICollection<UserRecomendationToMovie> UserRecomendations { get; set; }
    }
}