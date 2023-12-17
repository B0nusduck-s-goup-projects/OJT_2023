using AutoMapper;
using BusinessLayer.DTO;
using DataAccessLayer.ObjectEntity;

namespace BusinessLayer.Mapper
{
    public class SubjectStudentMapper : Profile
    {
        public SubjectStudentMapper()
        {
            CreateMap<SubjectStudentEntity, SubjectStudentDTO>();
            CreateMap<SubjectStudentDTO, SubjectStudentEntity>();
        }

    }
}
