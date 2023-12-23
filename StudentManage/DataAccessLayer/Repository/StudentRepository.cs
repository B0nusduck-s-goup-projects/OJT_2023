using DataAccessLayer.Data;
using DataAccessLayer.ObjectEntity;
using DataAccessLayer.Repository.Interface;

namespace DataAccessLayer.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //get list
        public List<StudentEntity> Get()
        {
            List<StudentEntity> list;
            list = _context.Students.Take(100).ToList();
            return list;
        }

        //get by name
        public List<StudentEntity> Get(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new List<StudentEntity>();
            }
            List<StudentEntity> list = _context.Students.Where(p => p.FirstName.Contains(name) ||
                                                                    p.MiddleName.Contains(name) ||
                                                                    p.LastName.Contains(name))
                                                                    .Take(100).ToList();
            if (list == null || list.Count == 0)
            {
                return new List<StudentEntity>();
            }
            return list;
        }

        //get by id
        public StudentEntity? Get(int id)
        {
            return HasStudent(id);
        }
        
        //get page
        public List<StudentEntity> Get(int pageNum, int pageLength)
        {
            List<StudentEntity> page = _context.Students.Skip(pageNum*pageLength)
                                                        .Take(pageLength)
                                                        .ToList();
            return page;
        }

        //get page by name
        public List<StudentEntity> Get(int pageNum, int pageLength,string name)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            List<StudentEntity> page = _context.Students.Where(p => p.FirstName.Contains(name) ||
                                                                    p.MiddleName.Contains(name) ||
                                                                    p.LastName.Contains(name))
                                                                    .Skip(pageNum * pageLength)
                                                                    .Take(pageLength).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return page;
        }

        //add
        public ActionStatusEntity Post(StudentEntity student)
        {
            if (student == null)
            {
                return new ActionStatusEntity { error = "please fill up all of the required field"};
            }
            _context.Students.Add(student);
            _context.SaveChanges();
            List<int> ids = new List<int>() { student.Id };
            return new ActionStatusEntity{ succeed = true, objectIds = ids };
        }

        //update
        public ActionStatusEntity Put(StudentEntity student)
        {
            if (student == null)
            {
                return new ActionStatusEntity { error = "please fill up all of the required field" };
            }
            if (!HasStudent())
            {
                return new ActionStatusEntity { error = "database has no student" };
            }
            if (HasStudent(student.Id) == null)
            {
                return new ActionStatusEntity { error = "student dont exist" };
            }
            _context.Students.Update(student);
            _context.SaveChanges();
            return new ActionStatusEntity { succeed = true };
        }

        //delete
        public ActionStatusEntity Delete(int id)
        {
            if(!HasStudent())
            {
                return new ActionStatusEntity { error = "database has no student" };
            }

            StudentEntity? instance = HasStudent(id);

            if(instance == null)
            {
                return new ActionStatusEntity { error = "student dont exist" };
            }
            _context.Students.Remove(instance);
            _context.SaveChanges();
            return new ActionStatusEntity { succeed = true };
        }

        public bool HasStudent()
        {
            if (_context.Students is null)
                return false;
            return true;
        }

        public StudentEntity? HasStudent(int id)
        {
            StudentEntity? instance = _context.Students.FirstOrDefault(x => x.Id == id);
            return instance;
        }
    }
}
