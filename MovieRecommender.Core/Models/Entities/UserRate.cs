using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Models
{
    public class UserRate
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public double Rate { get; set; }
        public bool IsCreate { get; set; }

        public User User { get; set; }
        public Movie Movie { get; set; }

    }
}
