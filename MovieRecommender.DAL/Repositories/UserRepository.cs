using Microsoft.EntityFrameworkCore;
using MovieRecommender.Core.Interfaces.Repositories;
using MovieRecommender.Core.Models;
using MovieRecommender.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MovieRecommender.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MoviesRecommenderContext _context;

        public UserRepository(MoviesRecommenderContext context)
        {
            _context = context;
        }
        public List<Dictionary> GetDictionaries()
        {
            return _context.Dictionaries.ToList();
        }

        public User GetUser(string email)
        {
            var user = _context.Users.Where(u => u.Email == email).SingleOrDefault();
            return user;
        }

        public User GetUserWithRates(int userId)
        {
            var user = _context.Users.Where(x => x.ID == userId).Include(x => x.UserRates).FirstOrDefault();
            return user;

        }

        public User SaveUser(string firsName, string lastName, string email, string password)
        {
            var user = new User
            {
                FirstName = firsName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            var userDbModel =_context.Users.Add(user);
            _context.SaveChanges();
            return userDbModel.Entity;
        }

        public void SaveUserFavourites<T>(ICollection<T> entities) where T : class
        {
            _context.Set<T>().AddRange(entities);
            _context.SaveChanges();
        }

        public User GetUserProfile(int userId)
        {
            var user = _context.Users
                .Include(i => i.UserArtists).ThenInclude(i => i.Artist)
                .Include(i => i.UserDirectors).ThenInclude(i => i.Director)
                .Include(i => i.UserGenres).ThenInclude(i => i.Genre)
                .Single(i => i.ID == userId);

            return user;
        }
    }
}
