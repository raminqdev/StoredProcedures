using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.EFCoreServices;
using Application.Models;
using Application.StoreProcedureServices;
using AspNetCore.Lib.Models;
using Persistence.EFModels;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductSpService _productSpService;
        private readonly IProductEfCoreService _productEfCoreService;

        public ProductController(IProductSpService productSpService,
            IProductEfCoreService productEfCoreService)
        {
            _productSpService = productSpService;
            _productEfCoreService = productEfCoreService;
        }

        [HttpGet("ListSp")]
        public async Task<IList<Product>> ListSp()
        {
            return await _productSpService.List();
        }

        [HttpGet("GetConvertedListSp")]
        public async Task<IList<Product>> GetConvertedListSp()
        {
            return await _productSpService.ConvertedList();
        }

        [HttpGet("ListEfCore")]
        public async Task<IList<Product>> ListEfCore()
        {
            return await _productEfCoreService.ListProducts();
        }

        [HttpPost("CreateSp")]
        public async Task<Result> CreateSp(Product product)
        {
            return await _productSpService.CreateOrUpdate(product);
        }

        [HttpPut("UpdateSp")]
        public async Task<Result> UpdateSp(Product product)
        {
            return await _productSpService.CreateOrUpdate(product);
        }

        [HttpPost("ProductReportSp")]
        public async Task<ResultList<Persistence.EFModels.Product>> ProductReportSp(ProductReportRequestModel product)
        {
            return await _productSpService.ProductReport(product);
        }
    }
}