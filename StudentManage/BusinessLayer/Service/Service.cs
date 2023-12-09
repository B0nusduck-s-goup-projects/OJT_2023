using BusinessLayer.Service.Interface;
using DataAccessLayer.Repository.Interface;
//this libary is for returning the result of the sample function and can be safely remove along with the sample
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Service
{
    //this is a sample service used to conveniently creating new service
    public class Service : IService
    {
        private readonly IRepository _repository;
        public Service(IRepository repository)
        {
            _repository = repository;
        }
        
        public DbContextId sample()
        {
            return _repository.sample();
        }
    }
}
