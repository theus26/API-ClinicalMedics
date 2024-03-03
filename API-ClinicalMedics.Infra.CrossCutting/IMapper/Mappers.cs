using API_ClinicalMedics.Domain.DTO;
using API_ClinicalMedics.Domain.Entities;
using AutoMapper;

namespace API_ClinicalMedics.Infra.CrossCutting.IMapper
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<Users, UserDTO>().ReverseMap();
            CreateMap<Attachaments, AttachamentDTO>().ReverseMap();
        }
    }
}
