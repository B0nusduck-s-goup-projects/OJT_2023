using AutoMapper;
using BusinessLayer.DTO;
using DataAccessLayer.ObjectEntity;

namespace BusinessLayer.Mapper
{
    //this is a sample mapper used to conveniently creating new mapper
    public class ActionStatusMapper : Profile
    {
        public ActionStatusMapper()
        {
            CreateMap<ActionStatusEntity, ActionStatusDTO>();
            CreateMap<ActionStatusDTO, ActionStatusEntity>();
        }

    }
}
