//this libary is for returning the result of the sample function and can be safely remove along with the sample
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Service.Interface
{
    //this is a sample service interface used to conveniently creating new interface
    public interface IService
    {
        public DbContextId sample();
    }
}
