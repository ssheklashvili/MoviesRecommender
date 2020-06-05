using MovieRecommender.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Infrastructure.Contexts
{
    public class FakeContext
    {
        public List<Movie> Movies { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Director> Directors { get; set; }
        public List<Artist> Artists { get; set; }
        public FakeContext()
        {
            this.Genres = new List<Genre>
            {
                new Genre { ID = 1, Name = "genre1"},
                new Genre { ID = 2, Name = "genre2"}
            };

            this.Directors = new List<Director>()
            {
                new Director { ID = 1, FirstName = "FirstName1", LastName = "LastName1"},
                new Director { ID = 2, FirstName = "FirstName2", LastName = "LastNam2"}
            };

            this.Artists = new List<Artist>()
            {
                new Artist { ID = 1, FirstName = "FirstName1", LastName = "LastName1"},
                new Artist { ID = 2, FirstName = "FirstName2", LastName = "LastNam2"}
            };

            this.Movies = new List<Movie>()
            {
                new Movie { ID = 1, Title = "Title1", Artists = this.Artists, Director = this.Directors[0], Genres = this.Genres },
                new Movie { ID = 2, Title = "Title2", Artists = this.Artists, Director = this.Directors[1], Genres = this.Genres }
            };
        }
    }
}
