using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Mc2.CrudTest.Contracts;
using Mc2.CrudTest.AcceptanceTests.Drivers;
using System.Net.Http.Json;

namespace Mc2.CrudTest.AcceptanceTests.Steps;

[Binding]
public class CustomerStepDefinitions : IDisposable
{
    private readonly ScenarioContext _scenarioContext;
    private CustomerApiDriver? _apiDriver;
    private CustomerDto? _customerRequest;
    private HttpResponseMessage? _lastResponse;
    private List<CustomerDto> _createdCustomers = new();

    public CustomerStepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    private CustomerApiDriver ApiDriver => _apiDriver ??= new CustomerApiDriver();

    #region Given Steps

    [Given(@"I have a valid customer with the following details:")]
    public void GivenIHaveAValidCustomerWithTheFollowingDetails(Table table)
    {
        var dict = new Dictionary<string, string>();
        foreach (var row in table.Rows)
        {
            dict[row["Field"]] = row["Value"];
        }

        _customerRequest = new CustomerDto
        {
            FirstName = dict["FirstName"],
            LastName = dict["LastName"],
            DateOfBirth = DateTime.Parse(dict["DateOfBirth"]),
            PhoneNumber = dict["PhoneNumber"],
            Email = dict["Email"],
            BankAccountNumber = dict["BankAccountNumber"]
        };
    }

    [Given(@"I have a customer with an invalid phone number ""(.*)""")]
    public void GivenIHaveACustomerWithAnInvalidPhoneNumber(string phoneNumber)
    {
        _customerRequest = new CustomerDto
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 15),
            PhoneNumber = phoneNumber,
            Email = "test@example.com",
            BankAccountNumber = "GB82WEST12345698765432"
        };
    }

    [Given(@"I have a customer with an invalid email ""(.*)""")]
    public void GivenIHaveACustomerWithAnInvalidEmail(string email)
    {
        _customerRequest = new CustomerDto
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 15),
            PhoneNumber = "+14155552671",
            Email = email,
            BankAccountNumber = "GB82WEST12345698765432"
        };
    }

    [Given(@"I have a customer with a landline phone number ""(.*)""")]
    public void GivenIHaveACustomerWithALandlinePhoneNumber(string phoneNumber)
    {
        _customerRequest = new CustomerDto
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 15),
            PhoneNumber = phoneNumber,
            Email = "test@example.com",
            BankAccountNumber = "GB82WEST12345698765432"
        };
    }

    [Given(@"I have created a customer with:")]
    public async Task GivenIHaveCreatedACustomerWith(Table table)
    {
        var dict = new Dictionary<string, string>();
        foreach (var row in table.Rows)
        {
            dict[row["Field"]] = row["Value"];
        }

        var customer = new CustomerDto
        {
            FirstName = dict["FirstName"],
            LastName = dict["LastName"],
            DateOfBirth = DateTime.Parse(dict["DateOfBirth"]),
            PhoneNumber = "+14155552671",
            Email = dict["Email"],
            BankAccountNumber = "GB82WEST12345698765432"
        };

        var response = await ApiDriver.CreateCustomerAsync(customer);
        response.EnsureSuccessStatusCode();

        var created = await response.Content.ReadFromJsonAsync<CustomerDto>();
        _createdCustomers.Add(created!);
        _customerRequest = customer;
    }

    [Given(@"I have created a customer with email ""(.*)""")]
    public async Task GivenIHaveCreatedACustomerWithEmail(string email)
    {
        var customer = new CustomerDto
        {
            FirstName = "Existing",
            LastName = "Customer",
            DateOfBirth = new DateTime(1990, 1, 15),
            PhoneNumber = "+14155552671",
            Email = email,
            BankAccountNumber = "GB82WEST12345698765432"
        };

        var response = await ApiDriver.CreateCustomerAsync(customer);
        response.EnsureSuccessStatusCode();

        var created = await response.Content.ReadFromJsonAsync<CustomerDto>();
        _createdCustomers.Add(created!);
    }

    [Given(@"I have created a customer")]
    public async Task GivenIHaveCreatedACustomer()
    {
        var customer = new CustomerDto
        {
            FirstName = "Test",
            LastName = "Customer",
            DateOfBirth = new DateTime(1990, 1, 15),
            PhoneNumber = "+14155552671",
            Email = $"test{Guid.NewGuid()}@example.com",
            BankAccountNumber = "GB82WEST12345698765432"
        };

        var response = await ApiDriver.CreateCustomerAsync(customer);
        response.EnsureSuccessStatusCode();

        var created = await response.Content.ReadFromJsonAsync<CustomerDto>();
        _createdCustomers.Add(created!);
    }

    [Given(@"I have created (.*) customers")]
    public async Task GivenIHaveCreatedMultipleCustomers(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var customer = new CustomerDto
            {
                FirstName = $"Customer{i}",
                LastName = $"Test{i}",
                DateOfBirth = new DateTime(1990, 1, 15).AddDays(i),
                PhoneNumber = "+14155552671",
                Email = $"customer{i}@example.com",
                BankAccountNumber = "GB82WEST12345698765432"
            };

            var response = await ApiDriver.CreateCustomerAsync(customer);
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<CustomerDto>();
            _createdCustomers.Add(created!);
        }
    }

    #endregion

    #region When Steps

    [When(@"I create the customer")]
    public async Task WhenICreateTheCustomer()
    {
        _lastResponse = await ApiDriver.CreateCustomerAsync(_customerRequest!);
    }

    [When(@"I attempt to create the customer")]
    public async Task WhenIAttemptToCreateTheCustomer()
    {
        _lastResponse = await ApiDriver.CreateCustomerAsync(_customerRequest!);
    }

    [When(@"I attempt to create another customer with the same FirstName, LastName, and DateOfBirth")]
    public async Task WhenIAttemptToCreateAnotherCustomerWithTheSameDetails()
    {
        var duplicateCustomer = new CustomerDto
        {
            FirstName = _customerRequest!.FirstName,
            LastName = _customerRequest.LastName,
            DateOfBirth = _customerRequest.DateOfBirth,
            PhoneNumber = "+14155559999",
            Email = "different@example.com",
            BankAccountNumber = "FR1420041010050500013M02606"
        };

        _lastResponse = await ApiDriver.CreateCustomerAsync(duplicateCustomer);
    }

    [When(@"I attempt to create another customer with email ""(.*)""")]
    public async Task WhenIAttemptToCreateAnotherCustomerWithEmail(string email)
    {
        var customer = new CustomerDto
        {
            FirstName = "Different",
            LastName = "Person",
            DateOfBirth = new DateTime(1995, 6, 10),
            PhoneNumber = "+14155559999",
            Email = email,
            BankAccountNumber = "FR1420041010050500013M02606"
        };

        _lastResponse = await ApiDriver.CreateCustomerAsync(customer);
    }

    [When(@"I update the customer's phone number to ""(.*)""")]
    public async Task WhenIUpdateTheCustomersPhoneNumberTo(string phoneNumber)
    {
        var customer = _createdCustomers.Last();
        customer.PhoneNumber = phoneNumber;

        _lastResponse = await ApiDriver.UpdateCustomerAsync(customer.Id, customer);
    }

    [When(@"I delete the customer")]
    public async Task WhenIDeleteTheCustomer()
    {
        var customerId = _createdCustomers.Last().Id;
        _lastResponse = await ApiDriver.DeleteCustomerAsync(customerId);
    }

    [When(@"I request all customers")]
    public async Task WhenIRequestAllCustomers()
    {
        _lastResponse = await ApiDriver.GetAllCustomersAsync();
    }

    #endregion

    #region Then Steps

    [Then(@"the customer should be created successfully")]
    public void ThenTheCustomerShouldBeCreatedSuccessfully()
    {
        _lastResponse.Should().NotBeNull();
        _lastResponse!.IsSuccessStatusCode.Should().BeTrue();
    }

    [Then(@"the customer should be retrievable by email")]
    public async Task ThenTheCustomerShouldBeRetrievableByEmail()
    {
        var response = await ApiDriver.GetCustomerByEmailAsync(_customerRequest!.Email);
        response.IsSuccessStatusCode.Should().BeTrue();

        var customer = await response.Content.ReadFromJsonAsync<CustomerDto>();
        customer.Should().NotBeNull();
        customer!.Email.Should().Be(_customerRequest.Email.ToLowerInvariant());
    }

    [Then(@"the creation should fail with validation error ""(.*)""")]
    public async Task ThenTheCreationShouldFailWithValidationError(string expectedError)
    {
        _lastResponse.Should().NotBeNull();
        _lastResponse!.IsSuccessStatusCode.Should().BeFalse();

        var content = await _lastResponse.Content.ReadAsStringAsync();
        content.ToLowerInvariant().Should().Contain(expectedError.ToLowerInvariant());
    }

    [Then(@"the creation should fail with error ""(.*)""")]
    public async Task ThenTheCreationShouldFailWithError(string expectedError)
    {
        _lastResponse.Should().NotBeNull();
        _lastResponse!.IsSuccessStatusCode.Should().BeFalse();

        var content = await _lastResponse.Content.ReadAsStringAsync();
        content.ToLowerInvariant().Should().Contain(expectedError.ToLowerInvariant());
    }

    [Then(@"the customer should be updated successfully")]
    public void ThenTheCustomerShouldBeUpdatedSuccessfully()
    {
        _lastResponse.Should().NotBeNull();
        _lastResponse!.IsSuccessStatusCode.Should().BeTrue();
    }

    [Then(@"the customer's phone number should be ""(.*)""")]
    public async Task ThenTheCustomersPhoneNumberShouldBe(string expectedPhoneNumber)
    {
        var customerId = _createdCustomers.Last().Id;
        var response = await ApiDriver.GetCustomerByIdAsync(customerId);
        response.IsSuccessStatusCode.Should().BeTrue();

        var customer = await response.Content.ReadFromJsonAsync<CustomerDto>();
        customer.Should().NotBeNull();
        customer!.PhoneNumber.Should().Be(expectedPhoneNumber);
    }

    [Then(@"the customer should be deleted successfully")]
    public void ThenTheCustomerShouldBeDeletedSuccessfully()
    {
        _lastResponse.Should().NotBeNull();
        _lastResponse!.IsSuccessStatusCode.Should().BeTrue();
    }

    [Then(@"the customer should not be retrievable")]
    public async Task ThenTheCustomerShouldNotBeRetrievable()
    {
        var customerId = _createdCustomers.Last().Id;
        var response = await ApiDriver.GetCustomerByIdAsync(customerId);
        response.IsSuccessStatusCode.Should().BeFalse();
    }

    [Then(@"I should receive (.*) customers")]
    public async Task ThenIShouldReceiveCustomers(int expectedCount)
    {
        _lastResponse.Should().NotBeNull();
        _lastResponse!.IsSuccessStatusCode.Should().BeTrue();

        var customers = await _lastResponse.Content.ReadFromJsonAsync<List<CustomerDto>>();
        customers.Should().NotBeNull();
        customers!.Count.Should().BeGreaterOrEqualTo(expectedCount);
    }

    #endregion

    public void Dispose()
    {
        _apiDriver?.Dispose();
    }
}
