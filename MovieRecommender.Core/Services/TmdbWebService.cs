using Microsoft.Extensions.Configuration;
using MovieRecommender.Core.Models.ApiModels;
using MovieRecommender.Core.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MovieRecommender.Core.Services
{
    public class TmdbWebService : ITmdbWebService
    {
        private readonly HttpClient client;

        private readonly string baseUrl;
        private readonly string apiKey;
        private IConfiguration Configuration { get; }

        public TmdbWebService(IConfiguration configuration)
        {
            Configuration = configuration.GetSection("TmdbService");
            baseUrl = Configuration.GetSection("Url").Value;
            apiKey = Configuration.GetSection("ApiKey").Value;

            client = new HttpClient { BaseAddress = new Uri(baseUrl ?? "") };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", "text/plain;charset=UTF-8");
        }

        public async Task<List<MovieApiModel>> GetPopularMovies()
        {
            try
            {
                var queryParams = new Dictionary<string, string> { { "api_key", apiKey } };
                var queryString = string.Join("&", queryParams.Where(p => !string.IsNullOrEmpty(p.Value)).Select(p => $"{p.Key}={p.Value}"));

                using(var requestMessage = new HttpRequestMessage(HttpMethod.Get,$"movie/popular?{queryString}"))
                {
                    var response = await client.SendAsync(requestMessage);
                    response.EnsureSuccessStatusCode();

                    var result = await response.Content.ReadAsStringAsync();
                    var movies = JObject.Parse(result)["results"].ToObject<ObservableCollection<MovieApiModel>>().ToList();
                    return movies;
                }

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<List<MovieApiModel>> SearchMovie(string name)
        {
            try
            {
                var queryParams = new Dictionary<string, string> { { "api_key", apiKey }, { "query", name } };
                var queryString = string.Join("&", queryParams.Where(p => !string.IsNullOrEmpty(p.Value)).Select(p => $"{p.Key}={p.Value}"));

                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"search/movie?{queryString}"))
                {
                    var response = await client.SendAsync(requestMessage);
                    response.EnsureSuccessStatusCode();

                    var result = await response.Content.ReadAsStringAsync();
                    var movies = JObject.Parse(result)["results"].ToObject<ObservableCollection<MovieApiModel>>().ToList();
                    return movies;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
