using DataAccessLayer.Data;
using DataAccessLayer.Repository.Interface;
//this libary is for returning the result of the sample function and can be safely remove along with the sample
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    //this is a sample repository used to conveniently creating new repository
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public DbContextId sample()
        {
            return _context.ContextId;
        }
    }
}
