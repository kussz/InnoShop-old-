using Microsoft.AspNetCore.Mvc;
using UMS.Shared.DataTransferObjects;
using UMS.Service.Contracts;

namespace UMS.Presentaion;

[Route("api/roles")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IServiceManager _service;
    public RolesController(IServiceManager service) => _service = service;
    [HttpGet]
    public IActionResult GetRoles()
    {
        var companies =
        _service.RoleService.GetAllRolesAsync(trackChanges: false);
        return Ok(companies);
    }
    [HttpGet("{id:guid}",Name ="RoleById")]
    public IActionResult GetRole(Guid id)
    {
        var role = _service.RoleService.GetRoleAsync(id, trackChanges: false);

        return Ok(role);
    }
    [HttpPost]
    public IActionResult CreateRole([FromBody] RoleForPostDTO role)
    {
        if(role is null)
            return BadRequest("RoleForPostDTO is null");
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var createdCompany = _service.RoleService.CreateRoleAsync(role);
        return CreatedAtRoute("RoleById", new {id = createdCompany.Id},createdCompany);
    }
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteRole(Guid id)
    {
        _service.RoleService.DeleteRoleAsync(id, false);
        return NoContent();
    }
    [HttpPut("{id:guid}")]
    public IActionResult UpdateRole(Guid id, [FromBody] RoleForUpdateDTO role)
    {
        if (role is null)
            return BadRequest("RoleForUpdateDTO is null");
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        _service.RoleService.UpdateRoleAsync(id, role,true);
        return NoContent();
    }

}
