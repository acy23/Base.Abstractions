using Cache;
using GenericRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRepository _repo;
        private readonly ICacheService _cache;

        public ProductController(AppDbContext context, ICacheService cache)
        {
            _context = context;
            _repo = new Repository(_context);
            _cache = cache;
        }

        [HttpPost]
        public async Task Create([FromBody] Product product)
        {
            await _repo.Create(product);

            _cache.Set<Product>(product.Id.ToString(), product);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            //// FROM DB
            //var item = await _repo.Get<Product>(id);
            //return item;

            // FROM CACHE
            var item = _cache.Get<Product>(id.ToString());
            if(item != null)
            {
                return Ok(item);
            }

            return NotFound();
        }

        [HttpPut]
        public async Task<Product> Update(int id, Product entity)
        {
            var updatedItem = await _repo.Update<Product>(entity, id);
            return updatedItem;
        }

        [HttpGet("get-list")]
        public async Task<List<Product>> GetListById()
        {
            var entities = await _repo.GetList<Product>();
            return entities;
        }

        [HttpGet("get-list-by-expression")]
        public async Task<List<Product>> GetListByExpression()
        {
            var entities = await _repo.GetListByExpression<Product>(x=>x.Id == 1);
            return entities;
        }
    }
}
