using MovieRecommender.Core.Interfaces.Repositories;
using MovieRecommender.Core.Models;
using MovieRecommender.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public User GetUser(string email, string password)
        {
            var user = _context.Users.Where(u => u.Email == email && u.Password == password).SingleOrDefault();
            return user;
        }
    }
}
