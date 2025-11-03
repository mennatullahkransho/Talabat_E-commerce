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
        public void DataSeed()
        {
            try
            {
                if (storeDbContext.Database.GetPendingMigrations().Any())
                {
                    storeDbContext.Database.Migrate();
                }

                if (!storeDbContext.ProductBrands.Any())
                {
                    var ProductBrandData = File.ReadAllText("../Infrastructure/Persistance/Data/SeedData/brands.json");
                    var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandData);
                    if (ProductBrands != null && ProductBrands.Any())
                    {
                        storeDbContext.ProductBrands.AddRange(ProductBrands);
                    }
                }
                if (!storeDbContext.ProductTypes.Any())
                {
                    var ProductTypeData = File.ReadAllText("../Infrastructure/Persistance/Data/SeedData/types.json");
                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypeData);
                    if (ProductTypes != null && ProductTypes.Any())
                    {
                        storeDbContext.ProductTypes.AddRange(ProductTypes);
                    }
                }
                if (!storeDbContext.Products.Any())
                {
                    var ProductData = File.ReadAllText("../Infrastructure/Persistance/Data/SeedData/products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                    if (Products != null && Products.Any())
                    {
                        storeDbContext.Products.AddRange(Products);
                    }
                }
                storeDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}
