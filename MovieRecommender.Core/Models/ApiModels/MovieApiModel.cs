using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Models.ApiModels
{
    public class MovieApiModel
    {
        
        public int ID { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }
        public string Overview { get; set; }

        [JsonProperty("release_date")]
        public DateTime? ReleaseDate { get; set; }

        [JsonProperty("genre_ids")]
        public List<int> GenreIds { get; set; }

        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }

        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }
        public string TiTle { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }
        public double Popularity { get; set; }

        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }
        public bool Video { get; set; }

        [JsonProperty("vote_average")]
        public double VoteAvarage { get; set; }
        public bool Adult { get; set; }
    }
}
