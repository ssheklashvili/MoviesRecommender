using AutoMapper;
using MovieRecommender.Core.Models;
using MovieRecommender.Core.Models.ApiModels;
using MovieRecommender.Core.Models.AppModels;
using System;
using System.Collections.Generic;
using System.Linq;
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


            CreateMap<User, UserProfileModel>()
                .ForMember(dest =>
                    dest.UserId,
                    src => src.MapFrom(s => s.ID))
                .ForMember(dest =>
                    dest.ActorIds,
                    src => src.MapFrom(s => s.UserArtists.Select(x => x.ArtistId).ToArray()))
                .ForMember(dest =>
                    dest.DirectoryIds,
                    src => src.MapFrom(s => s.UserDirectors.Select(x => x.DirectorId).ToArray()))
                .ForMember(dest =>
                    dest.GenreIds,
                    src => src.MapFrom(s => s.UserGenres.Select(x => x.GenreId).ToArray()));


            CreateMap<User, UserRatesModel>()
                .ForMember(dest =>
                    dest.UserId,
                    src => src.MapFrom(s => s.ID))
                .ForMember(dest =>
                    dest.UserRates,
                    src => src.MapFrom(s => s.UserRates.Select(x=> new UserRatesHelperModel() {MovieId = x.MovieId, Rate = x.Rate }).ToArray()));
        }
    }
}
