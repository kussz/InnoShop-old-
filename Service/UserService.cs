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
        await CheckIfRoleExists(roleId, trackChanges);
        var users = await _repository.User.GetUsersAsync(roleId, trackChanges);
        var usersDTO = _mapper.Map<IEnumerable<UserDTO>>(users);
        return usersDTO;
    }
    public async Task< UserDTO> GetUserAsync(Guid roleId, Guid userId, bool trackChanges)
    {
        await CheckIfRoleExists(roleId, trackChanges);
        var user = await GetUserAndCheckIfItExists(roleId, userId, trackChanges);
        return _mapper.Map<UserDTO>(user);
    }
    public async Task< UserDTO> CreateUserAsync(UserForPostDTO user, Guid roleId,bool trackChanges)
    {
        await CheckIfRoleExists(roleId, trackChanges);
        var userEntity = _mapper.Map<User>(user);
        _repository.User.CreateUser(roleId, userEntity);
        await _repository.SaveAsync();
        return _mapper.Map<UserDTO>(userEntity);

    }
    public async Task DeleteUserAsync(Guid roleId,Guid userId, bool trackChanges)
    {
        await CheckIfRoleExists(roleId,trackChanges);
        var user = await GetUserAndCheckIfItExists(roleId,userId,trackChanges);
        _repository.User.DeleteUser(user);
        await _repository.SaveAsync();
    }
    public async Task UpdateUserAsync(Guid roleId, Guid id, UserForUpdateDTO user, bool roleTrackChanges, bool userTrackChanges)
    {
        await CheckIfRoleExists(roleId,roleTrackChanges);
        var userEntity = await GetUserAndCheckIfItExists(roleId,id,userTrackChanges);
        _mapper.Map(user,userEntity);
        await _repository.SaveAsync();
    }

    private async Task CheckIfRoleExists(Guid roleId, bool trackChanges)
    {
        var role = await _repository.Role.GetRoleAsync(roleId,
        trackChanges);
        if (role is null)
            throw new RoleNotFoundException(roleId);
    }
    private async Task<User> GetUserAndCheckIfItExists
    (Guid roleId, Guid id, bool trackChanges)
    {
        var user = await _repository.User.GetUserAsync(roleId, id,
        trackChanges);
        if (user is null)
            throw new UserNotFoundException(id);
        return user;
    }
}
