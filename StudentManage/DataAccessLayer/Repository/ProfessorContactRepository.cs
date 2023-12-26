using DataAccessLayer.Data;
using DataAccessLayer.ObjectEntity;
using DataAccessLayer.Repository.Interface;

namespace DataAccessLayer.Repository
{
    public class ProfessorContactRepository : IProfessorContactRepository
    {
        private readonly ApplicationDbContext _context;
        public ProfessorContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        //get list
        public List<ProfessorContactEntity> Get()
        {
            List<ProfessorContactEntity> list;
            list = _context.PContacts.ToList();
            return list;
        }

        //get by user
        public ProfessorContactEntity? GetByUser(int id)
        {
            ProfessorContactEntity? result = _context.PContacts.FirstOrDefault(pc => pc.UserId == id);
            return result;
        }

        //get by id
        public ProfessorContactEntity? Get(int id)
        {
            return HasContact(id);
        }
        
        //get page
        public List<ProfessorContactEntity> Get(int pageNum, int pageLength)
        {
            List<ProfessorContactEntity> page = _context.PContacts.Skip(pageNum*pageLength)
                                                                    .Take(pageLength).ToList();
            return page;
        }

        //add
        public ActionStatusEntity Post(ProfessorContactEntity contact)
        {
            if (contact == null)
            {
                return new ActionStatusEntity { error = "please fill up all of the required field"};
            }
            if(GetByUser(contact.UserId) != null)
            {
                return new ActionStatusEntity { error = "contact for this professor has already exist" };
            }
            
            _context.PContacts.Add(contact);
            _context.SaveChanges();
            List<int> ids = new List<int>(){contact.Id};
            return new ActionStatusEntity{ succeed = true, objectIds = ids};
        }

        //update
        public ActionStatusEntity Put(ProfessorContactEntity contact)
        {
            if (contact == null)
            {
                return new ActionStatusEntity { error = "please fill up all of the required field" };
            }
            if(!HasContact())
            {
                return new ActionStatusEntity { error = "database has no contact to any professor" };
            }
            if(HasContact(contact.Id) == null)
            {
                return new ActionStatusEntity { error = "database has no contact with this id" };
            }
            _context.PContacts.Update(contact);
            _context.SaveChanges();
            return new ActionStatusEntity { succeed = true };
        }

        //delete
        public ActionStatusEntity Delete(int id)
        {
            if(!HasContact())
            {
                return new ActionStatusEntity { error = "database has no professor contact" };
            }

            ProfessorContactEntity? instance = HasContact(id);

            if(instance == null)
            {
                return new ActionStatusEntity { error = "contact for this professor dont exist" };
            }
            _context.PContacts.Remove(instance);
            _context.SaveChanges();
            return new ActionStatusEntity { succeed = true };
        }

        public bool HasContact()
        {
            if (_context.PContacts is null)
                return false;
            return true;
        }

        public ProfessorContactEntity? HasContact(int id)
        {
            ProfessorContactEntity? instance = _context.PContacts.FirstOrDefault(x => x.Id == id);
            return instance;
        }
    }
}
