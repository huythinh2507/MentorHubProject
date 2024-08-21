using AutoMapper;
using YPP.MH.DataAccessLayer.Models;
using YPP.MH.PresentationLayer.ViewModels;

namespace YPP.MH.PresentationLayer.Mappings
{
    public class MapperConfig : Profile

    {
        public MapperConfig()
        {
            CreateMap<UserViewModel, User>().ReverseMap();
        }
    }
}
