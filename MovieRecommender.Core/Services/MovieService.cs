using ExcelDataReader;
using MovieRecommender.Core.Interfaces.Repositories;
using MovieRecommender.Core.Interfaces.Services;
using MovieRecommender.Core.Models;
using MovieRecommender.Core.Models.AppModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MovieRecommender.Core.Services
{
    public class MovieService : IMoviesService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public List<Movie> GetRandomMovies()
        {
            return _movieRepository.GetRandomMovies();
        }

        public IEnumerable<Movie> GetMovies(string name, int? page, int? userId)
        {
            return _movieRepository.GetMovies(name, page, userId);
        }

        public List<Movie> GetMoviesByName(string name)
        {
            return _movieRepository.GetMoviesByName(name);
        } 

        public int[] GetOrderedMovieIds()
        {
            return _movieRepository.GetOrderedMovieIds();
        }

        public IEnumerable<Movie> GetMoviesByIds(List<int> MovieIds)
        {
            return _movieRepository.GetMoviesByIds(MovieIds);
        }
        public void RateMovie(int userId, int movieId, float rate)
        {
            var moviefromDb = _movieRepository.GetUserRate(userId, movieId);
            if(moviefromDb != null)
            {
                _movieRepository.UpdateUserRate(userId, movieId, rate);
            }
            else
            {
                _movieRepository.RateMovie(userId, movieId, rate);
            }
        }
    }
}