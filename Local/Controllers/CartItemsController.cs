using Local.DTOS.CartItems;
using Local.Settings;
using Local.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Local.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemService cartItemService;
        public CartItemsController(ICartItemService cartItemService)
        {
            this.cartItemService = cartItemService;
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByAccountId(string id)
        {
            try
            {
                return Ok(await cartItemService.GetCartItemByAccountId(id));
            } catch(Exception e)
            {
                return BadRequest(Constants.Return400(e.Message));
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateCartItem([FromForm]CartItemCreate data)
        {
            try
            {
                return Ok(await cartItemService.CreateCartItem(data));
            }
            catch (Exception e)
            {
                return BadRequest(Constants.Return400(e.Message));
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            try
            {
                return Ok(await cartItemService.DeleteCartItem(id));
            }
            catch (Exception e)
            {
                return BadRequest(Constants.Return400(e.Message));
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> ItemPlus(int id)
        {
            try
            {
                return Ok(await cartItemService.ItemPlus(id));
            }
            catch (Exception e)
            {
                return BadRequest(Constants.Return400(e.Message));
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> ItemRemove(int id)
        {
            try
            {
                return Ok(await cartItemService.ItemRemove(id));
            }
            catch (Exception e)
            {
                return BadRequest(Constants.Return400(e.Message));
            }
        }

    }
}
