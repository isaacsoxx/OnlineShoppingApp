using Microsoft.EntityFrameworkCore;
using Order.Domain.Models;

namespace Order.Application.Data;

/// <summary>
/// Represents abstraction layer for [ApplicationDbContext].
/// </summary>
public interface IApplicationDbContext
{
  DbSet<Customer> Customers { get; }
  DbSet<Product> Products { get; }
  DbSet<RootOrder> RootOrders { get; }
  DbSet<OrderItem> OrderItems { get; }

  Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}
