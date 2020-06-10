using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommender.Core.Services.Interfaces
{
    public interface ITmdbWebService
    {
        Task<string> GetPopularMovies();
        Task<string> SearchMovie(string name);
    }
}
