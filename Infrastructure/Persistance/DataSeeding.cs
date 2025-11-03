using DomainLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace Persistance
{
    public class DataSeeding(TStoreDbContext storeDbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                var pendingMigrations = await storeDbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    await storeDbContext.Database.MigrateAsync();
                }

                if (!storeDbContext.Set<ProductBrand>().Any())
                {
                    var ProductBrandData = File.OpenRead("../Infrastructure/Persistance/Data/DataSeed/brands.json");
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);
                    if (ProductBrands != null && ProductBrands.Any())
                    {
                        await storeDbContext.ProductBrands.AddRangeAsync(ProductBrands);
                    }
                }
                if (!storeDbContext.Set<ProductType>().Any())
                {
                    var ProductTypeData = File.OpenRead("../Infrastructure/Persistance/Data/DataSeed/types.json");
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypeData);
                    if (ProductTypes != null && ProductTypes.Any())
                    {
                        await storeDbContext.ProductTypes.AddRangeAsync(ProductTypes);
                    }
                }
                if (!storeDbContext.Set<Product>().Any())
                {
                    var ProductData = File.OpenRead("../Infrastructure/Persistance/Data/DataSeed/products.json");
                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);
                    if (Products != null && Products.Any())
                    {
                       await storeDbContext.Products.AddRangeAsync(Products);
                    }
                }
                await storeDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}
