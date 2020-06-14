using MovieRecommender.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Interfaces.Services
{
    public interface IUserService
    {
        List<Dictionary> GetDictionaries();
        User GetUser(string email, string password);
    }
}
