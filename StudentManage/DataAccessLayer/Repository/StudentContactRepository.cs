using DataAccessLayer.Data;
using DataAccessLayer.ObjectEntity;
using DataAccessLayer.Repository.Interface;

namespace DataAccessLayer.Repository
{
    public class StudentContactRepository : IStudentContactRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        //get list
        public List<StudentContactEntity> Get()
        {
            List<StudentContactEntity> list;
            list = _context.SContacts.Take(100).ToList();
            return list;
        }

        //get by user
        public StudentContactEntity? GetByUser(int id)
        {
            StudentContactEntity? result = _context.SContacts.FirstOrDefault(sc => sc.UserId == id);
            return result;
        }

        //get by id
        public StudentContactEntity? Get(int id)
        {
            return HasContact(id);
        }
        
        //get page
        public List<StudentContactEntity> Get(int pageNum, int pageLength)
        {
            List<StudentContactEntity> page = _context.SContacts.Skip(pageNum*pageLength)
                                                                    .Take(pageLength).ToList();
            return page;
        }

        //add
        public ActionStatusEntity Post(StudentContactEntity studentContact)
        {
            if (studentContact == null)
            {
                return new ActionStatusEntity { error = "please fill up all of the required field"};
            }
            if(GetByUser(studentContact.UserId) != null)
            {
                return new ActionStatusEntity { error = "contact for this student has already exist" };
            }
            
            _context.SContacts.Add(studentContact);
            _context.SaveChanges();
            return new ActionStatusEntity{ succeed = true };
        }

        //update
        public ActionStatusEntity Put(StudentContactEntity studentContact)
        {
            if (studentContact == null)
            {
                return new ActionStatusEntity { error = "please fill up all of the required field" };
            }
            if(!HasContact())
            {
                return new ActionStatusEntity { error = "database has no contact to any student" };
            }
            if(HasContact(studentContact.Id) == null)
            {
                return new ActionStatusEntity { error = "database has no contact with this id" };
            }
            _context.SContacts.Update(studentContact);
            _context.SaveChanges();
            return new ActionStatusEntity { succeed = true };
        }

        //delete
        public ActionStatusEntity Delete(int id)
        {
            if(!HasContact())
            {
                return new ActionStatusEntity { error = "database has no student contact" };
            }

            StudentContactEntity? instance = HasContact(id);

            if(instance == null)
            {
                return new ActionStatusEntity { error = "contact for this student dont exist" };
            }
            _context.SContacts.Remove(instance);
            _context.SaveChanges();
            return new ActionStatusEntity { succeed = true };
        }

        public bool HasContact()
        {
            if (_context.SContacts is null)
                return false;
            return true;
        }

        public StudentContactEntity? HasContact(int id)
        {
            StudentContactEntity? instance = _context.SContacts.FirstOrDefault(x => x.Id == id);
            return instance;
        }
    }
}
