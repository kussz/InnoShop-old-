using Microsoft.AspNetCore.Mvc;
using UMS.Shared.DataTransferObjects;
using UMS.Service.Contracts;
using UMS.ActionFilters;

namespace UMS.Presentaion;

[Route("api/roles")]
[ApiController]
//[ResponseCache(CacheProfileName = "120SecondsDuration")]
public class RolesController : ControllerBase
{
    private readonly IServiceManager _service;
    public RolesController(IServiceManager service) => _service = service;
    [HttpGet(Name ="GetRoles")]
    public async Task<IActionResult> GetRoles()
    {
        var companies = await
        _service.RoleService.GetAllRolesAsync(trackChanges: false);
        return Ok(companies);
    }
    [HttpGet("{id:guid}",Name ="RoleById")]

    public async Task<IActionResult> GetRole(Guid id)
    {
        var role = await _service.RoleService.GetRoleAsync(id, trackChanges: false);

        return Ok(role);
    }
    [HttpPost(Name ="CreateRoles")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task< IActionResult> CreateRole([FromBody] RoleForPostDTO role)
    {
        var createdCompany = await _service.RoleService.CreateRoleAsync(role);
        return CreatedAtRoute("RoleById", new {id = createdCompany.Id},createdCompany);
    }
    [HttpDelete("{id:guid}")]
    public async Task< IActionResult> DeleteRole(Guid id)
    {
        await _service.RoleService.DeleteRoleAsync(id, false);
        return NoContent();
    }
    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task< IActionResult> UpdateRole(Guid id, [FromBody] RoleForUpdateDTO role)
    {
        await _service.RoleService.UpdateRoleAsync(id, role, true);
        return NoContent();
    }
    [HttpOptions]
    public IActionResult GetRolesOptions()
    {
        Response.Headers.Add("Allow", "GET, OPTIONS, POST");
        return Ok();
    }


}
