using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace GameStore.API.Caching
{
	internal class CacheService : ICacheService
	{
		private readonly IDistributedCache distributedCache;
		private readonly ILogger<CacheService> logger;
		private readonly int defaultCacheExpirationInSeconds;

		public CacheService(
			IDistributedCache distributedCache,
			ILogger<CacheService> logger,
			int defaultCacheExpirationInSeconds = 600)
		{
			this.distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this.defaultCacheExpirationInSeconds = defaultCacheExpirationInSeconds;
		}

		public async Task Add<T>(string cacheKey, T obj, TimeSpan? absoluteExpiration = null)
		{
			if (absoluteExpiration == null)
			{
				absoluteExpiration = TimeSpan.FromSeconds(this.defaultCacheExpirationInSeconds);
			}

			await this.Add(cacheKey, obj, new DistributedCacheEntryOptions().SetAbsoluteExpiration((TimeSpan)absoluteExpiration));
		}

		public async Task Add<T>(string cacheKey, T obj, DistributedCacheEntryOptions distributedCacheEntryOptions)
		{
			var localCacheKey = this.GetServerCacheKey(cacheKey);
			this.logger.LogDebug("Adding item of type {type} for the key {key}", typeof(T).Name, localCacheKey);

			try
			{
				await this.distributedCache.SetAsync(
					localCacheKey,
					Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj)),
					distributedCacheEntryOptions);
			}
			catch (Exception ex)
			{
				this.logger.LogError(ex, "DistributedCache SetAsync exhausted the retry policy and failed to connect and/or set the type {type} using key {key}", typeof(T).FullName, localCacheKey);

				return;
			}
		}

		public Task<T> Get<T>(string cacheKey, Func<Task<T>> fallBack, TimeSpan? fallbackAbsoluteExpiration = null)
		{
			if (fallbackAbsoluteExpiration == null)
			{
				fallbackAbsoluteExpiration = TimeSpan.FromSeconds(this.defaultCacheExpirationInSeconds);
			}

			return this.Get(cacheKey, fallBack, new DistributedCacheEntryOptions().SetAbsoluteExpiration((TimeSpan)fallbackAbsoluteExpiration));
		}

		public async Task<T> Get<T>(string cacheKey, Func<Task<T>> fallBack, DistributedCacheEntryOptions fallbackDistributedCacheEntryOptions)
		{
			if (fallBack == null)
			{
				throw new ArgumentNullException(nameof(fallBack));
			}

			if (fallbackDistributedCacheEntryOptions is null)
			{
				throw new ArgumentNullException(nameof(fallbackDistributedCacheEntryOptions));
			}

			var localCacheKey = this.GetServerCacheKey(cacheKey);
			this.logger.LogDebug("Getting item of {type} for the key {key}", typeof(T).Name, localCacheKey);

			try
			{
				var cacheBytes = await this.distributedCache.GetAsync(localCacheKey);

				if (cacheBytes != null)
				{
					this.logger.LogInformation("Cache hit for {key}", localCacheKey);

					var decodedCacheBytes = Encoding.UTF8.GetString(cacheBytes);
					return JsonConvert.DeserializeObject<T>(decodedCacheBytes)!;
				}
			}
			catch (Exception ex)
			{
				this.logger.LogError(ex, "DistributedCache GetAsync exhausted the retry policy and failed to connect and/or retreive type {type} using key {key}", typeof(T).FullName, localCacheKey);
			}

			this.logger.LogInformation("Cache miss for {key}", localCacheKey);

			return await this.GetAndCacheFallBack(cacheKey, fallBack, fallbackDistributedCacheEntryOptions);
		}

		public async Task Remove(string cacheKey)
		{
			var localCacheKey = this.GetServerCacheKey(cacheKey);
			this.logger.LogDebug("Removing item for the key {key}", localCacheKey);
			await this.distributedCache.RemoveAsync(localCacheKey);
		}

		private async Task<T> GetAndCacheFallBack<T>(
			string cacheKey,
			Func<Task<T>> fallBack,
			DistributedCacheEntryOptions fallbackDistributedCacheEntryOptions)
		{
			var fallBackItem = await fallBack();
			await this.Add(cacheKey, fallBackItem, fallbackDistributedCacheEntryOptions);
			return fallBackItem;
		}

		private string GetServerCacheKey(string clientCacheKey) 
			=> $"__GameStoreApi__{clientCacheKey}";
	}
}