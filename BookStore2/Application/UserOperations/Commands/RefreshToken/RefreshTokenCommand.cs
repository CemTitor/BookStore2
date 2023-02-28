using AutoMapper;
using BookStore2.DbOperations;
using BookStore2.Entities;
using BookStore2.TokenOperations;
using BookStore2.TokenOperations.Models;

namespace BookStore2.Application.UserOperations.Commands.RefreshToken
{

    public class RefreshTokenCommand
    {
        public string RefreshToken { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public RefreshTokenCommand(IBookStoreDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public Token Handle()
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken);
            if (user is not null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _dbContext.SaveChanges();
                return token;
            }
            else
                throw new InvalidOperationException("There is no valid refresh token!");
        }
    }
}