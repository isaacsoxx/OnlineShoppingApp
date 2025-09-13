namespace Order.Domain.Models;

/// <summary>
/// Represents the aggregate root for the order domain model, encapsulating all related entities and value objects.
///</summary>
public class RootOrder : Aggregate<OrderId>
{
  /* Aggregates. */
  private readonly List<OrderItem> _orderItems = new();
  public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

  /* Domain business properties. */
  public decimal TotalPrice
  {
    get => OrderItems.Sum(x => x.Price * x.Quantity);
    private set { }
  }
  public OrderStatus Status { get; private set; } = OrderStatus.Pending;

  /* Identifier value objects. */
  public CustomerId CustomerId { get; private set; } = default!;
  public OrderName OrderName { get; private set; } = default!;

  /* Descriptive value objects. */
  public Address ShippingAddress { get; private set; } = default!;
  public Address BillingAddress { get; private set; } = default!;
  public Payment Payment { get; private set; } = default!;

  /// <summary>
  /// Creates a new [RootOrder] aggregate with business domain rules enforced on value objects,
  /// and raises an [OrderCreatedEvent] to capture the operation as a domain event.
  /// </summary>
  /// <returns>A fully initialized Order aggregate root.</returns>
  public static RootOrder Create(OrderId id, CustomerId customerId, OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment)
  {
    var order = new RootOrder
    {
      Id = id,
      CustomerId = customerId,
      OrderName = orderName,
      ShippingAddress = shippingAddress,
      BillingAddress = billingAddress,
      Payment = payment,
      Status = OrderStatus.Pending
    };
    order.AddDomainEvent(new OrderCreatedEvent(order));
    return order;
  }

  /// <summary>
  /// Updates an existing [RootOrder] aggregate with business domain rules enforced on value objects,
  /// and raises an [OrderUpdatedEvent] to capture the operation as a domain event.
  /// </summary>
  public void Update(OrderId id, CustomerId customerId, OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment)
  {
    OrderName = orderName;
    ShippingAddress = shippingAddress;
    BillingAddress = billingAddress;
    Payment = payment;
    Status = OrderStatus.Pending;

    AddDomainEvent(new OrderUpdatedEvent(this));
  }

  /// <summary>
  /// Adds and creates a new [OrderItem] to the [RootOrder] collection, enforcing business domain rules.
  /// </summary>
  public void Add(ProductId productId, int quantity, decimal price)
  {
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

    var orderItem = new OrderItem(Id, productId, quantity, price);
    _orderItems.Add(orderItem);
  }

  /// <summary>
  /// Removes an existing [OrderItem] to the [RootOrder] collection, enforcing business domain rules.
  /// </summary>
  public void Remove(ProductId productId)
  {
    var orderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);
    if (orderItem is not null) _orderItems.Remove(orderItem);
  }
}
