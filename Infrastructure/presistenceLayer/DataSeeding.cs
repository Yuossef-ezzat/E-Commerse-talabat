using DomainLayer.Contracts;
using DomainLayer.Models.Product;
using Microsoft.EntityFrameworkCore;
using PresistenceLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresistenceLayer
{
    public class DataSeeding(StoreDbContext storeDbContext) : IDataSeeding
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
    }
}
