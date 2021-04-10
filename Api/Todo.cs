using DataAccess.EFModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api
{


    public class Todo : ITodo
    {
        private readonly AppDbContext context;

        public Todo(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IList<Product>> ListProductsEntityFramWorkCore()
        {
            return await context.Products.ToListAsync();
        }
    }

    public interface ITodo
    {
        Task<IList<Product>> ListProductsEntityFramWorkCore();
    }
}
