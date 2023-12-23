using AutoMapper;
using BusinessLayer.DTO;
using DataAccessLayer.ObjectEntity;

namespace BusinessLayer.Mapper
{
    //this is a sample mapper used to conveniently creating new mapper
    public class StudentContactMapper : Profile
    {
        public StudentContactMapper()
        {
            CreateMap<StudentContactEntity, StudentContactDTO>();
            CreateMap<StudentContactDTO, StudentContactEntity>();
        }

    }
}
