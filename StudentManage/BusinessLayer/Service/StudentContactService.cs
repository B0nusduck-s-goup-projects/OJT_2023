using AutoMapper;
using BusinessLayer.DTO;
using BusinessLayer.Service.Interface;
using DataAccessLayer.ObjectEntity;
using DataAccessLayer.Repository.Interface;

namespace BusinessLayer.Service
{
    public class StudentContactService : IStudentContactService
    {
        private readonly IStudentContactRepository _repository;
        private readonly IMapper _mapper;
        public StudentContactService(IStudentContactRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //get list
        public List<StudentContactDTO> Get()
        {
            List<StudentContactEntity> list = _repository.Get();
            List<StudentContactDTO> result = _mapper.Map<List<StudentContactDTO>>(list);
            return result;
        }
        
        //get by user
        public StudentContactDTO? GetByUser(int id)
        {
            StudentContactEntity? request = _repository.GetByUser(id);
            StudentContactDTO? result = _mapper.Map<StudentContactDTO?>(request);
            return result;
        }

        //get by id
        public StudentContactDTO? Get(int id)
        {
            StudentContactEntity? list = _repository.Get(id);
            StudentContactDTO? result = _mapper.Map<StudentContactDTO?>(list);
            return result;
        }

        //get page
        public List<StudentContactDTO> Get(int pageNum, int pageLength)
        {
            List<StudentContactEntity> list = _repository.Get(pageNum, pageLength);
            List<StudentContactDTO> result = _mapper.Map<List<StudentContactDTO>>(list);
            return result;
        }

        //add
        public ActionStatusDTO Post(StudentContactDTO studentContact)
        {
            StudentContactEntity request = _mapper.Map<StudentContactEntity>(studentContact);
            ActionStatusEntity response = _repository.Post(request);
            ActionStatusDTO result = _mapper.Map<ActionStatusDTO>(response);
            return result;
        }

        //update
        public ActionStatusDTO Put(StudentContactDTO studentContact)
        {
            StudentContactEntity request = _mapper.Map<StudentContactEntity>(studentContact);
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
