using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.StoreProcedureServices;
using AspNetCore.Lib.Models;
using Persistence.EFModels;
using Persistence.Stores;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductStore _productStore;
        private readonly IProductService _productService;


        public ProductController(IProductStore productStore,IProductService productService)
        {
            _productStore = productStore;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IList<Product>> Get()
        {
            return await _productStore.List();
        }

        [HttpGet("GetConvertedList")]
        public async Task<IList<Product>> GetConvertedList()
        {
            return await _productStore.ConvertedList();
        }

        [HttpGet("GetProductsEntityFrameWorkCore")]
        public async Task<IList<Product>> GetProductsEntityFrameWorkCore()
        {
            return await _productService.ListProductsEntityFrameWorkCore();
        }
        
        [HttpPost]
        public async Task<Result> Create(Product product)
        {
            return await _productService.CreateOrUpdate(product);
        }
        
        [HttpPut]
        public async Task<Result> Update(Product product)
        {
            return await _productService.CreateOrUpdate(product);
        }
        
    }
}
