using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieRecommender.Core.Models.ViewModels;
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
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, 
            IMovieRepository movieRepository,
            ITmdbWebService tmdbWebService,
            IMapper mapper)
        {
            _logger = logger;
            _movieRepository = movieRepository;
            _tmdbWebService = tmdbWebService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var apiMovies = await _tmdbWebService.GetPopularMovies();
            //var movies =  _mapper.Map<MovieViewModel>(apiMovies);

            return View(apiMovies);
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
