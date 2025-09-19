using Order.Application.DTOs;

namespace Order.Application.Orders.Commands.CreateOrder;

/// <summary>
/// Represents command to trigger create order action.
/// </summary>
/// <param name="Order">Order to be created.</param>
public record CreateOrderCommand(RootOrderDto Order) : ICommand<CreateOrderResult>;

/// <summary>
/// Represents the result response to the create order action.
/// </summary>
/// <param name="Id">Id or the created order.</param>
/// <returns></returns>
public record CreateOrderResult(Guid Id);

/// <summary>
/// Represents validations needed to pass before continuing with the create operation.
/// </summary>
public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
  public CreateOrderCommandValidator()
  {
    RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is required.");
    RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("Customer Id is required.");
    RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("OderItems should not be empty.");
  }
}
