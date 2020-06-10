using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieRecommender.Core.Services.Interfaces;
using MovieRecommender.Infrastructure.Repositories.Interfaces;
using MoviesRecommender.Models;

namespace MoviesRecommender.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieRepository _movieRepository;
        private readonly ITmdbWebService _tmdbWebService;
        public HomeController(ILogger<HomeController> logger, IMovieRepository movieRepository, ITmdbWebService tmdbWebService)
        {
            _logger = logger;
            _movieRepository = movieRepository;
            _tmdbWebService = tmdbWebService;
        }

        public IActionResult Index()
        {
            //var movies = _tmdbWebService.GetPopularMovies();
            var search = _tmdbWebService.SearchMovie("Gladiator");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
