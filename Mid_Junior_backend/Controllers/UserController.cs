using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Mid_Junior_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly vending_machine _dbContext;
        private readonly ILogger _logger;

        public UserController(vending_machine dbContext, ILogger<UserController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        // GET, POST, PUT, DELETE actions for users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _dbContext.Remove(user);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        
        public async Task<User> GetUserAsync(string userName)
        {
            try
            {
                var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == userName);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user");
                throw; // Rethrow to be handled by the calling method
            }
        }









    }
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly vending_machine _dbContext;

        public ProductsController(vending_machine dbContext)
        {
            _dbContext = dbContext;
        }

        // GET, POST, PUT, DELETE actions for products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<product>>> GetProducts()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<ActionResult<User>> GetProduct(int id)
        {
            var prod = await _dbContext.Products.FindAsync(id);
            if (prod == null)
            {
                return NotFound();
            }

            return Ok(prod);
        }
        [HttpPost]
        [Authorize(Roles = "seller")]  // Restrict to sellers
        public async Task<ActionResult<product>> CreateProduct(product product)
        {
            // Validate sellerId and other fields
            if (product.SellerId == null)
            {
                return BadRequest("SellerId is required");
            }

            // Validate other fields as needed (e.g., product name, cost, etc.)

            _dbContext.Add(product);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "seller")]  // Restrict to sellers
        public async Task<IActionResult> UpdateProduct(int id, product product)
        {
            if (id != product.Id)
            {
                return BadRequest("Product ID mismatch");
            }

            var existingProduct = await _dbContext.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            // Validate sellerId and other fields (similar to CreateProduct)

            _dbContext.Update(product);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "seller")]  // Restrict to sellers
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _dbContext.Remove(product);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

    }
}