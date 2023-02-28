namespace BookStore2.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        /// <summary>
        /// Refresh token is used to generate new access token.
        /// </summary>
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpireDate { get; set; }
    }
}