Feature: Customer Manager

As an operator I wish to be able to Create, Update, Delete customers and list all customers

@smoke
Scenario: Create a valid customer
	Given I have a valid customer with the following details:
		| Field              | Value                    |
		| FirstName          | John                     |
		| LastName           | Doe                      |
		| DateOfBirth        | 1990-01-15               |
		| PhoneNumber        | +14155552671             |
		| Email              | john.doe@example.com     |
		| BankAccountNumber  | GB82WEST12345698765432   |
	When I create the customer
	Then the customer should be created successfully
	And the customer should be retrievable by email

@validation
Scenario: Reject customer with invalid mobile phone number
	Given I have a customer with an invalid phone number "+1234"
	When I attempt to create the customer
	Then the creation should fail with validation error "Invalid mobile phone number"

@validation
Scenario: Reject customer with invalid email
	Given I have a customer with an invalid email "not-an-email"
	When I attempt to create the customer
	Then the creation should fail with validation error "Invalid email address"

@validation
Scenario: Reject customer with landline phone number
	Given I have a customer with a landline phone number "+442071234567"
	When I attempt to create the customer
	Then the creation should fail with validation error "Only mobile phone numbers are allowed"

@uniqueness
Scenario: Reject duplicate customer by name and date of birth
	Given I have created a customer with:
		| Field       | Value      |
		| FirstName   | Jane       |
		| LastName    | Smith      |
		| DateOfBirth | 1985-05-20 |
		| Email       | jane@example.com |
	When I attempt to create another customer with the same FirstName, LastName, and DateOfBirth
	Then the creation should fail with error "Customer already exists"

@uniqueness
Scenario: Reject duplicate email address
	Given I have created a customer with email "unique@example.com"
	When I attempt to create another customer with email "unique@example.com"
	Then the creation should fail with error "Email address already exists"

@crud
Scenario: Update customer information
	Given I have created a customer
	When I update the customer's phone number to "+14155559876"
	Then the customer should be updated successfully
	And the customer's phone number should be "+14155559876"

@crud
Scenario: Delete a customer
	Given I have created a customer
	When I delete the customer
	Then the customer should be deleted successfully
	And the customer should not be retrievable

@crud
Scenario: List all customers
	Given I have created 3 customers
	When I request all customers
	Then I should receive 3 customers