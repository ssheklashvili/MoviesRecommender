using AutoMapper;
using MovieRecommender.Core.Models.ApiModels;
using MovieRecommender.Core.Models.AppModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Core.Configuration.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MovieApiModel, MovieViewModel>(MemberList.None)
                .ForMember(dest => dest.TmdbID, src => src.MapFrom(s => s.ID))
                .ForMember(dest => dest.ID, src => src.Ignore());
        }
    }
}
