using MovieRecommender.Core.Models;
using MovieRecommender.Core.Models.ApiModels;
using MovieRecommender.Core.Models.AppModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Interfaces.Services
{
    public interface IUserService
    {
        List<Dictionary> GetDictionaries();
        User GetUser(string email, string password);
        void RegisterUser(UserRegisterModel model);
        ProfileViewModel GetUserProfile(int userId);
        bool UserExists(string email);
        UserProfileModel GetUserProfileData(int userId);
        UserRatesModel GetUserRates(int userId);
    }
}
