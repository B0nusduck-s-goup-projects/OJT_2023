using AutoMapper;
using BusinessLayer.DTO;
using DataAccessLayer.ObjectEntity;
using System.Text.RegularExpressions;

namespace BusinessLayer.Mapper
{
    public class ProfessorMapper : Profile
    {
        private string first = @"^(?:\S*)";
        private string last = @"(?:\S*)$";
        public ProfessorMapper()
        {
            //map ProfessorEntity to ProfessorDTO
            CreateMap<ProfessorEntity, ProfessorDTO>()
                .ForMember(dest => dest.FullName, opt =>
                            opt.MapFrom(src => src.MiddleName != "" ? src.FirstName + " " + src.MiddleName + " " + src.LastName
                            : src.LastName != "" ? src.FirstName + " " + src.LastName : src.FirstName));
            //map ProfessorDTO to ProfessorEntity
            CreateMap<ProfessorDTO, ProfessorEntity>()
                .ForMember(dest => dest.FirstName, opt=>
                opt.MapFrom(src =>Regex.Match(src.FullName, first).Groups[0].Value))
                .ForMember(dest => dest.LastName, opt =>
                opt.MapFrom(src => Regex.Match(Regex.Replace(src.FullName, first, "").Trim(), last).Groups[0].Value))
                .ForMember(dest => dest.MiddleName, opt =>
                opt.MapFrom(src => Regex.Replace(Regex.Replace(src.FullName, first, ""), last, "")
                                        .Trim()
                            ));
        }
    }
}
