using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPP.MH.DataAccessLayer.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreatePostView, Post>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsPublished, opt => opt.MapFrom(src => (bool?)src.IsPublished))
                .ForMember(dest => dest.TimeSchedule, opt => opt.MapFrom(src => (DateTime?)src.TimeSchedule));
        }
    }
}
