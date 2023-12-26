using DataAccessLayer.Data;
using DataAccessLayer.ObjectEntity;
using DataAccessLayer.Repository.Interface;

namespace DataAccessLayer.Repository
{
    public class SubjectStudentRepository : ISubjectStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public SubjectStudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //get list
        public List<SubjectStudentEntity> Get()
        {
            List<SubjectStudentEntity> list = _context.SubjectStudent.ToList();
            return list;
        }

        //get by id
        public SubjectStudentEntity? Get(int subjectId, int studentId)
        {
            return HasSubjectStudent(subjectId, studentId);
        }

        //get by student
        public List<SubjectStudentEntity> GetByStudent(int studentId)
        {
            List<SubjectStudentEntity> list = _context.SubjectStudent.Where(x => x.StudentId == studentId).ToList();
            return list;
        }

        //get by subject
        public List<SubjectStudentEntity> GetBySubject(int subjectId)
        {
            List<SubjectStudentEntity> list = _context.SubjectStudent.Where(x => x.SubjectId == subjectId).ToList();
            return list;
        }

        //get page
        public List<SubjectStudentEntity> GetPage(int pageNum, int pageLength)
        {
            List<SubjectStudentEntity> page = _context.SubjectStudent.OrderBy(s=>s.SubjectId).Skip(pageNum * pageLength)
                                                                        .Take(pageLength).ToList();
            return page;
        }
        
        //add
        public ActionStatusEntity Post(SubjectStudentEntity subjectStudent)
        {
            if (subjectStudent == null)
            {
                return new ActionStatusEntity { error = "please fill up all of the required field" };
            }
            _context.SubjectStudent.Add(subjectStudent);
            _context.SaveChanges();
            List<int> ids = new List<int>() { subjectStudent.SubjectId, subjectStudent.StudentId };
            return new ActionStatusEntity { succeed = true, objectIds = ids };
        }

        //update
        public ActionStatusEntity Put(SubjectStudentEntity subjectStudent)
        {
            if (subjectStudent == null)
            {
                return new ActionStatusEntity { error = "please fill up all of the required field" };
            }
            if (!HasSubjectStudent())
            {
                return new ActionStatusEntity { error = "database has no subject with student" };
            }
            if (HasSubjectStudent(subjectStudent.SubjectId, subjectStudent.StudentId) == null)
            {
                return new ActionStatusEntity { error = "subject that has the specified student dont exist" };
            }
            _context.SubjectStudent.Update(subjectStudent);
            _context.SaveChanges();
            return new ActionStatusEntity { succeed = true };
        }

        //delete
        public ActionStatusEntity Delete(int subjectId, int studentId)
        {
            if (!HasSubjectStudent())
            {
                return new ActionStatusEntity { error = "database has no subject with student" };
            }

            SubjectStudentEntity? instance = HasSubjectStudent(subjectId, studentId);

            if (instance == null)
            {
                return new ActionStatusEntity { error = "subject that has the specified student dont exist" };
            }
            _context.SubjectStudent.Remove(instance);
            _context.SaveChanges();
            return new ActionStatusEntity { succeed = true };
        }

        public bool HasSubjectStudent()
        {
            if (_context.SubjectStudent is null)
                return false;
            return true;
        }

        public SubjectStudentEntity? HasSubjectStudent(int subjectId, int studentId)
        {
            SubjectStudentEntity? instance = _context.SubjectStudent.FirstOrDefault(x => x.SubjectId == subjectId && x.StudentId == studentId);
            return instance;
        }
    }
}
