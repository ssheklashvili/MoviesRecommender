using Microsoft.EntityFrameworkCore;
using MovieRecommender.Core.Constants;
using MovieRecommender.Core.Interfaces.Repositories;
using MovieRecommender.Core.Models.ApiModels;
using MovieRecommender.Core.Models.Entities;
using MovieRecommender.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieRecommender.Infrastructure.Repositories
{
    public class UserRecomendationRepository : IUserRecomendationRepository
    {

        private readonly MoviesRecommenderContext _context;

        public UserRecomendationRepository(MoviesRecommenderContext context)
        {
            _context = context;
        }

        public UserRecomendation GetUserRecomendationsByType(int userId, RecomendationTypes type)
        {
           return _context.UserRecomendations.Where(x => x.UserId == userId && x.RateType == type).Include(x => x.UserRecomendationMovies).ThenInclude(x => x.Movie).FirstOrDefault();
        }

        public void SetUserRecomendations(int userId, RecomendationTypes type, List<int> movieIds)
        {
            UserRecomendation userRecomendation = new UserRecomendation();
            userRecomendation.RateType = type;
            userRecomendation.UserId = userId;
            userRecomendation.UserRecomendationMovies = new List<UserRecomendationToMovie>();
            foreach(var id in movieIds)
            {
                userRecomendation.UserRecomendationMovies.Add(new UserRecomendationToMovie() { MovieId = id });
            }
            _context.UserRecomendations.Add(userRecomendation);
            _context.SaveChanges();
        }


        public void RemoveUserRatings(int userId)
        {
            var userRecomendations = _context.UserRecomendations.Where(x => x.UserId == userId).AsEnumerable();
            if(userRecomendations.Count() > 0)
            {
                foreach (var rec in userRecomendations)
                {
                    _context.UserRecomendations.Remove(rec);
                }
                _context.SaveChanges();
            }
        }


        public bool Exists(int userId, RecomendationTypes type)
        {
            var userRecomendations = _context.UserRecomendations.Where(x => x.UserId == userId && x.RateType == type).AsEnumerable();
            if (userRecomendations.Count() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
