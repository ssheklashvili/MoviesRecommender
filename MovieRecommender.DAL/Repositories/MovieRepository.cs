using MovieRecommender.Core.Models;
using MovieRecommender.Infrastructure.Contexts;
using MovieRecommender.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MovieRecommender.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly FakeContext _fakeContext;

        public MovieRepository()
        {
            this._fakeContext = new FakeContext();
        }

        public List<Movie> GetMovies()
        {
            return _fakeContext.Movies;
        }
    }
}
