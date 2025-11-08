using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Mc2.CrudTest.Contracts;

namespace Mc2.CrudTest.AcceptanceTests.Drivers;

public class CustomerApiDriver : IDisposable
{
    private readonly WebApplicationFactory<Mc2.CrudTest.Presentation.Program> _factory;
    private readonly HttpClient _client;

    public CustomerApiDriver()
    {
        _factory = new WebApplicationFactory<Mc2.CrudTest.Presentation.Program>();
        _client = _factory.CreateClient();
    }

    public async Task<HttpResponseMessage> CreateCustomerAsync(CustomerDto customer)
    {
        return await _client.PostAsJsonAsync("/api/customers", customer);
    }

    public async Task<HttpResponseMessage> UpdateCustomerAsync(Guid id, CustomerDto customer)
    {
        return await _client.PutAsJsonAsync($"/api/customers/{id}", customer);
    }

    public async Task<HttpResponseMessage> DeleteCustomerAsync(Guid id)
    {
        return await _client.DeleteAsync($"/api/customers/{id}");
    }

    public async Task<HttpResponseMessage> GetCustomerByIdAsync(Guid id)
    {
        return await _client.GetAsync($"/api/customers/{id}");
    }

    public async Task<HttpResponseMessage> GetAllCustomersAsync()
    {
        return await _client.GetAsync("/api/customers");
    }

    public async Task<HttpResponseMessage> GetCustomerByEmailAsync(string email)
    {
        return await _client.GetAsync($"/api/customers/by-email/{Uri.EscapeDataString(email)}");
    }

    public void Dispose()
    {
        _client?.Dispose();
        _factory?.Dispose();
    }
}

