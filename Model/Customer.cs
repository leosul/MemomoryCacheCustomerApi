using System.Text.Json.Serialization;

namespace MemoryCache.API.Model;

public class Customer
{
    public Customer(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Customer(int numberOfCustomers)
    {
        Customers = new List<Customer>();

        for (int i = 0; i < numberOfCustomers; i++)
            Customers.Add(new Customer(Guid.NewGuid(), $"Customer_{i}"));
    }

    [JsonIgnore]
    public List<Customer> Customers { get; set; }
    public Guid Id { get; set; }
    public string Name { get; set; }
}