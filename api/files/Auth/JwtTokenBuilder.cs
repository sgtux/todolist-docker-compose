using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Api.Auth
{
    public class JwtTokenBuilder
    {
        private readonly int _tokenExpiryMinutes;

        private readonly SecurityKey _key;

        private Dictionary<string, string> _claims;

        public JwtTokenBuilder(string key, int tokenExpiryMinutes, Dictionary<string, string> claims)
        {
            _key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            _claims = claims;
            _tokenExpiryMinutes = tokenExpiryMinutes;
        }

        public JwtToken Build()
        {
            var token = new JwtSecurityToken(
              claims: _claims.Select(item => new Claim(item.Key, item.Value)).ToList(),
                expires: DateTime.UtcNow.AddMinutes(_tokenExpiryMinutes),
                signingCredentials: new SigningCredentials(
                    _key,
                    SecurityAlgorithms.HmacSha256
                )
            );
            return new JwtToken(token);
        }
    }
}