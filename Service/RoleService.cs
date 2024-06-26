using UMS.Contracts;
using UMS.Service.Contracts;
using AutoMapper;
using UMS.Entities.Exceptions;
using UMS.Entities.Models;
using UMS.Shared.DataTransferObjects;

namespace UMS.Service;

internal sealed class RoleService : IRoleService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public RoleService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<IEnumerable<RoleDTO>> GetAllRolesAsync(bool trackChanges)
    {
        var roles =
        await _repository.Role.GetAllRolesAsync(trackChanges);
        var rolesDTO = _mapper.Map<IEnumerable<RoleDTO>>(roles);
        return rolesDTO;
    }
    public async Task<RoleDTO> GetRoleAsync(Guid id, bool trackChanges)
    {
        var role = await _repository.Role.GetRoleAsync(id, trackChanges);
        if (role is null)
            throw new RoleNotFoundException(id);

        var roleDTO = _mapper.Map<RoleDTO>(role);
        return roleDTO;
    }
    public async Task<RoleDTO> CreateRoleAsync(RoleForPostDTO role)
    {
        var roleEntity = _mapper.Map<Role>(role);
        _repository.Role.CreateRole(roleEntity);
        await _repository.SaveAsync();
        var roleToReturn = _mapper.Map<RoleDTO>(roleEntity);
        return roleToReturn;
    }
    public async Task DeleteRoleAsync(Guid id,bool trackChanges)
    {
        var role = await _repository.Role.GetRoleAsync(id, trackChanges);
        if (role is null)
            throw new RoleNotFoundException(id);
        _repository.Role.DeleteRole(role);
        await _repository.SaveAsync();
    }

    public async Task UpdateRoleAsync(Guid id, RoleForUpdateDTO role, bool trackChanges)
    {
        var roleEntity = await _repository.Role.GetRoleAsync(id, trackChanges);
        if(roleEntity is null)
            throw new RoleNotFoundException(id);
        _mapper.Map(role, roleEntity);
        await _repository.SaveAsync();
    }

}
