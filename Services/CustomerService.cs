using MemoryCache.API.Data;
using MemoryCache.API.Interfaces;
using MemoryCache.API.Model;
using Microsoft.Extensions.Caching.Memory;

namespace MemoryCache.API.Services;

public class CustomerService : ICustomerService
{
    public readonly MemoryDbContext _context;
    private readonly IMemoryCache _memoryCache;

    public CustomerService(MemoryDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
    }

    public void CreateCustomers(int numberOfCustomers)
    {
        var customers = new Customer(numberOfCustomers).Customers;

        _context.Customers.AddRange(customers);
        SaveChanges();
    }

    public IEnumerable<Customer> AddCustomer(Guid id, string name)
    {
        _context.Customers.Add(new Customer(id, name));
        SaveChanges();

        _memoryCache.Remove("customers");

        return _context.Customers.ToList();
    }

    public Customer GetCustomerById(Guid id)
    {
        return _context.Customers.FirstOrDefault(s => s.Id == id);
    }

    public IEnumerable<Customer> GetCustomers()
    {
        if (!_memoryCache.TryGetValue("customers", out IEnumerable<Customer> customers))
        {
            customers = _context.Customers.ToList();
            _memoryCache.Set("customers", customers, new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromHours(6))
                .SetSlidingExpiration(TimeSpan.FromMinutes(10)));
        }

        return customers;
    }

    public bool RemoveCustomer(Guid id)
    {
        var customer = _context.Customers.FirstOrDefault(s => s.Id == id);

        if (customer == null) return false;

        _context.Customers.Remove(customer);
        SaveChanges();

        _memoryCache.Remove("customers");

        return true;
    }

    private void SaveChanges()
    {
        _context.SaveChanges();
    }
}
