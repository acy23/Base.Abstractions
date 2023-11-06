using GenericRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRepository _repo;

        public ProductController(AppDbContext context)
        {
            _context = context;
            _repo = new Repository(_context);   
        }

        [HttpPost]
        public async Task Create([FromBody] Product product)
        {
            await _repo.Create(product);
        }

        [HttpGet]
        public async Task<Product> GetById(int id)
        {
            var item = await _repo.Get<Product>(id);
            return item;
        }

        [HttpPut]
        public async Task<Product> Update(int id, Product entity)
        {
            var updatedItem = await _repo.Update<Product>(entity, id);
            return updatedItem;
        }
    }
}
