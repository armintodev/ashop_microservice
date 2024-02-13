using Domain.Entities;

using Microsoft.AspNetCore.Mvc;

using Persistence.Repositories.Interfaces;

using System.Net;

namespace _99_Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CatalogController : ControllerBase
{

    private readonly IProductRepository _repository;
    private readonly ILogger<CatalogController> _logger;
    public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(CancellationToken cancellationToken)
    {
        var products = await _repository.GetProducts(cancellationToken);
        return Ok(products);
    }

    [HttpGet("{id:length(24)}", Name = "GetProduct")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> GetProductById(string id, CancellationToken cancellationToken)
    {
        var product = await _repository.GetProduct(id, cancellationToken);
        if (product is null)
        {
            _logger.LogError($"Product with id: {id}, not found.");
            return NotFound();
        }

        return Ok(product);
    }
    [Route("[action]/{category}", Name = "GetProductByCategory")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category, CancellationToken cancellationToken)
    {
        var products = await _repository.GetProductByCategory(category, cancellationToken);
        return Ok(products);
    }
    [HttpPost]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product, CancellationToken cancellationToken)
    {
        await _repository.Create(product, cancellationToken);
        return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
    }
    [HttpPut]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProduct([FromBody] Product product, CancellationToken cancellationToken)
    {
        return Ok(await _repository.Update(product, cancellationToken));
    }
    [HttpDelete("{id:length(24)}", Name = "Delete")]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProductById(string id, CancellationToken cancellationToken)
    {
        return Ok(await _repository.Delete(id, cancellationToken));
    }
}
