using MovieRecommender.Core.Constants;
using MovieRecommender.Core.Interfaces.Repositories;
using MovieRecommender.Core.Interfaces.Services;
using MovieRecommender.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Services
{
    public class UserRecomendationService : IUserRecomendationService
    {
        private readonly IUserRecomendationRepository _userRecomendationRepository;

        public UserRecomendationService(IUserRecomendationRepository userRecomendationRepository)
        {
            _userRecomendationRepository = userRecomendationRepository;
        }

        public UserRecomendation GetUserRecomendationsByType(int userId, RecomendationTypes type)
        {
            return _userRecomendationRepository.GetUserRecomendationsByType(userId, type);
        }

        public void SetUserRecomendations(int userId, RecomendationTypes type, List<int> movieIds)
        {
            _userRecomendationRepository.SetUserRecomendations(userId, type, movieIds);
        }


        public void RemoveUserRatings(int userId)
        {
            _userRecomendationRepository.RemoveUserRatings(userId);
        }

        public bool Exists(int userId, RecomendationTypes type)
        {
            return _userRecomendationRepository.Exists(userId, type);
        }
    }
}
