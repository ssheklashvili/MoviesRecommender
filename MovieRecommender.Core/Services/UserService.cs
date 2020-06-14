using MovieRecommender.Core.Interfaces.Repositories;
using MovieRecommender.Core.Interfaces.Services;
using MovieRecommender.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public List<Dictionary> GetDictionaries()
        {
            var dictionaries = _userRepository.GetDictionaries();
            return dictionaries;
        }

        public User GetUser(string email, string password)
        {
            var user = _userRepository.GetUser(email, password);
            return user;
        }
    }
}
