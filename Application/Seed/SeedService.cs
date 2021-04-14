using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AspNetCore.Lib.Services.Interfaces;
using Newtonsoft.Json;
using Persistence.EFModels;

namespace Application.Seed
{
    public class SeedService : ISeedService
    {
        private readonly ISerializerService _serializer;

        public SeedService(ISerializerService serializer)
        {
            _serializer = serializer;
        }

        public List<Storage> GetStorageBulkData()
        {
            string storageFile = File.ReadAllText(Path.Combine(Environment.CurrentDirectory,
                "../Application/Seed/Storages.json"));
            var storages = JsonConvert.DeserializeObject<IList<Storage>>(storageFile).ToList(); //500

            string productFile = File.ReadAllText(Path.Combine(Environment.CurrentDirectory,
                "../Application/Seed/Products.json"));
            var products = JsonConvert.DeserializeObject<IList<Product>>(productFile).ToList(); //1000

            string supplierFile = File.ReadAllText(Path.Combine(Environment.CurrentDirectory,
                "../Application/Seed/Suppliers.json"));
            var suppliers = JsonConvert.DeserializeObject<IList<Supplier>>(supplierFile).ToList(); //30

            
            var random = new Random();
            var x = 10;
            var y = x * 10;
            var totalStorage = 500 * x;                  
            var totalProduct = 1000 * y + 20;             
            var totalSupplier = 100 * x;                  


            var newStorages = new List<Storage>();
            for (var i = 0; i < totalStorage; i++)
            {
                var s = storages[random.Next(0, 500)];
                newStorages.Add(new Storage
                {
                    Id = Guid.NewGuid(),
                    Name = s.Name,
                    Address = s.Address,
                    City = s.City,
                    Phone = s.Phone,
                    Enabled = s.Enabled
                });
            }
            if (newStorages.GroupBy(x => x.Id).Any(g => g.Count() > 1))
                throw new Exception("Storages duplicate key");


            var newProducts = new List<Product>();
            for (var i = 0; i < totalProduct; i++)
            {
                var s = products[random.Next(0, 1000)];
                newProducts.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = s.Name,
                    Code = s.Code,
                    Description = s.Description,
                    Quantity = s.Quantity,
                    Enabled = s.Enabled,
                    UnitePrice = s.UnitePrice
                });
            }
            if (newProducts.GroupBy(x => x.Id).Any(g => g.Count() > 1))
                throw new Exception("Product duplicate key");


            var newSuppliers = new List<Supplier>();
            for (var i = 0; i < totalSupplier; i++)
            {
                var s = suppliers[random.Next(0, 30)];
                newSuppliers.Add(new Supplier
                {
                    Id = Guid.NewGuid(),
                    CompanyName = s.CompanyName,
                    Address = s.Address,
                    City = s.City,
                    Country = s.Country,
                    Enabled = s.Enabled,
                    Fax = s.Fax,
                    Phone = s.Phone,
                    Region = s.Region,
                    ContactName = s.ContactName,
                    ContactTitle = s.ContactTitle,
                    EmergencyMobile = s.EmergencyMobile,
                    HomePage = s.HomePage,
                    PostalCode = s.PostalCode
                });
            }
            if (newSuppliers.GroupBy(x => x.Id).Any(g => g.Count() > 1))
                throw new Exception("Supplier duplicate key");


            for (var i = 0; i < newStorages.Count; i++)
            {
                for (var j = 20 * i; j < 20 * (i + 1); j++)
                {
                    try
                    {
                        newProducts[j].StorageId = newStorages[i].Id;
                        newProducts[j].Storage = newStorages[i];
                        newStorages[i].Products.Add(newProducts[j]);

                        var index = random.Next(0, totalSupplier);
                        newProducts[j].SupplierId = newSuppliers[index].Id;
                        newProducts[j].Supplier = newSuppliers[index];
                        newSuppliers[index].Products.Add(newProducts[j]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(i); Console.WriteLine(j); throw e;
                    }
                }
            }

            return newStorages;
        }
    }

    public interface ISeedService
    {
        List<Storage> GetStorageBulkData();
    }
}