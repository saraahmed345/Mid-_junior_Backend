using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using  Mid_Junior_backend.Interfaces;

namespace Mid_Junior_backend
{
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet("/products/{productId}/availability")]
        public IActionResult CheckAvailability(int productId)
        {
            var availability = _purchaseService.CheckProductAvailability(productId);
            return Ok(availability); // Assuming CheckProductAvailability returns a bool or similar
        }

        [HttpPost("/cart/{productId}")]
        [Authorize(Roles = "buyer")]
        public IActionResult AddToCart(int productId, int quantity)
        {
            try
            {
                _purchaseService.AddToCart(User.Identity.Name, productId, quantity);
                return Ok();
            }
            catch (Exception ex)
            {
                // Handle specific exceptions (e.g., insufficient funds, product not found)
                return BadRequest(ex.Message);
            }
        }

       

        [HttpGet("/cart/change")]
        [Authorize(Roles = "buyer")]
        public IActionResult GetChangeAmount()
        {
            var changeAmount = _purchaseService.GetChangeAmount(User.Identity.Name);
            return Ok(changeAmount);
        }

        
    }
}

