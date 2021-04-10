using DataAccess.EFModels;
using DataAccess.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IProductStore productStore;
        private readonly ITodo todo;

        public WeatherController(IProductStore productStore,ITodo todo)
        {
            this.productStore = productStore;
            this.todo = todo;
        }

        [HttpGet]
        public async Task<IList<Product>> Get()
        {
            return await productStore.List();
        }

        [HttpGet("GetConvertedList")]
        public async Task<IList<Product>> GetConvertedList()
        {
            return await productStore.ConvertedList();
        }

        [HttpGet("GetProductsEntityFramWorkCore")]
        public async Task<IList<Product>> GetProductsEntityFramWorkCore()
        {
            return await todo.ListProductsEntityFramWorkCore();
        }
    }
}
