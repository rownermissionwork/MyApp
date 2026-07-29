using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Provider.Application.Common;
using Provider.Application.Dtos;
using Provider.Application.Interfaces;
using System.Security.Claims;

namespace Provider.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Provider")]
    public class ProviderController : ControllerBase
    {
        private readonly IServiceProviderService _serviceProviderService;
        public ProviderController(IServiceProviderService serviceProviderService)
        {
            _serviceProviderService = serviceProviderService;
        }
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit([FromBody] ProviderDetailsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = GetUserClaims().UserId;
                var result = await _serviceProviderService.EditProviderDetailsAsync(userId,request, cancellationToken);
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

        private UserClaims GetUserClaims()
        {
            if (User.Identity is ClaimsIdentity claimsIdentity)
            {
                var userIdClaim = claimsIdentity.FindFirst("userId");
                var userNameClaim = claimsIdentity.FindFirst("userName");

                return new UserClaims
                {
                    UserId = int.Parse(userIdClaim!.Value),
                    UserName = userNameClaim!.Value,
                };
            }
            throw new Exception("User claims not found.");
        }
    }
}
