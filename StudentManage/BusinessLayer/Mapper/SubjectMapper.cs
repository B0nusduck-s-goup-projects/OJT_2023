using AutoMapper;
using BusinessLayer.DTO;
using DataAccessLayer.ObjectEntity;

namespace BusinessLayer.Mapper
{
    //this is a sample mapper used to conveniently creating new mapper
    public class SubjectMapper : Profile
    {
        public SubjectMapper()
        {
            CreateMap<SubjectEntity, SubjectDTO>();
            CreateMap<SubjectDTO, SubjectEntity>();
        }

    }
}
