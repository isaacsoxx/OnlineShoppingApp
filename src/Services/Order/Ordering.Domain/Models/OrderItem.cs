namespace Order.Domain.Models;

/// <summary>
/// Represents an individual item within an [RootOrder], containing order business domain properties.
/// </summary>
public class OrderItem : Entity<OrderItemId>
{
  public OrderId OrderId { get; private set; }
  public ProductId ProductId { get; private set; }
  public int Quantity { get; private set; } = default!;
  public decimal Price { get; private set; } = default!;
  
  internal OrderItem(OrderId orderId, ProductId productId, int quantity, decimal price)
  {
    Id = OrderItemId.Of(Guid.NewGuid());
    OrderId = orderId;
    ProductId = productId;
    Quantity = quantity;
    Price = price;
  }

}
