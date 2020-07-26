using MovieRecommender.Core.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Models.Entities
{
    public class UserRecomendation
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public RecomendationTypes RateType { get; set; }

        public ICollection<UserRecomendationToMovie> UserRecomendationMovies { get; set; }
    }
}
