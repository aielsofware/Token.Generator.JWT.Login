namespace Token.Generator.JWT.Login.Models
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public TokenResponse(string token, DateTime expiration)
        {
            Token = token;
            Expiration = expiration;
        }
    }

    public class TokenResponsePremium
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }

    public class PremiumTokenResponse : TokenResponsePremium
    {
        public string UsageReport { get; set; }
    }

    public class TokenUsage
    {
        public int TokensGenerated { get; set; }
        public DateTime LastUsage { get; set; }
    }
}
