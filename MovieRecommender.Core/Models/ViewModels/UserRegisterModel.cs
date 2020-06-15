using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Models.ViewModels
{
    public class UserRegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<int> GenreIds { get; set; }
        public List<int> ArtistIds { get; set; }
        public List<int> DirectorIds { get; set; }
    }
}
