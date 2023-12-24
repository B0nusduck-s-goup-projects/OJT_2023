using DataAccessLayer.ObjectEntity;
using System.Diagnostics.SymbolStore;

namespace DataAccessLayer.Repository.Interface
{
    public interface IProfessorRepository
    {
        public List<ProfessorEntity> Get();
        public List<ProfessorEntity> Get(string name);
        public List<ProfessorEntity>? GetBySubject(int id);
        public ProfessorEntity? Get(int id);
        public List<ProfessorEntity> Get(int pageNum, int pageLength);
        public List<ProfessorEntity> Get(int pageNum, int pageLength, string name);
        public ActionStatusEntity Post(ProfessorEntity professor);
        public ActionStatusEntity Put(ProfessorEntity professor);
        public ActionStatusEntity Delete(int id);
        public bool HasProfessor();
        public ProfessorEntity? HasProfessor(int id);
    }
}
