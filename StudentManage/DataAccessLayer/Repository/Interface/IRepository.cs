//this libary is for returning the result of the sample function and can be safely remove along with the sample
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.Interface
{
    //this is a sample repository interface used to conveniently creating new interface
    public interface IRepository
    {
        public DbContextId sample();
    }
}
