using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesRecommender.WEB.ViewModels
{
    public class RegisterViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public IEnumerable<SelectListItem> Genres { get; set; }
        public IEnumerable<SelectListItem> Artists { get; set; }
        public IEnumerable<SelectListItem> Directors { get; set; }
        public List<int> GenreIds { get; set; }
        public List<int> ArtistIds { get; set; }
        public List<int> DirectorIds { get; set; }
    }
}
