using Local.Settings;
using Local.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Local.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImgService productImgService;
        public ProductImageController(IProductImgService productImgService) => this.productImgService = productImgService;

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<object> DeleteProductImage (string id)
        {
            try
            {
                return Ok(await productImgService.Delete(id));
            } catch(Exception e)
            {
                return Constants.Return400(e.Message);
            }
        }

          
    }
}
