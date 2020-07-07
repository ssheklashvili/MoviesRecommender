using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Models.ApiModels
{
    public class UserRatesModel
    {
        public int UserId { get; set; }

        public UserRatesHelperModel[] UserRates { get; set; }
    }

    public class UserRatesHelperModel
    {
        public int MovieId { get; set; }

        public double Rate { get; set; }
    }
}
