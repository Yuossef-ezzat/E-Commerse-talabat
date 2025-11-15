using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PresistenceLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresistenceLayer
{
    public class DataSeeding(StoreDbContext storeDbContext,
                             UserManager<ApplicationUser> _userManager,
                             RoleManager<IdentityRole> _roleManager) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                //production
                if ((await storeDbContext.Database.GetPendingMigrationsAsync()).Any())
                {
                    await storeDbContext.Database.MigrateAsync();
                }
                if (!storeDbContext.ProductBrands.Any())
                {
                    //var Brand = await File.ReadAllTextAsync(@"..\Infrastructure\presistenceLayer\Data\DataSeeding\brands.json");///
                    var Brand =  File.OpenRead(@"..\Infrastructure\presistenceLayer\Data\DataSeeding\brands.json");///
                    var Brands = await System.Text.Json.JsonSerializer.DeserializeAsync<List<ProductBrand>>(Brand);
                    if (Brands != null && Brands.Count > 0)
                    {
                        await storeDbContext.ProductBrands.AddRangeAsync(Brands);
                    }
                }
                if (!storeDbContext.ProductTypes.Any())
                {
                    var type = File.OpenRead(@"..\Infrastructure\presistenceLayer\Data\DataSeeding\types.json");///
                    var types = await System.Text.Json.JsonSerializer.DeserializeAsync<List<ProductType>>(type);
                    if (types != null && types.Count > 0)
                    {
                        await storeDbContext.ProductTypes.AddRangeAsync(types);
                    }
                }
                if (!storeDbContext.Products.Any())
                {
                    var Product = File.OpenRead(@"..\Infrastructure\presistenceLayer\Data\DataSeeding\products.json");///
                    var Products = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Product>>(Product);
                    if (Products != null && Products.Count > 0)
                    {
                       await storeDbContext.Products.AddRangeAsync(Products);
                    }
                }
                await storeDbContext.SaveChangesAsync();


            }
            catch (Exception)
            {

                //to do
            };
             
        }

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                    await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
                }


                if (!_userManager.Users.Any())
                {

                    var user01 = new ApplicationUser
                    {
                        Email = "YuossefEzzat@gmail.com",
                        DisplayName = "Youssef Ezzat",
                        PhoneNumber = "0123478912",
                        UserName = "YuossefEzzat"
                    };
                    var user02 = new ApplicationUser
                    {
                        Email = "HamzaEzzat@gmail.com",
                        DisplayName = "Hamza Ezzat",
                        PhoneNumber = "0123499992",
                        UserName = "HamzaEzzat"
                    };
                    await _userManager.CreateAsync(user01, "P@ssw0rd");
                    await _userManager.CreateAsync(user02, "P@ssw0rd");

                    await _userManager.AddToRoleAsync(user01, "Admin");
                    await _userManager.AddToRoleAsync(user02, "SuperAdmin");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
