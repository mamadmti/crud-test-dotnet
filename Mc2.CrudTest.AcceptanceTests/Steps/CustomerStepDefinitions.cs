using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Mc2.CrudTest.AcceptanceTests.Steps;

[Binding]
public class CustomerStepDefinitions
{
    private readonly ScenarioContext _scenarioContext;
    private object? _customerRequest;
    private object? _customerResponse;
    private Exception? _lastException;

    public CustomerStepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    #region Given Steps

    [Given(@"I have a valid customer with the following details:")]
    public void GivenIHaveAValidCustomerWithTheFollowingDetails(Table table)
    {
        // var customerData = table.CreateInstance<CustomerDto>();
        // _customerRequest = customerData;

        _scenarioContext.Pending();
    }

    [Given(@"I have a customer with an invalid phone number ""(.*)""")]
    public void GivenIHaveACustomerWithAnInvalidPhoneNumber(string phoneNumber)
    {

        _scenarioContext.Pending();
    }

    [Given(@"I have a customer with an invalid email ""(.*)""")]
    public void GivenIHaveACustomerWithAnInvalidEmail(string email)
    {

        _scenarioContext.Pending();
    }

    [Given(@"I have a customer with a landline phone number ""(.*)""")]
    public void GivenIHaveACustomerWithALandlinePhoneNumber(string phoneNumber)
    {

        _scenarioContext.Pending();
    }

    [Given(@"I have created a customer with:")]
    public void GivenIHaveCreatedACustomerWith(Table table)
    {

        _scenarioContext.Pending();
    }

    [Given(@"I have created a customer with email ""(.*)""")]
    public void GivenIHaveCreatedACustomerWithEmail(string email)
    {

        _scenarioContext.Pending();
    }

    [Given(@"I have created a customer")]
    public void GivenIHaveCreatedACustomer()
    {

        _scenarioContext.Pending();
    }

    [Given(@"I have created (.*) customers")]
    public void GivenIHaveCreatedMultipleCustomers(int count)
    {

        _scenarioContext.Pending();
    }

    #endregion

    #region When Steps

    [When(@"I create the customer")]
    public void WhenICreateTheCustomer()
    {
        // Use HTTP client to POST to /api/customers

        _scenarioContext.Pending();
    }

    [When(@"I attempt to create the customer")]
    public void WhenIAttemptToCreateTheCustomer()
    {
        try
        {
            // Store exception for later assertion

            _scenarioContext.Pending();
        }
        catch (Exception ex)
        {
            _lastException = ex;
        }
    }

    [When(@"I attempt to create another customer with the same FirstName, LastName, and DateOfBirth")]
    public void WhenIAttemptToCreateAnotherCustomerWithTheSameDetails()
    {
        try
        {

            _scenarioContext.Pending();
        }
        catch (Exception ex)
        {
            _lastException = ex;
        }
    }

    [When(@"I attempt to create another customer with email ""(.*)""")]
    public void WhenIAttemptToCreateAnotherCustomerWithEmail(string email)
    {
        try
        {

            _scenarioContext.Pending();
        }
        catch (Exception ex)
        {
            _lastException = ex;
        }
    }

    [When(@"I update the customer's phone number to ""(.*)""")]
    public void WhenIUpdateTheCustomersPhoneNumberTo(string phoneNumber)
    {
        // Use HTTP client to PUT to /api/customers/{id}

        _scenarioContext.Pending();
    }

    [When(@"I delete the customer")]
    public void WhenIDeleteTheCustomer()
    {
        // Use HTTP client to DELETE to /api/customers/{id}

        _scenarioContext.Pending();
    }

    [When(@"I request all customers")]
    public void WhenIRequestAllCustomers()
    {
        // Use HTTP client to GET from /api/customers

        _scenarioContext.Pending();
    }

    #endregion

    #region Then Steps

    [Then(@"the customer should be created successfully")]
    public void ThenTheCustomerShouldBeCreatedSuccessfully()
    {
        // _customerResponse.Should().NotBeNull();

        _scenarioContext.Pending();
    }

    [Then(@"the customer should be retrievable by email")]
    public void ThenTheCustomerShouldBeRetrievableByEmail()
    {
        // Assert customer exists and matches expected data

        _scenarioContext.Pending();
    }

    [Then(@"the creation should fail with validation error ""(.*)""")]
    public void ThenTheCreationShouldFailWithValidationError(string expectedError)
    {
        // _lastException.Should().NotBeNull();
        // errorMessage.Should().Contain(expectedError);

        _scenarioContext.Pending();
    }

    [Then(@"the creation should fail with error ""(.*)""")]
    public void ThenTheCreationShouldFailWithError(string expectedError)
    {
        // _lastException.Should().NotBeNull();

        _scenarioContext.Pending();
    }

    [Then(@"the customer should be updated successfully")]
    public void ThenTheCustomerShouldBeUpdatedSuccessfully()
    {

        _scenarioContext.Pending();
    }

    [Then(@"the customer's phone number should be ""(.*)""")]
    public void ThenTheCustomersPhoneNumberShouldBe(string expectedPhoneNumber)
    {

        _scenarioContext.Pending();
    }

    [Then(@"the customer should be deleted successfully")]
    public void ThenTheCustomerShouldBeDeletedSuccessfully()
    {

        _scenarioContext.Pending();
    }

    [Then(@"the customer should not be retrievable")]
    public void ThenTheCustomerShouldNotBeRetrievable()
    {

        _scenarioContext.Pending();
    }

    [Then(@"I should receive (.*) customers")]
    public void ThenIShouldReceiveCustomers(int expectedCount)
    {
        // customers.Should().HaveCount(expectedCount);

        _scenarioContext.Pending();
    }

    #endregion
}

