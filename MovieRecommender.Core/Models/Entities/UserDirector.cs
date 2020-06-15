using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Models
{
    public class UserDirector
    {
        public int UserId { get; set; }
        public int DirectorId { get; set; }

        public User User { get; set; }
        public Dictionary Director { get; set; }
    }
}
