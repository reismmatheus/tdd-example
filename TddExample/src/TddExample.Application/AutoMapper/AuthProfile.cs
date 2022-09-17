using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddExample.Application.Model;
using TddExample.Domain;

namespace TddExample.Application.AutoMapper
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<AuthModel, User>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => $"{Convert.ToInt32(src.Id)}")
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name)
                ).ReverseMap();
        }
    }
}
