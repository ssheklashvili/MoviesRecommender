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
        void RateMovie(int userId, int movieId, float rate);
        void UpdateUserRate(int userId, int movieId, float rate);
        UserRate GetUserRate(int userId, int movieId);
    }
}
