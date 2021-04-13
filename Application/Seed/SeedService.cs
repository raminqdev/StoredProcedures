using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
            var storages = JsonConvert.DeserializeObject<IList<Storage>>(storageFile).ToList();

            string productFile = File.ReadAllText(Path.Combine(Environment.CurrentDirectory,
                "../Application/Seed/Products.json"));
            var products = JsonConvert.DeserializeObject<IList<Product>>(productFile).ToList();


            return storages;
        }
    }
    
    public interface ISeedService
    {
        List<Storage> GetStorageBulkData();
    }
}