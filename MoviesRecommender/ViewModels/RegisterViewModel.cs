using Microsoft.AspNetCore.Mvc.Rendering;
using MoviesRecommender.WEB.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesRecommender.WEB.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public IEnumerable<SelectListItem> Genres { get; set; }
        public IEnumerable<SelectListItem> Artists { get; set; }
        public IEnumerable<SelectListItem> Directors { get; set; }

        [MultiselectRequired(ErrorMessage = "აირჩიეთ საყვარელი ჟანრები")]
        public List<int> GenreIds { get; set; }

        [MultiselectRequired(ErrorMessage = "აირჩიეთ საყვარელი მსახიობები")]
        public List<int> ArtistIds { get; set; }

        [MultiselectRequired(ErrorMessage = "აირჩიეთ საყვარელი რეჟისორები")]
        public List<int> DirectorIds { get; set; }

    }
}
