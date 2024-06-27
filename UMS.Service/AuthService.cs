using AutoMapper;
using System.ComponentModel.Design;
using UMS.Contracts;
using UMS.Service.Contracts;
using UMS.Entities.Exceptions;
using UMS.Shared.DataTransferObjects;
using UMS.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;
namespace UMS.Service;

internal sealed class AuthService : IAuthService
{
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private User? _user;
    private readonly UserManager<User> _userManager;
    public AuthService(IConfiguration configuration, ILoggerManager logger, IMapper mapper, UserManager<User> userManager)
    {
        _userManager = userManager;
        _configuration = configuration;
        _logger = logger;
        _mapper = mapper;
    }
    //public async Task<(IEnumerable<UserDTO> users, MetaData metaData)> GetUsersAsync(Guid roleId, UserParameters userParams, bool trackChanges)
    //{
    //    await CheckIfRoleExists(roleId, trackChanges);
    //    var usersWithMetaData = await _repository.User.GetUsersAsync(roleId,userParams, trackChanges);

    //    var usersDTO = _mapper.Map<IEnumerable<UserDTO>>(usersWithMetaData);
    //    return (users: usersDTO, metaData: usersWithMetaData.MetaData);
    //}
    //public async Task< UserDTO> GetUserAsync(Guid roleId, Guid userId, bool trackChanges)
    //{
    //    //await CheckIfRoleExists(roleId, trackChanges);
    //    var user = await GetUserAndCheckIfItExists(roleId, userId, trackChanges);
    //    return _mapper.Map<UserDTO>(user);
    //}
    public async Task<IdentityResult> RegisterUserAsync(UserForRegDTO userForReg)
    {
        var user = _mapper.Map<User>(userForReg);
        var result = await _userManager.CreateAsync(user,userForReg.Password);
        if (result.Succeeded)
            await _userManager.AddToRolesAsync(user, userForReg.Roles);
        return result;

    }
    public async Task<bool> ValidateUser(UserForAuthDTO userForAuth)
    {
        _user = await _userManager.FindByNameAsync(userForAuth.UserName);
        var result = (_user != null && await _userManager.CheckPasswordAsync(_user,
        userForAuth.Password));
        if (!result)
            _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong username or password.");
        return result;
    }
    public async Task<string> CreateToken()
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
    private SigningCredentials GetSigningCredentials()
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(jwtSettings["validKey"]);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }
    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>
         {
         new Claim(ClaimTypes.Name, _user.UserName)
         };
        var roles = await _userManager.GetRolesAsync(_user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return claims;
    }
    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,
List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var tokenOptions = new JwtSecurityToken
        (
        issuer: jwtSettings["validIssuer"],
        audience: jwtSettings["validAudience"],
        claims: claims,
        expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
        signingCredentials: signingCredentials
        );
        return tokenOptions;
    }

    //public async Task DeleteUserAsync(Guid roleId,Guid userId, bool trackChanges)
    //{
    //    await CheckIfRoleExists(roleId,trackChanges);
    //    var user = await GetUserAndCheckIfItExists(roleId,userId,trackChanges);
    //    _repository.User.DeleteUser(user);
    //    await _repository.SaveAsync();
    //}
    //public async Task UpdateUserAsync(Guid roleId, Guid id, UserForUpdateDTO user, bool roleTrackChanges, bool userTrackChanges)
    //{
    //    await CheckIfRoleExists(roleId,roleTrackChanges);
    //    var userEntity = await GetUserAndCheckIfItExists(roleId,id,userTrackChanges);
    //    _mapper.Map(user,userEntity);
    //    await _repository.SaveAsync();
    //}

    //private async Task CheckIfRoleExists(Guid roleId, bool trackChanges)
    //{
    //    var role = await _repository.Role.GetRoleAsync(roleId,
    //    trackChanges);
    //    if (role is null)
    //        throw new RoleNotFoundException(roleId);
    //}
    //private async Task<User> GetUserAndCheckIfItExists
    //(Guid roleId, Guid id, bool trackChanges)
    //{
    //    var user = await _repository.User.GetUserAsync(roleId, id,
    //    trackChanges);
    //    if (user is null)
    //        throw new UserNotFoundException(id);
    //    return user;
    //}
}
