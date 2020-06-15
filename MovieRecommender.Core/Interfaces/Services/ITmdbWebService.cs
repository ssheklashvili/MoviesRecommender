using MovieRecommender.Core.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommender.Core.Interfaces.Services
{
    public interface ITmdbWebService
    {
        Task<List<MovieApiModel>> GetPopularMovies();
        Task<List<MovieApiModel>> SearchMovie(string name);
        Task<MovieApiModel> SearchMovieById(int id);
    }
}
