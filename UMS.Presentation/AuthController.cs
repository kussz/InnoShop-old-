using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

//using Microsoft.EntityFrameworkCore;
//using Repository;
using System.Runtime.InteropServices;
using UMS.ActionFilters;
using UMS.Service.Contracts;
using UMS.Shared.DataTransferObjects;
using System.Text.Json;

namespace UMS.Presentation;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IServiceManager _service;
    public AuthController(IServiceManager service) => _service = service;
    //[HttpGet]
    //[HttpHead]
    //public async Task< IActionResult> GetUsers(Guid roleId, [FromQuery]UserParameters userParameters)
    //{
    //    var pagedResult = await _service.AuthService.GetUsersAsync(roleId,userParameters, trackChanges: false);
    //    Response.Headers.Add("X-Pagination",
    //    JsonSerializer.Serialize(pagedResult.metaData));
    //    return Ok(pagedResult.users);
    //}
    //[HttpGet("{id:guid}", Name ="GetUserById")]
    //public async Task<IActionResult> GetUser(Guid roleId, Guid id)
    //{
    //    var user = await _service.AuthService.GetUserAsync(roleId, id, false);
    //    return Ok(user);
    //}
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task< IActionResult> RegisterUser([FromBody] UserForRegDTO userFoReg)
    {
        var result =
        await _service.AuthService.RegisterUserAsync(userFoReg);
        if(!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }
        return StatusCode(201);
    }
    [HttpPost("login")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Authenticate([FromBody] UserForAuthDTO user)
    {
        if (!await _service.AuthService.ValidateUser(user))
            return Unauthorized();
        return Ok(new
        {
            Token = await _service
        .AuthService.CreateToken()
        });
    }
    //[HttpDelete("{id:guid}")]
    //public async Task< IActionResult> DeleteUser(Guid roleId, Guid id)
    //{
    //    await _service.AuthService.DeleteUserAsync(roleId, id, false);
    //    return NoContent();
    //}
    //[HttpPut("{id:guid}")]
    //[ServiceFilter(typeof(ValidationFilterAttribute))]
    //public async Task<IActionResult> UpdateUser(Guid roleId, Guid id, [FromBody] UserForUpdateDTO user)
    //{
    //    await _service.AuthService.UpdateUserAsync(roleId, id, user, false, true);
    //    return NoContent();
    //}
}
//    private readonly RepositoryContext _context;
//    public UsersController(RepositoryContext context)
//    {
//        _context = context;
//    }
//    [HttpGet("{id}")]
//    public async Task<ActionResult<User>> GetUser(int id)
//    {
//        var user = await _context.Users.FindAsync(id);
//        if (user == null)
//        {
//            return NotFound();
//        }

//        return user;
//    }

//    [HttpPost]
//    public async Task<ActionResult<User>> CreateUser(User user)
//    {
//        _context.Users.Add(user);
//        await _context.SaveChangesAsync();

//        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
//    }

//    [HttpPut("{id}")]
//    public async Task<IActionResult> UpdateUser(Guid id, User user)
//    {
//        if (id != user.Id)
//        {
//            return BadRequest();
//        }

//        _context.Entry(user).State = EntityState.Modified;
//        await _context.SaveChangesAsync();

//        return NoContent();
//    }

//    [HttpDelete("{id}")]
//    public async Task<IActionResult> DeleteUser(int id)
//    {
//        var user = await _context.Users.FindAsync(id);
//        if (user == null)
//        {
//            return NotFound();
//        }

//        _context.Users.Remove(user);
//        await _context.SaveChangesAsync();

//        return NoContent();
//    }
//    [HttpPost("forgot-password")]
//    public async Task<IActionResult> ForgotPassword(string email)
//    {
//        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
//        if (user == null)
//        {
//            return NotFound();
//        }

//        //var token = _authService.GeneratePasswordResetToken(user);
//        // Генерация токена для сброса пароля и отправка по электронной почте
//        // (реализация отправки письма с токеном сброса пароля должна быть добавлена)
//        //var resetLink = Url.Action("ResetPassword", "Users", new { token = token, email = user.Email }, Request.Scheme);
//        //var message = $"Для сброса пароля, пожалуйста, перейдите по ссылке: {resetLink}";

//        //_authService.SendEmail(user.Email, "Сброс пароля", message);

//        return Ok();
//    }

//    [HttpPost("confirm-email")]
//    public async Task<IActionResult> ConfirmEmail(int userId, string token)
//    {
//        var user = await _context.Users.FindAsync(userId);
//        if (user == null)
//        {
//            return NotFound();
//        }


//        // Проверка токена и подтверждение электронной почты
//        // (реализация проверки токена должна быть добавлена)

//        user.EmailConfirmed = true;
//        await _context.SaveChangesAsync();

//        return Ok();
//    }

//}
