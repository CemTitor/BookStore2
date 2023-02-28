using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BookStore2.Entities;
using BookStore2.TokenOperations.Models;
using Microsoft.IdentityModel.Tokens;

namespace BookStore2.TokenOperations
{
    public class TokenHandler
    {
        public IConfiguration Configuration { get; set; }
        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Token CreateAccessToken(User user)
        {
            Token tokenModel = new Token();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenOptions:SecurityKey"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            tokenModel.Expiration = DateTime.Now.AddMinutes(15);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: Configuration["TokenOptions:Issuer"],
                audience: Configuration["TokenOptions:Audience"],
                expires: tokenModel.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: credentials
            );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            ///Token is created
            tokenModel.AccessToken = handler.WriteToken(securityToken);
            tokenModel.RefreshToken = CreateRefreshToken();
            tokenModel.Expiration = tokenModel.Expiration.AddMinutes(5);
            return tokenModel;
        }

        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

    }
}