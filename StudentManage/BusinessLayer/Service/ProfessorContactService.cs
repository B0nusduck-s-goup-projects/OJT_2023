using AutoMapper;
using BusinessLayer.DTO;
using BusinessLayer.Service.Interface;
using DataAccessLayer.ObjectEntity;
using DataAccessLayer.Repository.Interface;
using System.ComponentModel;

namespace BusinessLayer.Service
{
    public class ProfessorContactService : IProfessorContactService
    {
        private readonly IProfessorContactRepository _repository;
        private readonly IMapper _mapper;
        public ProfessorContactService(IProfessorContactRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //get list
        public List<ProfessorContactDTO> Get()
        {
            List<ProfessorContactEntity> list = _repository.Get();
            List<ProfessorContactDTO> result = _mapper.Map<List<ProfessorContactDTO>>(list);
            return result;
        }

        //get by user
        public ProfessorContactDTO? GetByUser(int id)
        {
            ProfessorContactEntity? list = _repository.GetByUser(id);
            ProfessorContactDTO? result = _mapper.Map<ProfessorContactDTO?>(list);
            return result;
        }

        //get by id
        public ProfessorContactDTO? Get(int id)
        {
            ProfessorContactEntity? entity = _repository.Get(id);
            ProfessorContactDTO? result = _mapper.Map<ProfessorContactDTO?>(entity);
            return result;
        }

        //get page
        public List<ProfessorContactDTO> Get(int pageNum, int pageLength)
        {
            List<ProfessorContactEntity> list = _repository.Get(pageNum, pageLength);
            List<ProfessorContactDTO> result = _mapper.Map<List<ProfessorContactDTO>>(list);
            return result;
        }
        
        //add
        public ActionStatusDTO Post(ProfessorContactDTO contact)
        {
            
            ProfessorContactEntity request = _mapper.Map<ProfessorContactEntity>(contact);
            ActionStatusEntity response = _repository.Post(request);
            ActionStatusDTO result = _mapper.Map<ActionStatusDTO>(response);
            return result;
        }
        
        //update
        public ActionStatusDTO Put(ProfessorContactDTO contact)
        {
            ProfessorContactEntity request = _mapper.Map<ProfessorContactEntity>(contact);
            ActionStatusEntity response = _repository.Put(request);
            ActionStatusDTO result = _mapper.Map<ActionStatusDTO>(response);
            return result;
        }
        
        //delete
        public ActionStatusDTO Delete(int id)
        {
            ActionStatusEntity response = _repository.Delete(id);
            ActionStatusDTO result = _mapper.Map<ActionStatusDTO>(response);
            return result;
        }
    }
}
