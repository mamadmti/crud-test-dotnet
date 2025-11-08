using Microsoft.EntityFrameworkCore;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Domain.Interfaces;
using Mc2.CrudTest.Domain.ValueObjects;
using Mc2.CrudTest.Infrastructure.Data;

namespace Mc2.CrudTest.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerDbContext _context;

    public CustomerRepository(CustomerDbContext context)
    {
        _context = context;
    }

    public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Customer?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(c => c.Email == email, cancellationToken);
    }

    public async Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Customers.ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string firstName, string lastName, DateTime dateOfBirth, CancellationToken cancellationToken = default)
    {
        return await _context.Customers
            .AnyAsync(c => c.FirstName == firstName 
                        && c.LastName == lastName 
                        && c.DateOfBirth == dateOfBirth.Date, 
                      cancellationToken);
    }

    public async Task<bool> EmailExistsAsync(Email email, Guid? excludeCustomerId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Customers.Where(c => c.Email == email);
        
        if (excludeCustomerId.HasValue)
        {
            query = query.Where(c => c.Id != excludeCustomerId.Value);
        }
        
        return await query.AnyAsync(cancellationToken);
    }

    public async Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        await _context.Customers.AddAsync(customer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var customer = await GetByIdAsync(id, cancellationToken);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

