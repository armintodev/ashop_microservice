using System.Net;
using Discount.Data;
using Discount.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Discount.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DiscountController(IDiscountRepository repository) : ControllerBase
{
    private readonly IDiscountRepository
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    [HttpGet("{productName}", Name = "GetDiscount")]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Coupon>> GetDiscount(string productName, CancellationToken ct)
    {
        var discount = await _repository.GetDiscount(productName, ct);
        return Ok(discount);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon, CancellationToken ct)
    {
        await _repository.CreateDiscount(coupon, ct);

        return CreatedAtRoute("GetDiscount", new
        {
            productName = coupon.ProductName
        }, coupon);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public ActionResult<Coupon> UpdateDiscount([FromBody] Coupon coupon)
    {
        _repository.UpdateDiscount(coupon);

        return Ok();
    }

    [HttpDelete("{productName}", Name = "DeleteDiscount")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> DeleteDiscount(string productName, CancellationToken ct)
    {
        await _repository.DeleteDiscount(productName, ct);

        return Ok();
    }
}