using GenericRepository;
using Response;

namespace Web.API
{
    public class ProductService : IProductService
    {
        private readonly IRepository _repo;
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
            _repo = new Repository(_context);
        }

        public async Task<ServiceResponse<Product>> GetById(int id)
        {
            var item = await _repo.Get<Product>(id);
            if(item == null)
            {
                return ServiceResponse<Product>.NotFound($"Item with id: {id} not found");
            }

            return ServiceResponse<Product>.Ok(item);

        }
    }
}
