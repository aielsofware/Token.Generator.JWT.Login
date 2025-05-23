﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Token.Generator.JWT.Login.Models;

namespace Token.Generator.JWT.Login
{
    // Token Generator
    public interface ITokenService
    {
        string BasicToken(string SecretKey);
        string ProToken(string issuer, string audience, string clientUsername, string SecretKey);
        Task<string> PremiumTokenAsync(string key, string issuer, string audience, string clientUsername, string SecretKey);
    }

    public class TokenService : ITokenService
    {
        private readonly TimeSpan ExpiryDuration = new TimeSpan(0, 30, 0);
        private const string GitHubKeysUrl = "https://raw.githubusercontent.com/rellytechgame/key_TokenGeneratorJWT/refs/heads/main/key.txt?token=GHSAT0AAAAAADCCPLMUGLVOTBRK623DNKZO2AQYC5A"; // URL con las claves


        // Caché para almacenar el uso de tokens por usuario
        private static readonly Dictionary<string, TokenUsage> TokenUsageCache = new Dictionary<string, TokenUsage>();

        public string BasicToken(string SecretKey)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            int KeySize = IsKeySizeValid(securityKey);

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            if (KeySize < 256)
            {
                return $"Your secret key size need must greater than {SecretKey}";
            }
            else if (KeySize >= 256 && KeySize < 383)
            {
                credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha384Signature);
            }
            else if (KeySize >= 383 && KeySize < 512)
            {
                credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha384Signature);
            }
            else if (KeySize >= 512)
            {
                credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            }


            var tokenDescriptor = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.Add(ExpiryDuration),
                signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return token;
        }

        public string ProToken(string issuer, string audience, string clientUsername, string SecretKey)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, clientUsername),
                new Claim(ClaimTypes.NameIdentifier,
                Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            int KeySize = IsKeySizeValid(securityKey);

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            if (KeySize < 256)
            {
                return $"Your secret key size need must greater than {SecretKey}";
            }
            else if (KeySize >= 256 && KeySize < 383)
            {
                credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha384Signature);
            }
            else if (KeySize >= 383 && KeySize < 512)
            {
                credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha384Signature);
            }
            else if (KeySize >= 512)
            {
                credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            }

            var tokenDescriptor = new JwtSecurityToken(issuer, audience, claims,

            expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            var tokenResponse = new TokenResponse(token, DateTime.Now.Add(ExpiryDuration));

            var response = JsonSerializer.Serialize(tokenResponse);

            return response;
        }

        public async Task<string> PremiumTokenAsync(string key, string issuer, string audience, string clientUsername, string SecretKey)
        {
            if (!await ValidateKeyAsync(key))
            {
                return "Invalid key. Access denied.";
            }

            // Generar token premium con características adicionales
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, clientUsername),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim("PremiumUser", "true")
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            int KeySize = IsKeySizeValid(securityKey);

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            if (KeySize < 256)
            {
                return $"Your secret key size need must greater than {SecretKey}";
            }
            else if (KeySize >= 256 && KeySize < 383)
            {
                credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha384Signature);
            }
            else if (KeySize >= 383 && KeySize < 512)
            {
                credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha384Signature);
            }
            else if (KeySize >= 512)
            {
                credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            }

            var expiration = DateTime.Now.Add(TimeSpan.FromDays(30)); // Mayor duración para usuarios premium
            var tokenDescriptor = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: expiration,
                signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            // Actualizar el caché de uso del token
            UpdateTokenUsage(clientUsername);

            var response = new PremiumTokenResponse
            {
                Token = token,
                Expiration = expiration,
                UsageReport = GenerateUsageReport(clientUsername)
            };

            return JsonSerializer.Serialize(response);
        }

        private async Task<bool> ValidateKeyAsync(string key)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(GitHubKeysUrl);
            var validKeys = response.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return Array.Exists(validKeys, k => k == key);
        }

        private string GenerateUsageReport(string clientUsername)
        {
            if (TokenUsageCache.TryGetValue(clientUsername, out var usage))
            {
                return JsonSerializer.Serialize(usage);
            }
            return "{}"; // Retorna un JSON vacío si no hay datos de uso
        }

        private void UpdateTokenUsage(string clientUsername)
        {
            if (TokenUsageCache.ContainsKey(clientUsername))
            {
                TokenUsageCache[clientUsername].TokensGenerated++;
                TokenUsageCache[clientUsername].LastUsage = DateTime.Now;
            }
            else
            {
                TokenUsageCache[clientUsername] = new TokenUsage
                {
                    TokensGenerated = 1,
                    LastUsage = DateTime.Now
                };
            }
        }

        public void ClearCache()
        {
            TokenUsageCache.Clear();
        }

        public static int IsKeySizeValid(SymmetricSecurityKey key)
        {
            int KeySize = key.KeySize;

            return KeySize;
        }


    }
}
