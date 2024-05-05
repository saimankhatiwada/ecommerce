using Asp.Versioning;
using Ecommerce.Api.Utils;
using Ecommerce.Application.SubCategories.CreateSubCategory;
using Ecommerce.Application.SubCategories.DeleteSubCategory;
using Ecommerce.Application.SubCategories.GetAllSubCategory;
using Ecommerce.Application.SubCategories.GetSubCategory;
using Ecommerce.Application.SubCategories.Shared;
using Ecommerce.Application.SubCategories.UpdateSubCategory;
using Ecommerce.Domain.Abstractions;
using Ecommerce.Infrastructure.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.SubCategories;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/sub-categories")]
public class SubCategoryController : ControllerBase
{
    private readonly ISender _sender;

    public SubCategoryController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
    [HttpGet()]
    public async Task<IActionResult> GetAllSubCategory(CancellationToken cancellationToken)
    {
        var query = new GetAllSubCategoryQuery();

        Result<IReadOnlyList<SubCategoryResponse>> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubCategory(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetSubCategoryQuery(id);

        Result<SubCategoryResponse> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost()]
    [HasPermission(Permissions.SubCategoriesCreate)]
    public async Task<IActionResult> CreateSubCategory(SubCategoryCreateRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateSubCategoryCommand(request.Name, request.CategoryId);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? 
            CreatedAtAction(nameof(GetSubCategory), new { id = result.Value }, result.Value) : 
            BadRequest(result.Error);
    }

    [HttpPut("{id}")]
    [HasPermission(Permissions.SubCategoriesUpdate)]
    public async Task<IActionResult> UpdateSubCategory(Guid id, [FromBody] SubCategoryUpdateRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateSubCategoryCommand(id, request.Name, request.CategoryId);

        Result result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpDelete("{id}")]
    [HasPermission(Permissions.SubCategoriesDelete)]
    public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteSubCategoryCommand(id);

        Result result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }
}
