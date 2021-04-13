using System.Threading.Tasks;
using Application.StoreProcedureServices;
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
        public async Task<Result> AddStorages()
        {
            return await _storageService.AddStorages();
        }
    }
}