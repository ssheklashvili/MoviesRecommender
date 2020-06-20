using MovieRecommender.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Interfaces.Services
{
    public interface IMoviesService
    {
        public List<Movie> GetRandomMovies();
        List<Movie> GetMoviesByName(string name);
        void RateMovie(int userId, int movieId, float rate);
    }
}
