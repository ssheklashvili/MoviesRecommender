using AutoMapper;
using MovieRecommender.Core.Models.ApiModels;
using MovieRecommender.Core.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Configuration.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MovieApiModel, MovieViewModel>(MemberList.None);
            CreateMap<MovieViewModel, MovieApiModel>(MemberList.None);
        }
    }
}
