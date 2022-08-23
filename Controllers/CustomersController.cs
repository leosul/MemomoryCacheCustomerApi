using MemoryCache.API.Interfaces;
using MemoryCache.API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MemoryCache.API.Controllers;


[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;
    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost("{numberOfCustomers:int}")]
    public IActionResult CreateCustomers(int numberOfCustomers)
    {
        _customerService.CreateCustomers(numberOfCustomers);

        return Ok($"Number of customers created: {numberOfCustomers}");
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetCustomerById(Guid id)
    {
        return Ok(_customerService.GetCustomerById(id));
    }

    [HttpGet]
    public IActionResult GetCustomers()
    {
        var watch = Stopwatch.StartNew();

        var customers = _customerService.GetCustomers();

        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;

        return Ok(new { ResponseTime = $"Response Time => {elapsedMs}", customers });
    }

    [HttpPost]
    public IActionResult AddCustomers(CustomerViewModel customerViewModel)
    {
        return Ok(_customerService.AddCustomer(Guid.NewGuid(), customerViewModel.Name));
    }

    [HttpDelete]
    public IActionResult RemoveCustomers(Guid id)
    {
        return Ok(_customerService.RemoveCustomer(id));
    }
}
