using Microsoft.Extensions.Caching.Distributed;

namespace GameStore.API.Caching
{
	public interface ICacheService
	{
		Task<T> Get<T>(string cacheKey, Func<Task<T>> fallBack, TimeSpan? fallbackAbsoluteExpiration = null);

		Task<T> Get<T>(string cacheKey, Func<Task<T>> fallBack, DistributedCacheEntryOptions fallbackDistributedCacheEntryOptions);

		Task Add<T>(string cacheKey, T obj, TimeSpan? absoluteExpiration = null);

		// Use this method if we want to add some options when saving the cache values
		Task Add<T>(string cacheKey, T obj, DistributedCacheEntryOptions distributedCacheEntryOptions);

		Task Remove(string cacheKey);
	}
}