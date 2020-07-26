using MovieRecommender.Core.Constants;
using MovieRecommender.Core.Models.ApiModels;
using MovieRecommender.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Interfaces.Repositories
{
    public interface IUserRecomendationRepository
    {
        UserRecomendation GetUserRecomendationsByType(int userId, RecomendationTypes type);

        void SetUserRecomendations(int userId, RecomendationTypes type, List<int> movieIds);

        void RemoveUserRatings(int userId);

        bool Exists(int userId, RecomendationTypes type);
    }
}
