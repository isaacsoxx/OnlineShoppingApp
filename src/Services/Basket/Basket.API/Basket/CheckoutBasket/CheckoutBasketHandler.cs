
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(CheckoutBasketDto CheckoutBasketDto) : ICommand<CheckoutBasketResult>;
public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
  public CheckoutBasketCommandValidator()
  {
    RuleFor(x => x.CheckoutBasketDto).NotNull().WithMessage("CheckoutBasketDto cannot be null.");
    RuleFor(x => x.CheckoutBasketDto.UserName).NotEmpty().WithMessage("Username is required.");
  }
}

public class CheckoutBasketCommandHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
  public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
  {
    var basket = await repository.GetBasket(command.CheckoutBasketDto.UserName, cancellationToken);
    if (basket is null) throw new BasketNotFoundException(command.CheckoutBasketDto.UserName);

    var eventMessage = command.CheckoutBasketDto.Adapt<CheckoutBasketEvent>();
    await publishEndpoint.Publish(eventMessage, cancellationToken);

    await repository.DeleteBasket(command.CheckoutBasketDto.UserName, cancellationToken);
    return new CheckoutBasketResult(true);
  }
}
