﻿using ExcelDataReader;
using MovieRecommender.Core.Interfaces.Repositories;
using MovieRecommender.Core.Interfaces.Services;
using MovieRecommender.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

        public IEnumerable<Movie> GetMovies(string name, int? page)
        {
            return _movieRepository.GetMovies(name, page);
        }

        public List<Movie> GetMoviesByName(string name)
        {
            return _movieRepository.GetMoviesByName(name);
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