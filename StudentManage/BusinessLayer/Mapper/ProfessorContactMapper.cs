using AutoMapper;
using BusinessLayer.DTO;
using DataAccessLayer.ObjectEntity;

namespace BusinessLayer.Mapper
{
    //this is a sample mapper used to conveniently creating new mapper
    public class ProfessorContactMapper : Profile
    {
        public ProfessorContactMapper()
        {
            CreateMap<ProfessorContactEntity, ProfessorContactDTO>();
            CreateMap<ProfessorContactDTO, ProfessorContactEntity>();
        }

    }
}
