//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;
//using UserManagementService.Models;
//using MailKit.Net.Smtp;
//using MimeKit;

//namespace Service;

//public interface IAuthService
//{
//    string GenerateToken(User user);
//}

//public class AuthService : IAuthService
//{
//    private readonly IConfiguration _configuration;

//    public AuthService(IConfiguration configuration)
//    {
//        _configuration = configuration;
//    }

//    public string GenerateToken(User user)
//    {
//        var tokenHandler = new JwtSecurityTokenHandler();
//        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
//        var tokenDescriptor = new SecurityTokenDescriptor
//        {
//            Subject = new ClaimsIdentity(new[]
//            {
//                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//                new Claim(ClaimTypes.Name, user.Name),
//                new Claim(ClaimTypes.Email, user.Email),
//                new Claim(ClaimTypes.Role, user.Role.Name)
//            }),
//            Expires = DateTime.UtcNow.AddDays(7),
//            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
//            Issuer = _configuration["Jwt:Issuer"],
//            Audience = _configuration["Jwt:Audience"]
//        };

//        var token = tokenHandler.CreateToken(tokenDescriptor);
//        return tokenHandler.WriteToken(token);
//    }
//    public string GeneratePasswordResetToken(User user)
//    {
//        using var rng = new RNGCryptoServiceProvider();
//        var tokenData = new byte[32];
//        rng.GetBytes(tokenData);
//        return Convert.ToBase64String(tokenData);
//    }

//    public void SendEmail(string email, string subject, string message)
//    {
//        var emailMessage = new MimeMessage();
//        emailMessage.From.Add(new MailboxAddress("Support", "support@example.com"));
//        emailMessage.To.Add(new MailboxAddress("", email));
//        emailMessage.Subject = subject;
//        emailMessage.Body = new TextPart("plain") { Text = message };

//        using var client = new SmtpClient();
//        client.Connect("smtp.example.com", 587, false); // Укажите ваш SMTP сервер
//        client.Authenticate("your_email@example.com", "your_password"); // Укажите ваши учетные данные
//        client.Send(emailMessage);
//        client.Disconnect(true);
//    }
//}
