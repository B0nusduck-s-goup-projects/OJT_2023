using AutoMapper;
using BusinessLayer.DTO;
using BusinessLayer.Service.Interface;
using DataAccessLayer.ObjectEntity;
using DataAccessLayer.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Service
{
    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _repository;
        private readonly IMapper _mapper;
        public ProfessorService(IProfessorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //get list
        public List<ProfessorDTO> Get()
        {
            List<ProfessorEntity> list = _repository.Get();
            List<ProfessorDTO> result = _mapper.Map<List<ProfessorDTO>>(list);
            return result;
        }
        
        //get by name
        public List<ProfessorDTO> Get(string name)
        {
            List<ProfessorEntity> list = _repository.Get(name);
            List<ProfessorDTO> result = _mapper.Map<List<ProfessorDTO>>(list);
            return result;
        }

        //get by subject
        public ProfessorDTO? GetBySubject(int id)
        {
            ProfessorEntity? list = _repository.GetBySubject(id);
            ProfessorDTO? result = _mapper.Map<ProfessorDTO?>(list);
            return result;
        }
        
        //get by id
        public ProfessorDTO? Get(int id)
        {
            ProfessorEntity? list = _repository.Get(id);
            ProfessorDTO? result = _mapper.Map<ProfessorDTO?>(list);
            return result;
        }

        //get page
        public List<ProfessorDTO> Get(int pageNum, int pageLength)
        {
            List<ProfessorEntity> list = _repository.Get(pageNum, pageLength);
            List<ProfessorDTO> result = _mapper.Map<List<ProfessorDTO>>(list);
            return result;
        }

        //get page by name
        public List<ProfessorDTO> Get(int pageNum, int pageLength, string name)
        {
            List<ProfessorEntity> list = _repository.Get(pageNum, pageLength, name);
            List<ProfessorDTO> result = _mapper.Map<List<ProfessorDTO>>(list);
            return result;
        }

        //add
        public ActionStatusDTO Post(ProfessorDTO professor)
        {
            ProfessorEntity request = _mapper.Map<ProfessorEntity>(professor);
            if (request.FirstName == "" || request.LastName == "")
            {
                return new ActionStatusDTO { error = "student name must contain first and last name" };
            }
            ActionStatusEntity response = _repository.Post(request);
            ActionStatusDTO result = _mapper.Map<ActionStatusDTO>(response);
            return result;
        }

        //update
        public ActionStatusDTO Put(ProfessorDTO professor)
        {
            ProfessorEntity request = _mapper.Map<ProfessorEntity>(professor);
            if (request.FirstName == "" || request.LastName == "")
            {
                return new ActionStatusDTO { error = "student name must contain first and last name" };
            }
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
