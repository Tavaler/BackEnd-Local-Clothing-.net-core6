using Local.Settings;
using Local.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Local.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoriesController(ICategoryService categoryService) => this.categoryService = categoryService;

        [HttpGet("GetCategory/")]
        public async Task<object> GetCategory()
        {
            try
            {
                return Ok(await categoryService.Getcategory());
            } catch(Exception e)
            {
                return BadRequest(Constants.Return400(e.Message));
            }
        }

        [HttpPost("CreateCategory/")]
        public async Task<object> CreateCategory([FromForm] string id, [FromForm] string name)
        {
            try
            {
                return Ok(await categoryService.CreateCategory(id,name));
            }
            catch (Exception e)
            {
                return BadRequest(Constants.Return400(e.Message));
            }
        }

        [HttpPut("UpdateCategory/")]
        public async Task<object> UpdateCategory([FromForm]string id,[FromForm]string name)
        {
            try
            {
                return Ok(await categoryService.UpdateCategory(id,name));
            }
            catch (Exception e)
            {
                return BadRequest(Constants.Return400(e.Message));
            }
        }

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<object> DeleteCategory(string id)
        {
            try
            {
                return Ok(await categoryService.DeleteCategory(id));
            }
            catch (Exception e)
            {
                return BadRequest(Constants.Return400(e.Message));
            }
        }


    }
}
