using DomainLayer.Contracts;
using DomainLayer.Models;
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
                    var Brand = File.ReadAllText(@"..\Infrastructure\presistenceLayer\Data\DataSeeding\brands.json");///
                    var Brands = System.Text.Json.JsonSerializer.Deserialize<List<ProductBrand>>(Brand);
                    if (Brands != null && Brands.Count > 0)
                    {
                        storeDbContext.ProductBrands.AddRange(Brands);
                    }
                }
                if (!storeDbContext.ProductTypes.Any())
                {
                    var type = File.ReadAllText(@"..\Infrastructure\presistenceLayer\Data\DataSeeding\types.json");///
                    var types = System.Text.Json.JsonSerializer.Deserialize<List<ProductType>>(type);
                    if (types != null && types.Count > 0)
                    {
                        storeDbContext.ProductTypes.AddRange(types);
                    }
                }
                if (!storeDbContext.Products.Any())
                {
                    var Product = File.ReadAllText(@"..\Infrastructure\presistenceLayer\Data\DataSeeding\products.json");///
                    var Products = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(Product);
                    if (Products != null && Products.Count > 0)
                    {
                        storeDbContext.Products.AddRange(Products);
                    }
                }
                storeDbContext.SaveChanges();


            }
            catch (Exception)
            {

                //to do
            };
             
        }
    }
}
