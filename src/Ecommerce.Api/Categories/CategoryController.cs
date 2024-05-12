using Asp.Versioning;
using Ecommerce.Api.Utils;
using Ecommerce.Application.Categories.CreateCategory;
using Ecommerce.Application.Categories.DeleteCategory;
using Ecommerce.Application.Categories.GetAllCategory;
using Ecommerce.Application.Categories.GetCategory;
using Ecommerce.Application.Categories.GetCategoryName;
using Ecommerce.Application.Categories.Shared;
using Ecommerce.Application.Categories.UpdateCategory;
using Ecommerce.Domain.Abstractions;
using Ecommerce.Infrastructure.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Categories;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/categories")]
public class CategoryController : ControllerBase
{
    private readonly ISender _sender;

    public CategoryController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
    [HttpGet()]
    public async Task<IActionResult> GetAllCategory(CancellationToken cancellationToken)
    {
        var query = new GetAllCategoryQuery();

        Result<IReadOnlyList<CategoryResponse>> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCategoryQuery(id);

        Result<CategoryResponse> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [AllowAnonymous]
    [HttpGet("{id}/name")]
    public async Task<IActionResult> GetCategoryName(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCategoryNameQuery(id);

        Result<CategoryNameResponse> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost()]
    [HasPermission(Permissions.CategoriesCreate)]
    public async Task<IActionResult> CreateCategory(CategoryCreateRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCategoryCommand(request.Name);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? 
            CreatedAtAction(nameof(GetCategory), new { id = result.Value }, result.Value) : 
            BadRequest(result.Error);
    }

    [HttpPut("{id}")]
    [HasPermission(Permissions.CategoriesUpdate)]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryUpdateRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateCategoryCommand(id, request.Name);

        Result result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpDelete("{id}")]
    [HasPermission(Permissions.CategoriesDelete)]
    public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCategoryCommand(id);

        Result result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }
}
