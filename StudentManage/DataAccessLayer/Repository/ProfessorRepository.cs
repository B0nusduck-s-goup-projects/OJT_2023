using DataAccessLayer.Data;
using DataAccessLayer.ObjectEntity;
using DataAccessLayer.Repository.Interface;
using System.Xml.Linq;

namespace DataAccessLayer.Repository
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly ApplicationDbContext _context;
        public ProfessorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //get list
        public List<ProfessorEntity> Get()
        {
            List<ProfessorEntity> list = _context.Professors.Take(100).ToList();
            return list;
        }

        //get by name
        public List<ProfessorEntity> Get(string name)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            List<ProfessorEntity> list = _context.Professors.Where(p => p.FirstName.Contains(name) ||
                                                                        p.MiddleName.Contains(name) ||
                                                                        p.LastName.Contains(name))
                                                                        .Take(100).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return list;
        }

        //get by subject
        public ProfessorEntity? GetBySubject(int id)
        {
            ProfessorEntity? result = _context.Professors.FirstOrDefault(x => x.SubjectId == id);
            return result;
        }
        //get by id
        public ProfessorEntity? Get(int id)
        {
            return HasProfessor(id);
        }
        
        //get page
        public List<ProfessorEntity> Get(int pageNum, int pageLength)
        {
            List<ProfessorEntity> page = _context.Professors.Skip(pageNum*pageLength)
                                                            .Take(pageLength).ToList();
            return page;
        }

        //gat page by name
        public List<ProfessorEntity> Get(int pageNum, int pageLength, string name)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            List<ProfessorEntity> page = _context.Professors.Where(p => p.FirstName.Contains(name) ||
                                                                        p.MiddleName.Contains(name) ||
                                                                        p.LastName.Contains(name))
                                                                        .Skip(pageNum * pageLength)
                                                                        .Take(pageLength).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return page;
        }

        //add
        public ActionStatusEntity Post(ProfessorEntity professor)
        {
            if (professor == null)
            {
                return new ActionStatusEntity { error = "please fill up all of the required field"};
            }
            _context.Professors.Add(professor);
            _context.SaveChanges();
            return new ActionStatusEntity{ succeed = true };
        }

        //update
        public ActionStatusEntity Put(ProfessorEntity professor)
        {
            if (professor == null)
            {
                return new ActionStatusEntity { error = "please fill up all of the required field" };
            }
            if (!HasProfessor())
            {
                return new ActionStatusEntity { error = "database has no professor" };
            }
            if (HasProfessor(professor.Id) == null)
            {
                return new ActionStatusEntity { error = "professor dont exist" };
            }
            _context.Professors.Update(professor);
            _context.SaveChanges();
            return new ActionStatusEntity { succeed = true };
        }

        //delete
        public ActionStatusEntity Delete(int id)
        {
            if(!HasProfessor())
            {
                return new ActionStatusEntity { error = "database has no professor" };
            }

            ProfessorEntity? instance = HasProfessor(id);

            if(instance == null)
            {
                return new ActionStatusEntity { error = "professor dont exist" };
            }
            _context.Professors.Remove(instance);
            _context.SaveChanges();
            return new ActionStatusEntity { succeed = true };
        }

        public bool HasProfessor()
        {
            if (_context.Professors is null)
                return false;
            return true;
        }

        public ProfessorEntity? HasProfessor(int id)
        {
            ProfessorEntity? instance = _context.Professors.FirstOrDefault(x => x.Id == id);
            return instance;
        }
    }
}
