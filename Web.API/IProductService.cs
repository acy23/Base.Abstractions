using Response;

namespace Web.API
{
    public interface IProductService
    {
        Task<ServiceResponse<Product>> GetById(int Id);
    }
}
