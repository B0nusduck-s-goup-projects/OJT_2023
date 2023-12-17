using AutoMapper;
using BusinessLayer.DTO;
using BusinessLayer.Service.Interface;
using DataAccessLayer.ObjectEntity;
using DataAccessLayer.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        private readonly IMapper _mapper;
        public StudentService(IStudentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //get list
        public List<StudentDTO> Get()
        {
            List<StudentEntity> list = _repository.Get();
            List<StudentDTO> result = _mapper.Map<List<StudentDTO>>(list);
            return result;
        }
        
        //get by name
        public List<StudentDTO> Get(string name)
        {
            List<StudentEntity> list = _repository.Get(name);
            List<StudentDTO> result = _mapper.Map<List<StudentDTO>>(list);
            return result;
        }

        //get by id
        public StudentDTO? Get(int id)
        {
            StudentEntity? list = _repository.Get(id);
            StudentDTO? result = _mapper.Map<StudentDTO?>(list);
            return result;
        }

        //get page
        public List<StudentDTO> Get(int pageNum, int pageLength)
        {
            List<StudentEntity> list = _repository.Get(pageNum, pageLength);
            List<StudentDTO> result = _mapper.Map<List<StudentDTO>>(list);
            return result;
        }

        //get page by name
        public List<StudentDTO> Get(int pageNum, int pageLength, string name)
        {
            List<StudentEntity> list = _repository.Get(pageNum, pageLength, name);
            List<StudentDTO> result = _mapper.Map<List<StudentDTO>>(list);
            return result;
        }

        //add
        public ActionStatusDTO Post(StudentDTO student)
        {
            StudentEntity request = _mapper.Map<StudentEntity>(student);
            ActionStatusEntity response = _repository.Post(request);
            ActionStatusDTO result = _mapper.Map<ActionStatusDTO>(response);
            return result;
        }

        //update
        public ActionStatusDTO Put(StudentDTO student)
        {
            StudentEntity request = _mapper.Map<StudentEntity>(student);
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
