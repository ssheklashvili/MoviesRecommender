using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Models.AppModels
{
    public class MovieViewModel
    {
        public int ID { get; set; }

        public int TmdbID { get; set; }
        public string PosterPath { get; set; }
        public string TiTle { get; set; }
        public double? Rate { get; set; }
    }
}
