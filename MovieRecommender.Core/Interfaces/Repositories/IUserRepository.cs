using MovieRecommender.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        List<Dictionary> GetDictionaries();
        User GetUser(string email, string password);
    }
}
