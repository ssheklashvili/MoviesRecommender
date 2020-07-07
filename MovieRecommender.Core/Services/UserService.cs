using AutoMapper;
using MovieRecommender.Core.Interfaces.Repositories;
using MovieRecommender.Core.Interfaces.Services;
using MovieRecommender.Core.Models;
using MovieRecommender.Core.Models.ApiModels;
using MovieRecommender.Core.Models.AppModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MovieRecommender.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        
        public List<Dictionary> GetDictionaries()
        {
            var dictionaries = _userRepository.GetDictionaries();
            return dictionaries;
        }

        public bool UserExists(string email)
        {
            var user = _userRepository.GetUser(email);
            if (user == null) return false;
            return true;
        }


        public UserProfileModel GetUserProfileData(int userId)
        {
            var user = _userRepository.GetUserProfile(userId);
            return _mapper.Map<UserProfileModel>(user);
        }


        public UserRatesModel GetUserRates(int userId)
        {
            var user = _userRepository.GetUserWithRates(userId);
            return _mapper.Map<UserRatesModel>(user);
        }

        public User GetUser(string email, string password)
        {
            var user = _userRepository.GetUser(email);
            if (user == null) return null;

            var savedPasswordHash = user.Password;
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return null;
            return user;
        }

        public ProfileViewModel GetUserProfile(int userId)
        {
            var user = _userRepository.GetUserProfile(userId);

            string artists = string.Empty;
            string genres = string.Empty;
            string directors = string.Empty;

            foreach(var artist in user.UserArtists)
            {
                artists += artist.Artist.Name + ", ";
            }
            foreach(var genre in user.UserGenres)
            {
                genres += genre.Genre.Name + ", ";
            }
            foreach(var director in user.UserDirectors)
            {
                directors += director.Director.Name + ", ";
            }
            var profileVM = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Artists = artists.Remove(artists.Length - 2),
                Genres = genres.Remove(genres.Length - 2),
                Directors = directors.Remove(directors.Length - 2)
            };

            return profileVM;
        }

        private string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            return savedPasswordHash;
        }
        public void RegisterUser(UserRegisterModel model)
        {
            var password = HashPassword(model.Password);
            var user = _userRepository.SaveUser(model.FirstName, model.LastName, model.Email, password);

            var userGenres = model.GenreIds.Select(i => new UserGenre
            {
                UserId = user.ID,
                GenreId = i
            }).ToList();

            var userArtists = model.ArtistIds.Select(i => new UserArtist
            {
                UserId = user.ID,
                ArtistId = i
            }).ToList();

            var userDirectors = model.DirectorIds.Select(i => new UserDirector
            {
                UserId = user.ID,
                DirectorId = i
            }).ToList();

            _userRepository.SaveUserFavourites(userGenres);
            _userRepository.SaveUserFavourites(userArtists);
            _userRepository.SaveUserFavourites(userDirectors);

        }
    }
}
