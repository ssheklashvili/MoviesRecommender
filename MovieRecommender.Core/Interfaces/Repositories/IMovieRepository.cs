using MovieRecommender.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Interfaces.Repositories
{
    public interface IMovieRepository
    {
        void ImportMovies();
        List<Movie> GetRandomMovies();
        List<Movie> GetMoviesByName(string name);
    }
}
