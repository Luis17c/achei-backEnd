using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Models;


namespace Utils {
    public class Token {
        public static string tokenSecret = WebApplication.CreateBuilder().Configuration["Token:Key"] ?? throw new Exception("JWT key missing");
        public static string Generate(User user) {
            
            var key = Encoding.ASCII.GetBytes(tokenSecret);
            var tokenConfig = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[] {
                    new ("userId", user.id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
        public static int GetUserId(HttpContext httpContext) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenSecret);      
            
            tokenHandler.ValidateToken(
                httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()
                , new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedToken
            );

            var jwtToken = (JwtSecurityToken)validatedToken;

            int userId = int.Parse(jwtToken.Claims.First(x => x.Type == "userId").Value);

            return userId;
        }
    }
}