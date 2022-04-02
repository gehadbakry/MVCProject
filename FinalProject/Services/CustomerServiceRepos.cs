using FinalProject.Areas.Admin.Models;
using FinalProject.Models;

namespace FinalProject.Services
{
    public class CustomerServiceRepos:ICustomerRepos
    {
        public PurchaseDbContext Context { get; set; }

        public CustomerServiceRepos(PurchaseDbContext context)
        {
            Context = context;
        }

        public List<Customer> GetAll()
        {
            return Context.Customers.ToList();
        }

        public Customer GetDetails(int id)
        {
            return Context.Customers.Find(id);
        }

        public void Insert(Customer Customer)
        {
            Context.Customers.Add(Customer);
            Context.SaveChanges();
        }

        public void UpdateCust(int id, Customer Customer)
        {
            Customer CustUpdated = Context.Customers.Find(id);
            CustUpdated.FirstName = Customer.FirstName;
            CustUpdated.LastName = Customer.LastName;
            CustUpdated.Email = Customer.Email;
            CustUpdated.Address = Customer.Address;
            CustUpdated.PhoneNumber = Customer.PhoneNumber;
            CustUpdated.Password = Customer.Password;

            Context.SaveChanges();
        }
        public void DeleteCust(int id)
        {
            Context.Remove(Context.Customers.Find(id));
            Context.SaveChanges();
        }

    }
}
