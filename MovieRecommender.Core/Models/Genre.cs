using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Models
{
    public class Genre
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
