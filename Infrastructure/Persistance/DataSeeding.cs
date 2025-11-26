using DomainLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using DomainLayer.Models.IdentityModule;
using Persistance.Identity;
using DomainLayer.Models.OrderModule;

namespace Persistance
{
    public class DataSeeding(TStoreDbContext storeDbContext,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        StoreIdentityDbContext identityDbContext) : IDataSeeding
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
                if (!storeDbContext.Set<DeliveryMethod>().Any())
                {
                    var DeliveryMethodData = File.OpenRead("../Infrastructure/Persistance/Data/DataSeed/delivery.json");
                    var DeliveryMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliveryMethodData);
                    if (DeliveryMethods != null && DeliveryMethods.Any())
                    {
                        await storeDbContext.Set<DeliveryMethod>().AddRangeAsync(DeliveryMethods);
                    }
                }
                await storeDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }



        }

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        Email = "mennatullahkransho@gmail.com",
                        DisplayName = "Mennatullah Kransho",
                        UserName = "mennatullahkransho",
                        PhoneNumber = "01013872972"
                    };
                    var User02 = new ApplicationUser()
                    {
                        Email = "ahmedkransho@gmail.com",
                        DisplayName = "Ahmed Kransho",
                        UserName = "Ahmedahkransho",
                        PhoneNumber = "01001025337"
                    };

                    await userManager.CreateAsync(User01, "Password@123");
                    await userManager.CreateAsync(User02, "Password@456");

                    await userManager.AddToRoleAsync(User01, "Admin");
                    await userManager.AddToRoleAsync(User02, "SuperAdmin");
                }
                await identityDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
