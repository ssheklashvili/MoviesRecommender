using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Models
{
    public class Artist
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
