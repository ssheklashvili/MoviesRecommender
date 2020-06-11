using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Models.ViewModels
{
    public class MovieViewModel
    {
        public int ID { get; set; }
        public string PosterPath { get; set; }
        public string TiTle { get; set; }
    }
}
