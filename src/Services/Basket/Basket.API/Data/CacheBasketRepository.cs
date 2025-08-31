using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data;

public class CacheBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
{
  public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
  {
    /* Pull the basket from the cache based on the username as key. */
    var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);

    /* If it was found, then deserialize into a shopping cart and return it. */
    if (!string.IsNullOrEmpty(cachedBasket)) return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;

    /* If not, pull basket matching the username from the database. */
    var basket = await repository.GetBasket(userName, cancellationToken);

    /* Set the found basket into the cache storage based on the username as key. */
    await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);

    /* Return the found basket to the client. */
    return basket;
  }

  public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
  {
    /* Store the basket into the database. */
    await repository.StoreBasket(basket, cancellationToken);

    /* Store that same basket into the cache storage based on the username as key. */
    await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);

    /* return the basket back to the client. */
    return basket;
  }

  public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
  {
    /* Delete from the database. */
    await repository.DeleteBasket(userName, cancellationToken);

    /* Then clear the basket from the cache storage based on the username as key. */
    await cache.RemoveAsync(userName, cancellationToken);

    /* Return that the operation was succesful. */
    return true;
  }
}
