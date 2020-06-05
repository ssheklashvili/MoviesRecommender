using System.Collections;
using System.Collections.Generic;

namespace MovieRecommender.Core.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public ICollection<Genre> Genres { get; set; }
        public ICollection<Artist> Artists { get; set; }
        public Director Director { get; set; }
    }
}