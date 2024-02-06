using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;  // Namespace for JWT authentication
using Microsoft.AspNetCore.Builder;                 // Namespace for building ASP.NET Core applications
using Microsoft.AspNetCore.Hosting;                // Namespace for hosting ASP.NET Core applications
using Microsoft.Extensions.Configuration;          // Namespace for configuration management
using Microsoft.Extensions.DependencyInjection;    // Namespace for dependency injection
using Microsoft.Extensions.Hosting;                // Namespace for extension-based hosting
using Microsoft.IdentityModel.Tokens;              // Namespace for working with security tokens
using System.Text;
using Mid_Junior_backend.Interfaces;



namespace Mid_Junior_backend.Controllers
{
    public class DepositController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<DepositController> _logger;

        public DepositController(IUserService userService, ILogger<DepositController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit(int userName, int[] coins)
        {
            try
            {
                // Validate input
                if (coins == null || coins.Length == 0)
                {
                    return BadRequest("Invalid coins.");
                }

                foreach (var coin in coins)
                {
                    if (!ValidCoinValues.Contains(coin))
                    {
                        return BadRequest("Invalid coin value.");
                    }
                }

                // Retrieve the user entity
                var user = await _userService.GetUserById(userName);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Check user role
                if (user.Role != "buyer")
                {
                    return Unauthorized("Only buyers can deposit coins.");
                }

                // Calculate total deposit
                var totalDeposit = coins.Sum();

                // Update user's deposit
                user.Deposit += totalDeposit;
                await _userService.UpdateUser(userName,user);

                return Ok("Deposit successful.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing deposit");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("reset")]
        public async Task<IActionResult> ResetDeposit(int ID)
        {
            try
            {
                // Retrieve the user entity
                var user = await _userService.GetUserById(ID);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Check user role
                if (user.Role != "buyer")
                {
                    return Unauthorized("Only buyers can reset their deposit.");
                }

                // Reset deposit to zero
                user.Deposit = 0;
                await _userService.UpdateUser(ID,user);

                return Ok("Deposit reset successful.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resetting deposit");
                return StatusCode(500, "Internal server error");
            }
        }

        private static readonly List<int> ValidCoinValues = new List<int> { 5, 10, 20, 50, 100 };
    }
}
