using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mid_Junior_backend;



[Authorize(Roles = "seller")] // Restrict all actions to sellers
public class ProductController : ControllerBase
{
    private readonly PurchaseService _purchaseService; // Inject PurchaseService
    private readonly vending_machine _cont;
     
    public ProductController(PurchaseService purchaseService,vending_machine v)
    {
        _purchaseService = purchaseService;
        _cont = v;
    }

    // GET: api/products
    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = _purchaseService.GetProducts().ToList(); // Assuming GetProducts on PurchaseService
        return Ok(products);
    }

    // GET: api/products/{id}
    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var product = _purchaseService.GetProducts().SingleOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public IActionResult AddProduct(product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _cont.Products.Add(product);
        _cont.SaveChanges(); // Assuming SaveChanges on PurchaseService

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    // PUT: api/products/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingProduct = _purchaseService.Find(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        // Update product details (assuming properties are settable)
        existingProduct.Product_Name = product.Product_Name;
        existingProduct.Cost = product.Cost;
        existingProduct.AmountAvailable = product.AmountAvailable;
        existingProduct.SellerId=existingProduct.SellerId;
        existingProduct.Id = existingProduct.Id;
        

        _cont.SaveChanges();

        return NoContent();
    }

    // DELETE: api/products/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = _purchaseService.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        // In ProductController
       var products = _purchaseService.Products.ToList();
        _cont.Products.Remove(product);
        _cont.SaveChanges();

        return NoContent();
    }
}
