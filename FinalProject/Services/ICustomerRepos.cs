using FinalProject.Areas.Admin.Models;

namespace FinalProject.Services
{
    public interface ICustomerRepos
    {
        public List<Customer> GetAll();
        public Customer GetDetails(int id);
        public void Insert(Customer Customer);
        public void UpdateCust(int id, Customer Customer);
        public void DeleteCust(int id);
    }
}
