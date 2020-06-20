using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
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
                if (apiMovie != null)
                    movies.Add(apiMovie);
            }
            
            var moviesVm = _mapper.Map<IEnumerable<MovieViewModel>>(movies);
            foreach (var item in moviesVm)
            {
                var movie = moviesFromDb.FirstOrDefault(i => i.TmdbID == item.TmdbID);
                item.ID = movie.ID;
                item.Rate = movie.UserRates?.FirstOrDefault(i => i.UserId == int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))?.Rate;
            }

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
            foreach (var item in moviesVm)
            {
                var movie = moviesFromDb.FirstOrDefault(i => i.TmdbID == item.ID);
                item.ID = movie.ID;
                item.TmdbID = movie.TmdbID;
                item.Rate = movie.UserRates?.FirstOrDefault(i => i.UserId == int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))?.Rate;
            }

            return PartialView("_MovieCard", moviesVm);
        }


        public async Task<IActionResult> GetRecommendation(int userId)
        {
            var movies = new List<MovieApiModel>();
            var moviesFromDb = _moviesService.GetRandomMovies().Take(10).ToList();

            foreach (var movie in moviesFromDb)
            {
                var apiMovie = await _tmdbWebService.SearchMovieById(movie.TmdbID);
                if (apiMovie != null)
                    movies.Add(apiMovie);
            }

            var moviesVm = _mapper.Map<IEnumerable<MovieViewModel>>(movies);

            return PartialView("_MovieCard", moviesVm);
        }

        [HttpPost]
        public async Task<IActionResult> RateMovie(int userId, int movieId, float rate)
        {
            try
            {
                _moviesService.RateMovie(userId, movieId, rate);
                return Json("success");
            }
            catch(Exception ex)
            {
                return Json("fail");
            }
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
