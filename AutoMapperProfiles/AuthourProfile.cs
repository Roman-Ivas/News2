using AutoMapper;
using WebApplication1.Data.Entities;
using WebApplication1.Models.DTO;
namespace WebApplication1.AutoMapperProfiles
{
    public class AuthourProfile:Profile
    {
        public AuthourProfile() { 
        CreateMap<Authour, AuthourDTO>().ReverseMap();
        }
    }
}
