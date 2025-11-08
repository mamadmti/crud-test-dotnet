using System.Net.Http.Json;
using Mc2.CrudTest.Contracts;

namespace Mc2.CrudTest.Presentation.Client.Services;

public class CustomerService
{
    private readonly HttpClient _httpClient;

    public CustomerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CustomerDto>> GetAllCustomersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<CustomerDto>>("api/customers") ?? new List<CustomerDto>();
    }

    public async Task<CustomerDto?> GetCustomerByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<CustomerDto>($"api/customers/{id}");
    }

    public async Task<CustomerDto> CreateCustomerAsync(CustomerDto customer)
    {
        var response = await _httpClient.PostAsJsonAsync("api/customers", customer);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CustomerDto>() ?? throw new Exception("Failed to create customer");
    }

    public async Task<CustomerDto> UpdateCustomerAsync(Guid id, CustomerDto customer)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/customers/{id}", customer);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CustomerDto>() ?? throw new Exception("Failed to update customer");
    }

    public async Task DeleteCustomerAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/customers/{id}");
        response.EnsureSuccessStatusCode();
    }
}

