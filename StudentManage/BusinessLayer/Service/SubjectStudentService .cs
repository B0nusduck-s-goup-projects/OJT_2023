using AutoMapper;
using BusinessLayer.DTO;
using BusinessLayer.Service.Interface;
using DataAccessLayer.ObjectEntity;
using DataAccessLayer.Repository.Interface;

namespace BusinessLayer.Service
{
    public class SubjectStudentService : ISubjectStudentService
    {
        private readonly ISubjectStudentRepository _repository;
        private readonly IMapper _mapper;
        public SubjectStudentService(ISubjectStudentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //get list
        public List<SubjectStudentDTO> Get()
        {
            List<SubjectStudentEntity> list = _repository.Get();
            List<SubjectStudentDTO> result = _mapper.Map<List<SubjectStudentDTO>>(list);
            return result;
        }

        //get by id
        public SubjectStudentDTO? Get(int subjectId, int studentId)
        {
            SubjectStudentEntity? list = _repository.Get(subjectId, studentId);
            SubjectStudentDTO? result = _mapper.Map<SubjectStudentDTO?>(list);
            return result;
        }

        //get by student
        public List<SubjectStudentDTO> GetByStudent(int studentId)
        {
            List<SubjectStudentEntity> list = _repository.GetByStudent(studentId);
            List<SubjectStudentDTO> result = _mapper.Map<List<SubjectStudentDTO>>(list);
            return result;
        }

        //get by subject
        public List<SubjectStudentDTO> GetBySubject(int subjectId)
        {
            List<SubjectStudentEntity> list = _repository.GetBySubject(subjectId);
            List<SubjectStudentDTO> result = _mapper.Map<List<SubjectStudentDTO>>(list);
            return result;
        }

        //get page
        public List<SubjectStudentDTO> GetPage(int pageNum, int pageLength)
        {
            List<SubjectStudentEntity> list = _repository.GetPage(pageNum, pageLength);
            List<SubjectStudentDTO> result = _mapper.Map<List<SubjectStudentDTO>>(list);
            return result;
        }

        //add
        public ActionStatusDTO Post(SubjectStudentDTO subjectStudent)
        {
            SubjectStudentEntity request = _mapper.Map<SubjectStudentEntity>(subjectStudent);
            ActionStatusEntity response = _repository.Post(request);
            ActionStatusDTO result = _mapper.Map<ActionStatusDTO>(response);
            return result;
        }

        //update
        public ActionStatusDTO Put(SubjectStudentDTO subjectStudent)
        {
            SubjectStudentEntity request = _mapper.Map<SubjectStudentEntity>(subjectStudent);
            ActionStatusEntity response = _repository.Put(request);
            ActionStatusDTO result = _mapper.Map<ActionStatusDTO>(response);
            return result;
        }

        //delete
        public ActionStatusDTO Delete(int subjectId, int studentId)
        {
            ActionStatusEntity response = _repository.Delete(subjectId, studentId);
            ActionStatusDTO result = _mapper.Map<ActionStatusDTO>(response);
            return result;
        }
    }
}
