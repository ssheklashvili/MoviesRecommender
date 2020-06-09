using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Models
{
    public class Dictionary
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsActor { get; set; }
        public bool IsDirector { get; set; }
        public bool IsGenre { get; set; }

        public ICollection<UserArtist> UserArtists { get; set; }
        public ICollection<UserGenre> UserGenres { get; set; }
        public ICollection<UserDirector> UserDirectors { get; set; }


    }
}
