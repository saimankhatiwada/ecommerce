using Asp.Versioning;
using Ecommerce.Api.Utils;
using Ecommerce.Application.Products.CreateProduct;
using Ecommerce.Application.Products.DeleteProduct;
using Ecommerce.Application.Products.GetAllProduct;
using Ecommerce.Application.Products.GetProduct;
using Ecommerce.Application.Products.Shared;
using Ecommerce.Application.Products.UpdateProduct;
using Ecommerce.Domain.Abstractions;
using Ecommerce.Infrastructure.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Products;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/products")]
public class ProductController : ControllerBase
{
    private readonly ISender _sender;

    public ProductController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
    [HttpGet()]
    public async Task<IActionResult> GetAllProduct(CancellationToken cancellationToken)
    {
        var query = new GetAllProductQuery();

        Result<IReadOnlyList<ProductResponse>> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProductQuery(id);

        Result<ProductResponse> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost()]
    [HasPermission(Permissions.ProductsCreate)]
    public async Task<IActionResult> CreateProduct(ProductCreateRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand(
            request.CategoryId, 
            request.SubCategoryId,
            request.Name,
            request.Description,
            request.Status,
            request.Amount,
            request.Currency,
            $"Products/{Guid.NewGuid()}{Path.GetExtension(request.ImageFile.FileName)}",
            request.ImageFile.ContentType,
            request.ImageFile.OpenReadStream());

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? 
            CreatedAtAction(nameof(GetProduct), new { id = result.Value }, result.Value) : 
            BadRequest(result.Error);
    }

    [HttpPut("{id}")]
    [HasPermission(Permissions.ProductsUpdate)]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductUpdateRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateProductCommand(
            id,
            request.CategoryId, 
            request.SubCategoryId,
            request.Name,
            request.Description,
            request.Status,
            request.Amount,
            request.Currency,
            $"Products/{Guid.NewGuid()}{Path.GetExtension(request.ImageFile.FileName)}",
            request.ImageFile.ContentType,
            request.ImageFile.OpenReadStream());

        Result result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpDelete("{id}")]
    [HasPermission(Permissions.ProductsDelete)]
    public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand(id);

        Result result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }
}
