using Local.DTOS.Products;
using Local.Models;

namespace Local.Interfaces
{
    public interface IProductService
    {
        //Task<IEnumerable<Product>> FindAll();
        Task<object> GetProducts(int currentPage, int pageSize, int categoryId, string search);
        Task<Product> FindById(string id);
        Task<Product> FindById1(string id);
        Task<object> Create(ProductRequest data);
        Task<object> Update(ProductUpdate data);
        Task Delete(Product product);
        Task<object> DeleteProduct(string id);

        Task<IEnumerable<Product>> Search(string name);
        //Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles);
        Task DeleteImage(string imageName);
        
    }
}
