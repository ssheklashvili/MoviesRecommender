using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Models.Entities
{
    public class UserRecomendationToMovie
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public int UserRecomendationId { get; set; }

        public Movie Movie { get; set; }

        public UserRecomendation UserRecomendation { get; set; }
    }
}
