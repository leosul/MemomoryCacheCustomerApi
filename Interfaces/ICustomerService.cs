using MemoryCache.API.Model;

namespace MemoryCache.API.Interfaces;

public interface ICustomerService
{
    void CreateCustomers(int numberOfCustomers);
    IEnumerable<Customer> AddCustomer(Guid id, string name);
    Customer GetCustomerById(Guid id);
    IEnumerable<Customer> GetCustomers();
    bool RemoveCustomer(Guid id);
}
