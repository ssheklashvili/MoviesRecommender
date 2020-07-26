using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieRecommender.Core.Constants;
using MovieRecommender.Core.Interfaces.Repositories;
using MovieRecommender.Core.Interfaces.Services;
using MovieRecommender.Core.Models.ApiModels;
using MovieRecommender.Core.Models.AppModels;
using MovieRecommender.Core.Services;
using MovieRecommender.Infrastructure.Repositories;
using MoviesRecommender.Models;
using Newtonsoft.Json;

namespace MoviesRecommender.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMoviesService _moviesService;
        private readonly IUserRecomendationService _userRecomendationService;
        private readonly ITmdbWebService _tmdbWebService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, 
            IMoviesService moviesService,
            IUserService userService,
            ITmdbWebService tmdbWebService,
            IUserRecomendationService userRecomendationService,
            IMapper mapper)
        {
            _logger = logger;
            _userService = userService;
            _moviesService = moviesService;
            _userRecomendationService = userRecomendationService;
            _tmdbWebService = tmdbWebService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetMovies(string name, int? page, int? userId = null)
        {
            var moviesFromDb = _moviesService.GetMovies(name, page, userId);

            var movies = new List<MovieApiModel>();

            foreach (var movie in moviesFromDb)
            {
                var apiMovie = await _tmdbWebService.SearchMovieById(movie.TmdbID);
                if (apiMovie != null)
                    movies.Add(apiMovie);

            }
            var moviesVm = _mapper.Map<IEnumerable<MovieViewModel>>(movies).ToList();
            foreach (var item in moviesVm)
            {
                var movie = moviesFromDb.FirstOrDefault(i => i.TmdbID == item.TmdbID);
                item.ID = movie.ID;
                item.Rate = movie.UserRates?.FirstOrDefault(i => i.UserId == int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))?.Rate;
            }

            return PartialView("_MovieCard", moviesVm);
        }

        public async Task<PartialViewResult> GetRecommendationWithProfile(int userId)
        {
            if(_userRecomendationService.Exists(userId, RecomendationTypes.OWA))
            {
                var userRecomendation = _userRecomendationService.GetUserRecomendationsByType(userId, RecomendationTypes.OWA);
                var moviesVm = await GetMoveiModel(userRecomendation.UserRecomendationMovies.Select(x=>x.MovieId).ToList());
                return PartialView("_MovieCard", moviesVm);
            }
            var profile = _userService.GetUserProfileData(userId);
            var rates = _userService.GetUserRates(userId);
            var movies = _moviesService.GetOrderedMovieIds();
            var ratedMovieIds = _moviesService.GetRatedMovieIds(userId);
            double[] ratedMovies = new double[movies.Length];
            foreach (var item in rates.UserRates)
            {
                int index = Array.IndexOf(movies, item.MovieId);
                ratedMovies[index] = item.Rate;
            }

            var directors = string.Join(",", profile.DirectoryIds.Select(x => x.ToString()));
            var actors = string.Join(",", profile.ActorIds.Select(x => x.ToString()));
            var genres = string.Join(",", profile.GenreIds.Select(x => x.ToString()));
            var profileQuery = $"[{userId},[{directors}],[{actors}],[{genres}]]";
            var userRates = string.Join(",", ratedMovies);
            var usratedMovies = string.Join(",", ratedMovieIds);
            var ratesQuery = $"[{userId},[{userRates}]]";
            var ratedMovieIdQuery = $"[{usratedMovies}]";

            var model = new
            {
                profile = profileQuery,
                rating = ratesQuery
            };
            var myContent = JsonConvert.SerializeObject(model);


            var content = "{\"profile\":" + profileQuery + ",\"rating\":" + ratesQuery + ", \"userRatedMovieIds\":" + ratedMovieIdQuery + "}";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"http://localhost:5443/{userId}/profileratings/top/50");

                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.Timeout = TimeSpan.FromMinutes(10);
                var result = client.PostAsync("", byteContent).Result;
                string resultContent = result.Content.ReadAsStringAsync().Result;
                var mo = apiToModel(resultContent);
                _userRecomendationService.SetUserRecomendations(userId, RecomendationTypes.OWA, mo.Select(x => x.movieId).ToList());


                var moviesVm = await GetMoveiModel(mo.Select(x => x.movieId).ToList());

                return PartialView("_MovieCard", moviesVm);
            }
        }

        public async Task<PartialViewResult> GetRecommendationWithTopsis(int userId)
        {
            if (_userRecomendationService.Exists(userId, RecomendationTypes.TOPSIS))
            {
                var userRecomendation = _userRecomendationService.GetUserRecomendationsByType(userId, RecomendationTypes.TOPSIS);
                var moviesVm = await GetMoveiModel(userRecomendation.UserRecomendationMovies.Select(x => x.MovieId).ToList());
                return PartialView("_MovieCard", moviesVm);
            }
            var profile = _userService.GetUserProfileData(userId);
            var rates = _userService.GetUserRates(userId);
            var movies = _moviesService.GetOrderedMovieIds();
            var ratedMovieIds = _moviesService.GetRatedMovieIds(userId);
            double[] ratedMovies = new double[movies.Length];
            foreach (var item in rates.UserRates)
            {
                int index = Array.IndexOf(movies, item.MovieId);
                ratedMovies[index] = item.Rate;
            }

            var directors = string.Join(",", profile.DirectoryIds.Select(x => x.ToString()));
            var actors = string.Join(",", profile.ActorIds.Select(x => x.ToString()));
            var genres = string.Join(",", profile.GenreIds.Select(x => x.ToString()));
            var profileQuery = $"[{userId},[{directors}],[{actors}],[{genres}]]";
            var userRates = string.Join(",", ratedMovies);
            var usratedMovies = string.Join(",", ratedMovieIds);
            var ratesQuery = $"[{userId},[{userRates}]]";
            var ratedMovieIdQuery = $"[{usratedMovies}]";
            var model = new
            {
                profile = profileQuery,
                rating = ratesQuery
            };
            var myContent = JsonConvert.SerializeObject(model);


            var content = "{\"profile\":" + profileQuery + ",\"rating\":" + ratesQuery + ", \"userRatedMovieIds\":" + ratedMovieIdQuery + "}";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"http://localhost:5443/{userId}/profileratingswithtopsis/top/50");

                var buffer = Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.Timeout = TimeSpan.FromMinutes(10);
                var result = client.PostAsync("", byteContent).Result;
                string resultContent = result.Content.ReadAsStringAsync().Result;
                var mo = apiToModel(resultContent);
                _userRecomendationService.SetUserRecomendations(userId, RecomendationTypes.TOPSIS, mo.Select(x => x.movieId).ToList());


                var moviesVm = await GetMoveiModel(mo.Select(x => x.movieId).ToList());

                return PartialView("_MovieCard", moviesVm);
            }
        }




        private async Task<IEnumerable<MovieViewModel>> GetMoveiModel(List<int> MovieIds)
        {
            var moviesFromDb = _moviesService.GetMoviesByIds(MovieIds);
            var moviess = new List<MovieApiModel>();
            foreach (var movie in moviesFromDb)
            {
                var apiMovie = await _tmdbWebService.SearchMovieById(movie.TmdbID);
                if (apiMovie != null)
                    moviess.Add(apiMovie);

            }
            var moviesVm = _mapper.Map<IEnumerable<MovieViewModel>>(moviess).ToList();
            foreach (var item in moviesVm)
            {
                var movie = moviesFromDb.FirstOrDefault(i => i.TmdbID == item.TmdbID);
                item.ID = movie.ID;
                item.Rate = movie.UserRates?.FirstOrDefault(i => i.UserId == int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))?.Rate;
            }

            return moviesVm;
        }


        public async Task<PartialViewResult> GetRecommendationWithoutProfile(int userId)
        {
            var rates = _userService.GetUserRates(userId);
            var movies = _moviesService.GetOrderedMovieIds();
            double[] ratedMovies = new double[movies.Length];
            foreach (var item in rates.UserRates)
            {
                int index = Array.IndexOf(movies, item.MovieId);
                ratedMovies[index] = item.Rate;
            }

            var userRates = string.Join(",", ratedMovies);

            //   var ratesQuery = $"[{rates.UserId},[{userRates}]]";
            var ratesQuery = $"[611,[{userRates}]]";
            var model = new
            {
                rating = ratesQuery
            };
            var myContent = JsonConvert.SerializeObject(model);


            var content = "{\"rating\":" + ratesQuery + "}";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5443/611/cosineratings/top/50");

                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.Timeout = TimeSpan.FromMinutes(10);
                var result = client.PostAsync("", byteContent).Result;
                string resultContent = result.Content.ReadAsStringAsync().Result;
                var mo = apiToModel(resultContent);
                var moviesFromDb = _moviesService.GetMoviesByIds(mo.Select(x=>x.movieId).ToList());

                var moviess = new List<MovieApiModel>();

                foreach (var movie in moviesFromDb)
                {
                    var apiMovie = await _tmdbWebService.SearchMovieById(movie.TmdbID);
                    if (apiMovie != null)
                        moviess.Add(apiMovie);

                }
                var moviesVm = _mapper.Map<IEnumerable<MovieViewModel>>(moviess).ToList();
                foreach (var item in moviesVm)
                {
                    var movie = moviesFromDb.FirstOrDefault(i => i.TmdbID == item.TmdbID);
                    item.ID = movie.ID;
                    item.Rate = movie.UserRates?.FirstOrDefault(i => i.UserId == int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))?.Rate;
                }
                return PartialView("_MovieCard", moviesVm);
            }
        }


        private List<MovieRateModel> apiToModel(string rrr)
        {
            
            rrr = rrr.Remove(0, 1);
            rrr = rrr.Replace("[", string.Empty);
            rrr = rrr.Remove(rrr.Length - 2, 2);
            var tt = rrr.Split("],");
            List<MovieRateModel> m = new List<MovieRateModel>();
            foreach (var item in tt)
            {
                MovieRateModel mmm = new MovieRateModel();
                var str = item.Trim();
                var tss = str.Split(",");
                mmm.movieId = int.Parse(tss[0]);
                mmm.rateId = double.Parse(tss[1]);

                m.Add(mmm);
            }
            return m;
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
        public async Task<IActionResult> RateMovie(int movieId, float rate)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                _moviesService.RateMovie(userId, movieId, rate);
                _userRecomendationService.RemoveUserRatings(userId);
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
