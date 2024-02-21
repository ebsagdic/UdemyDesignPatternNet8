﻿using Microsoft.Extensions.Caching.Memory;
using System.Configuration;
using System.Runtime.CompilerServices;
using WebApp.Decorator.Models;

namespace WebApp.Decorator.Repositories.Decorator
{
    public class ProductRepositoryCacheDecorator : BaseProductRepositoryDecorator
    {
        private readonly IMemoryCache _memoryCache;
        private const string ProductCacheName = "products";
        public ProductRepositoryCacheDecorator(IProductRepository repository, IMemoryCache memoryCache) : base(repository)
        {
            _memoryCache = memoryCache;
        }

        public async override Task<List<Product>> GetAll()
        {
            if(_memoryCache.TryGetValue(ProductCacheName, out List<Product> cacheProducts))
            {
                return cacheProducts;
            }
            await UpdateCache();

            return _memoryCache.Get<List<Product>>(ProductCacheName);
        }
        

        public async override Task<List<Product>> GetAll(string userId)
        {
            var products = await GetAll();
            return products.Where(x => x.UserId == userId).ToList();
        }

        public override async Task<Product> Save(Product product)
        {
            await base.Save(product);
            await UpdateCache();
            return product;
        }

        public override async Task Update(Product product)
        {
            await base.Update(product);
            await UpdateCache();
        }

        public override async Task Remove(Product product)
        {
            await base.Remove(product);
            await UpdateCache();
        }

        private async Task UpdateCache()
        {
            _memoryCache.Set(ProductCacheName, await base.GetAll());
        }
    }
}
