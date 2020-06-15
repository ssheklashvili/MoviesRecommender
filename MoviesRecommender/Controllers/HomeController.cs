using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieRecommender.Core.Interfaces.Repositories;
using MovieRecommender.Core.Interfaces.Services;
using MovieRecommender.Core.Models.ApiModels;
using MovieRecommender.Core.Models.AppModels;
using MovieRecommender.Core.Services;
using MovieRecommender.Infrastructure.Repositories;
using MoviesRecommender.Models;

namespace MoviesRecommender.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMoviesService _moviesService;
        private readonly ITmdbWebService _tmdbWebService;
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, 
            IMoviesService moviesService,
            ITmdbWebService tmdbWebService,
            IMapper mapper)
        {
            _logger = logger;
            _moviesService = moviesService;
            _tmdbWebService = tmdbWebService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var movies = new List<MovieApiModel>();
            var moviesFromDb = _moviesService.GetRandomMovies();

            foreach(var movie in moviesFromDb)
            {
                var apiMovie = await _tmdbWebService.SearchMovieById(movie.TmdbID);
                movies.Add(apiMovie);
            }
            
            var moviesVm = _mapper.Map<IEnumerable<MovieViewModel>>(movies);

            return View(moviesVm);
        }

        public async Task<IActionResult> SearchMovie(string name)
        {
            var moviesFromDb = _moviesService.GetMoviesByName(name).Take(20).ToList();
            var movies = new List<MovieApiModel>();

            foreach (var movie in moviesFromDb)
            {
                var apiMovie = await _tmdbWebService.SearchMovieById(movie.TmdbID);
                if(apiMovie != null)
                    movies.Add(apiMovie);

            }

            
            var moviesVm = _mapper.Map<IEnumerable<MovieViewModel>>(movies).ToList();

            return PartialView("_MovieCard", moviesVm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
