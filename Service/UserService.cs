using AutoMapper;
using System.ComponentModel.Design;
using UMS.Contracts;
using UMS.Service.Contracts;
using UMS.Entities.Exceptions;
using UMS.Shared.DataTransferObjects;
using UMS.Entities.Models;
namespace UMS.Service;

internal sealed class UserService : IUserService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public UserService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<IEnumerable<UserDTO>> GetUsersAsync(Guid roleId, bool trackChanges)
    {
        var role = await _repository.Role.GetRoleAsync(roleId, trackChanges);
        if (role is null)
            throw new RoleNotFoundException(roleId);
        var users = await _repository.User.GetUsersAsync(roleId, trackChanges);
        var usersDTO = _mapper.Map<IEnumerable<UserDTO>>(users);
        return usersDTO;
    }
    public async Task< UserDTO> GetUserAsync(Guid roleId, Guid userId, bool trackChanges)
    {
        var role = await _repository.Role.GetRoleAsync(roleId, false);
        if (role is null)
            throw new RoleNotFoundException(roleId);
        var user = await _repository.User.GetUserAsync(roleId, userId, trackChanges);
        if(user is null)
            throw new UserNotFoundException(userId);
        return _mapper.Map<UserDTO>(user);
    }
    public async Task< UserDTO> CreateUserAsync(UserForPostDTO user, Guid roleId,bool trackChanges)
    {
        var role = await _repository.Role.GetRoleAsync(roleId,trackChanges);
        if(role is null)
            throw new RoleNotFoundException(roleId);
        var userEntity = _mapper.Map<User>(user);
        _repository.User.CreateUser(role, userEntity);
        await _repository.SaveAsync();
        return _mapper.Map<UserDTO>(userEntity);

    }
    public async Task DeleteUserAsync(Guid roleId,Guid userId, bool trackChanges)
    {
        var role = await _repository.Role.GetRoleAsync(roleId,trackChanges);
        if(role is null)
            throw new RoleNotFoundException(roleId);
        var user = await _repository.User.GetUserAsync(roleId, userId, trackChanges);
        if(user is null)
            throw new UserNotFoundException(userId);
        _repository.User.DeleteUser(user);
        await _repository.SaveAsync();
    }
    public async Task UpdateUserAsync(Guid roleId, Guid id, UserForUpdateDTO user, bool roleTrackChanges, bool userTrackChanges)
    {
        var role = await _repository.Role.GetRoleAsync(roleId, roleTrackChanges);
        if(role == null)
            throw new RoleNotFoundException(roleId);
        var userEntity = await _repository.User.GetUserAsync(roleId, id, userTrackChanges);
        if(userEntity is null)
            throw new UserNotFoundException(id);
        _mapper.Map(user,userEntity);
        await _repository.SaveAsync();
    }

}
