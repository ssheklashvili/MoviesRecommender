using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRecommender.Core.Models.AppModels
{
    public class ProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Genres { get; set; }
        public string Artists { get; set; }
        public string Directors { get; set; }

    }
}
