using Local.DTOS.Products;
using Local.Interfaces;
using Local.Models;
using Local.Models.Data;
using Local.Settings;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Local.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //private readonly LocaldbContext context;
        private readonly IProductService ps;
        public ProductController(IProductService productService)
        {
            //this.context = context;
            this.ps = productService;
        }

        [HttpGet("GetProduct/")]
        public async Task<IActionResult> GetProduct(int currentPage = 1, int pageSize = 10, int categoryId = 0, string? search = "")
        //public async Task<IActionResult> GetProducts(int currentPage = 1, int pageSize = 10, int categoryId = 0, string? search = "")
        {
            //try
            //{
            //    return Ok(await productService.GetProducts(currentPage, pageSize, categoryId, search!));
            //}
            //catch (Exception e)
            //{
            //    return BadRequest(Constants.Return400(e.Message));
            //}

            //var product = (await ps.FindAll()).Select(ProductResponse.FromProduct);
            //return Ok(product);

            try
            {
                return Ok(await ps.GetProducts(currentPage, pageSize, categoryId, search!));
            }
            catch (Exception e)
            {
                return BadRequest(Constants.Return400(e.Message));
            }
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            //var ps = new ProductService(databaseContext);
            //var data = await ps.FindById(id);
            //return Ok(ProductResponse.FromProduct(data));
            try
            {
                return Ok(await ps.FindById(id));
            }
            catch (Exception e)
            {
                return BadRequest(Constants.Return400(e.Message));
            }
        }

        //[HttpGet("{id}/search")] ///https://localhost:7097/Products/search?name=asdd
        //public IActionResult GetProduvtByName(int id,[FromQuery] string name)
        //{
        //[HttpGet("search")]
        //public async Task<IActionResult> GetProduvtByName([FromQuery] string name)
        //{
        //    var data = (await ps.Search(name)).Select(ProductResponse.FromProduct);
        //    //return Ok(new {RecieveID= id,product= data});
        //    return Ok(data);

        //}

        [HttpPost("CreateProduct/")] //[FromBody]-Json [FromForm]-Multipart/form-data
        public async Task<IActionResult> CreateProduct([FromForm] ProductRequest data)
        {
            try
            {
                return Ok(await ps.Create(data));
            }
            catch (Exception e)
            {
                return BadRequest(Constants.Return400(e.Message));
            }
        }
        //public async Task<ActionResult<Product>> AddProduct([FromForm] ProductRequest productRequest)
        //{

        //    (string errorMessage, string imageName) = await ps.UploadImage(productRequest.FormFiles);
        //    if (!string.IsNullOrEmpty(errorMessage)) return BadRequest(errorMessage);

        //    var product = productRequest.Adapt<Product>();
        //    product.ProductImage = imageName;

        //    //product.Image = "";
        //    await ps.Create(product);


        //    //redirect
        //    //return CreatedAtAction(nameof(GetProductById),new { id = 999},product);
        //    return CreatedAtAction(nameof(AddProduct), product);

        //}


        [HttpPut("UpdateProduct/")]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductUpdate data)
        {
            try
            {
                return Ok(await ps.Update(data));
            }
            catch (Exception e)
            {
                return BadRequest(Constants.Return400(e.Message));
            }
        }

        //public async Task<object> UpdateProduct(string id, [FromForm] ProductUpdate productUpdate)
        //{


        //    if (id != productUpdate.ProductId) return BadRequest("ไม่พบข้อมูล");
        //    var result = await context.Products.SingleOrDefaultAsync(a => a.ProductId.Equals(productUpdate.ProductId));
        //    //var result = await ps.FindById(id);
        //    //var result = databaseContext.Products.AsNoTracking().FirstOrDefault(x=>x.ProductId.Equals(product.ProductId));
        //    if (result is null) return Constants.Return400("ไม่พบข้อมูล");
        //    //if (result == null) return NotFound();


        //    #region จัดการรูปภาพ
        //    (string errorMessage, string imageName) = await ps.UploadImage(productUpdate.FormFiles);
        //    if (!string.IsNullOrEmpty(errorMessage)) return BadRequest(errorMessage);

        //    if (!string.IsNullOrEmpty(imageName))
        //    {
        //        await ps.DeleteImage(result.ProductImage);
        //        result.ProductImage = imageName;
        //    }
        //    #endregion

        //    productUpdate.Adapt(result);

        //    //result.Name = product.Name;
        //    //result.Price = product.Price;
        //    //result.Stock = product.Stock;

        //    await ps.Update(result);
        //    //return Ok(result);
        //    return Constants.Return200("อัพเดตข้อมูลสำเร็จ");
        //}

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                return Ok(await ps.DeleteProduct(id));
            }
            catch (Exception e)
            {
                return BadRequest(Constants.Return400(e.Message));
            }
        }
        //public async Task<ActionResult<Product>> DeleteProduct([FromQuery] string id)
        //{
        //    var data = await ps.FindById1(id);

        //    if (data == null) return Ok("ไม่พบข้อมูล");

        //    await ps.Delete(data);
        //    //await ps.DeleteImage(data);

        //    return NoContent();
        //}





        //[HttpDelete("[action]/{id}")]
        //[Authorize]
        //public async Task<IActionResult> DeleteProduct(string id)
        //{
        //    try
        //    {
        //        return Ok(await productService.DeleteProduct(id));
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(Constants.Return400(e.Message));
        //    }
        //}


    }
}
