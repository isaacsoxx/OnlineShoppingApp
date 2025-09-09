using Discount.GRPC;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
  public StoreBasketCommandValidator()
  {
    RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null.");
    RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("Username is required.");
  }
}

/// <summary>
/// Handler class that will be triggered when command StoreBasketCommand is called.
/// </summary>
/// <param name="repository">Data Access Layer abstraction.</param>
/// <param name="discountProto">Client for interacting with the Discount gRPC service to retrieve and apply discount information.</param>
internal class StoreBasketCommandHandler
(
  IBasketRepository repository,
  DiscountProtoService.DiscountProtoServiceClient discountProto
) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
  public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
  {
    await DeductDiscount(command.Cart, cancellationToken);

    await repository.StoreBasket(command.Cart, cancellationToken);
    return new StoreBasketResult(command.Cart.UserName);
  }

  public async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
  {
    foreach (var item in cart.Items)
    {
      var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
      item.Price -= coupon.Amount;
    }
  }
}
