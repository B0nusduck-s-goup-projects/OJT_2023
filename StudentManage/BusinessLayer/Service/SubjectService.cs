using AutoMapper;
using BusinessLayer.DTO;
using BusinessLayer.Service.Interface;
using DataAccessLayer.ObjectEntity;
using DataAccessLayer.Repository.Interface;

namespace BusinessLayer.Service
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _repository;
        private readonly IMapper _mapper;
        public SubjectService(ISubjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //get list
        public List<SubjectDTO> Get()
        {
            List<SubjectEntity> list = _repository.Get();
            List<SubjectDTO> result = _mapper.Map<List<SubjectDTO>>(list);
            return result;
        }

        //get by name
        public List<SubjectDTO> Get(string name)
        {
            List<SubjectEntity> list = _repository.Get(name);
            List<SubjectDTO> result = _mapper.Map<List<SubjectDTO>>(list);
            return result;
        }

        //get by id
        public SubjectDTO? Get(int id)
        {
            SubjectEntity? list = _repository.Get(id);
            SubjectDTO? result = _mapper.Map<SubjectDTO?>(list);
            return result;
        }

        //get page
        public List<SubjectDTO> Get(int pageNum, int pageLength)
        {
            List<SubjectEntity> list = _repository.Get(pageNum, pageLength);
            List<SubjectDTO> result = _mapper.Map<List<SubjectDTO>>(list);
            return result;
        }

        //get page by name
        public List<SubjectDTO> Get(int pageNum, int pageLength, string name)
        {
            List<SubjectEntity> list = _repository.Get(pageNum, pageLength, name);
            List<SubjectDTO> result = _mapper.Map<List<SubjectDTO>>(list);
            return result;
        }

        //add
        public ActionStatusDTO Post(SubjectDTO subject)
        {
            SubjectEntity request = _mapper.Map<SubjectEntity>(subject);
            ActionStatusEntity response = _repository.Post(request);
            ActionStatusDTO result = _mapper.Map<ActionStatusDTO>(response);
            return result;
        }

        //update
        public ActionStatusDTO Put(SubjectDTO subject)
        {
            SubjectEntity request = _mapper.Map<SubjectEntity>(subject);
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
