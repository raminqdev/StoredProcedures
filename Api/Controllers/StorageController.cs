using System.Threading.Tasks;
using Application.StoreProcedureServices.Storage;
using AspNetCore.Lib.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageController : ControllerBase
    {
        private readonly IStorageService _storageService;

        public StorageController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpPost]
        [Route("AddStoragesSp")]
        public async Task<Result> AddStoragesSp()
        {
            return await _storageService.AddStoragesSp();
        }
        
        [HttpPost]
        [Route("AddStoragesEfCore")]
        public async Task<Result> AddStoragesEfCore()
        {
            return await _storageService.AddStoragesEfCore();
        }
    }
}