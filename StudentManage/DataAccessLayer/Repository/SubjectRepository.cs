using DataAccessLayer.Data;
using DataAccessLayer.ObjectEntity;
using DataAccessLayer.Repository.Interface;

namespace DataAccessLayer.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext _context;
        public SubjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //get list
        public List<SubjectEntity> Get()
        {
            List<SubjectEntity> list = _context.Subjects.Take(100).ToList();
            return list;
        }

        //get by name
        public List<SubjectEntity> Get(string name)
        {
            List<SubjectEntity> list = _context.Subjects.Where(p => p.Name.Contains(name))
                                                        .Take(100).ToList();
            return list;
        }

        //get by id        
        public SubjectEntity? Get(int id)
        {
            return HasSubject(id);
        }

        //get page
        public List<SubjectEntity> Get(int pageNum, int pageLength)
        {
            List<SubjectEntity> page = _context.Subjects.Skip(pageNum * pageLength)
                                                        .Take(pageLength).ToList();
            return page;
        }

        //get page by name
        public List<SubjectEntity> Get(int pageNum, int pageLength, string name)
        {
            List<SubjectEntity> page = _context.Subjects.Where(p => p.Name.Contains(name))
                                                        .Skip(pageNum * pageLength)
                                                        .Take(pageLength).ToList();
            return page;
        }

        //add
        public ActionStatusEntity Post(SubjectEntity subject)
        {
            if (subject == null)
            {
                return new ActionStatusEntity { error = "please fill up all of the required field" };
            }
            _context.Subjects.Add(subject);
            _context.SaveChanges();
            return new ActionStatusEntity { succeed = true };
        }

        //update
        public ActionStatusEntity Put(SubjectEntity subject)
        {
            if (subject == null)
            {
                return new ActionStatusEntity { error = "please fill up all of the required field" };
            }
            if (!HasSubject())
            {
                return new ActionStatusEntity { error = "database has no subject" };
            }
            if (HasSubject(subject.Id) == null)
            {
                return new ActionStatusEntity { error = "subject dont exist" };
            }
            _context.Subjects.Update(subject);
            _context.SaveChanges();
            return new ActionStatusEntity { succeed = true };
        }

        //delete
        public ActionStatusEntity Delete(int id)
        {
            if (!HasSubject())
            {
                return new ActionStatusEntity { error = "database has no subject" };
            }

            SubjectEntity? instance = HasSubject(id);

            if (instance == null)
            {
                return new ActionStatusEntity { error = "subject dont exist" };
            }
            _context.Subjects.Remove(instance);
            _context.SaveChanges();
            return new ActionStatusEntity { succeed = true };
        }

        public bool HasSubject()
        {
            if (_context.Subjects is null)
                return false;
            return true;
        }

        public SubjectEntity? HasSubject(int id)
        {
            SubjectEntity? instance = _context.Subjects.FirstOrDefault(x => x.Id == id);
            return instance;
        }
    }
}
