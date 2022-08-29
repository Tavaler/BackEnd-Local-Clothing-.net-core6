using Local.DTOS.Account;
using Local.Models;
using Local.Services;
using Local.Settings;
using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Local.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("Register/")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest data)
        {
            try
            {
                return Ok(await userService.Register(data));
            }
            catch (Exception e)
            {
                return BadRequest(Constants.Return400(e.Message));
            }
        }
        //public async Task<IActionResult> Register([FromForm] RegisterRequest registerRequset)
        //{
        //    var user = registerRequset.Adapt<User>();
        //    await userService.Register(user);
        //    return Ok(new { msg = "OK" });
        //}

        [HttpPost("Login/")]
        public async Task<IActionResult> Login([FromForm] LoginRequest data)
        {
            //var user = await userService.Login(loinRequest.UserEmail, loinRequest.UserPassword);
            //if (user == null) return Ok(new { msg = "success" });

            //var token = userService.GenerateToken(user);

            //return Ok(token);
            try
            {
                return Ok(await userService.Login(data));
            }
            catch (Exception e)
            {
                return BadRequest(Constants.Return400(e.Message));
            }
        }


        [HttpGet("[action]")]
        public async Task<ActionResult> Info()
        {
            //อ่านค่า Token (คล้ายๆ การอ่าน session)
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (accessToken == null) return Unauthorized();

            var account = userService.GetInfo(accessToken);
            return Ok(new
            {
                username = account.UserEmail,
                role = account.Role.RoleName
            });
        }

        [HttpGet("GetUser/")]
        public async Task<IActionResult> GetUser()
        {
            var user = await userService.FindAll();
            return Ok(user);
            //var result = User
            //             .OrderByDescending(p => p.UserId).ToList();
            //return result;
        }

        [HttpGet("GetByToken/")]
        public async Task<IActionResult> GetByToken()
        {
            try
            {
                var userToken = await HttpContext.GetTokenAsync("access_token");
                return Ok(await userService.GetByToken(userToken));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, e.Message });
            }
        }

        //[HttpGet("[action]")]
        //public async Task<IActionResult> GetProduct()
        //{
        //    var product = (await ps.FindAll()).Select(ProductResponse.FromProduct);
        //    return Ok(product);
        //}

    }
}
