using AutoMapper;
using WebApplication1.Data.Entities;
using WebApplication1.Models.DTO;

namespace WebApplication1.AutoMapperProfiles
{
    public class NewsProfile:Profile
    {
        public NewsProfile()
        {
            CreateMap<News, newsDTO>().ReverseMap();
        }
    }
}
