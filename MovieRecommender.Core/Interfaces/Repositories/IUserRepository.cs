using MovieRecommender.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        List<Dictionary> GetDictionaries();
        User GetUser(string email);
        User SaveUser(string firsName, string lastName, string email, string password);
        void SaveUserFavourites<T>(ICollection<T> entities) where T : class;
    }
}
