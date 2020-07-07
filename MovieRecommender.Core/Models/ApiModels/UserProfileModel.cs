using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Models.ApiModels
{
    public class UserProfileModel
    {
        public int UserId { get; set; }

        public int[] DirectoryIds { get; set; }

        public int[] ActorIds { get; set; }

        public int[] GenreIds { get; set; }


    }
}
