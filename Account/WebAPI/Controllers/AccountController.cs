using Account.Application.Dtos.User;
using Account.Application.Interfaces;
using Account.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Account.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUserService userService, ISecretGeneratorService secretGeneratorService) : ControllerBase
    {

        private readonly IUserService _userService = userService;
        private readonly ISecretGeneratorService _secretGeneratorService = secretGeneratorService;
        [HttpGet("Role")]
        public async Task<ActionResult<List<UserRoleDto>>> GetRole(CancellationToken cancellationToken)
        {
            try
            {
                var header = this.Request.Headers;
                if (!header.ContainsKey("SecretKey")) {
                    return StatusCode(StatusCodes.Status400BadRequest, "SecretKey not found.");
                }
                var key = header.FirstOrDefault(x => x.Key == "SecretKey").Value.ToString();
                var rawSecretKey = _secretGeneratorService.GenerateSecretKey($"_{ DateTime.UtcNow.ToString("MMddyyyy")}");
                var sk = rawSecretKey.Substring(4, 20);
                if (key != sk)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Unauthorized access.");
                }

                return await _userService.GetRoleAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"There was an error processing your request. Please try again : {ex.Message}");
            }

        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request,CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.RegisterAsync(request,cancellationToken);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result.Error);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            try
            {
                var result = await _userService.LoginAsync(request);
                if (result.IsSuccess) {
                    return Ok(result);
                }
                else {
                    return StatusCode(StatusCodes.Status400BadRequest, result.Error);
                }
                
            }
            catch (Exception ex) {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }


        [Authorize]
        // GET: api/<AccountController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return ["value1", "value2"];
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {  
            return "value";
        }

        // POST api/<AccountController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
