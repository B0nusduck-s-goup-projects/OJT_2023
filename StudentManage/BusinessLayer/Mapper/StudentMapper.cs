using AutoMapper;
using BusinessLayer.DTO;
using DataAccessLayer.ObjectEntity;
using System.Text.RegularExpressions;

namespace BusinessLayer.Mapper
{
    public class StudentMapper : Profile
    {
        private string first = @"^(?:\S*)";
        private string last = @"(?:\S*)$";
        public StudentMapper()
        {
                    
            //map StudentEntity to StudentDTO
            CreateMap<StudentEntity, StudentDTO>()
                .ForMember(dest => dest.FullName, opt =>
                            opt.MapFrom(src => src.MiddleName != "" ? src.FirstName + " " + src.MiddleName + " " + src.LastName
                            : src.FirstName + " " + src.LastName));
            //map StudentDTO to StudentEntity
            CreateMap<StudentDTO, StudentEntity>()
                .ForMember(dest => dest.FirstName, opt=>
                opt.MapFrom(src =>Regex.Match(src.FullName, first).Groups[0].Value))
                .ForMember(dest => dest.LastName, opt =>
                opt.MapFrom(src => Regex.Match(src.FullName, last).Groups[0].Value))
                .ForMember(dest => dest.MiddleName, opt =>
                opt.MapFrom(src => Regex.Replace(Regex.Replace(src.FullName, first, ""), last, "")
                                        .Trim()
                            ));
        }
    }
}
