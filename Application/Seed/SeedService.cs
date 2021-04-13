using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Lib.Services.Interfaces;
using Persistence.EFModels;

namespace Application.Seed
{
    public class SeedService
    {
        private readonly ISerializerService _serializer;

        public SeedService(ISerializerService serializer)
        {
            _serializer = serializer;
        }
        public Task<List<Storage>> GetStorageBulkData()
        {
            string storageFile = File.ReadAllText(Path.Combine(Environment.CurrentDirectory,
                ""));
            var storages = _serializer.DeserializeFromJson<IList<Storage>>(storageFile).ToList();
            
            string productFile = File.ReadAllText(Path.Combine(Environment.CurrentDirectory,
                "wwwroot/seeddata/product.json"));
            var products = _serializer.DeserializeFromJson<IList<Product>>(productFile).ToList();



            return null;
        }
    }
}