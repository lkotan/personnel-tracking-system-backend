using Microsoft.IdentityModel.Tokens;
using PTS.Core.Extenstions;
using PTS.Core.Helpers;
using PTS.Core.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PTS.Core.Plugins.Authentication.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        private readonly JwtOptions _tokenOptions;
        private DateTime _accessTokenExpiration;

        public JwtHelper(JwtOptions tokenOptions)
        {
            _tokenOptions = tokenOptions;
        }

        public AccessToken CreateToken(int accountId,List<PersonnelRulesModel> rules)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

            var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwt = CreateJwtSecurityToken(_tokenOptions, accountId, signinCredentials, rules);

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var token = jwtSecurityTokenHandler.WriteToken(jwt);
            return new AccessToken
            {
                Token = token,
                RefreshToken = Helper.CreateToken(),
                TokenExpiration = _accessTokenExpiration,
            };
        }

        private JwtSecurityToken CreateJwtSecurityToken(JwtOptions tokenOptions,int accountId,SigningCredentials signingCredentials,List<PersonnelRulesModel> rules)
        {
            var jwt = new JwtSecurityToken(
                issuer:tokenOptions.Issuer,
                audience:tokenOptions.Audience,
                expires:_accessTokenExpiration,
                notBefore:DateTime.Now,
                claims:SetClaims(accountId, rules),
                signingCredentials:signingCredentials
                );
            return jwt;
        }

        private static IEnumerable<Claim> SetClaims(int accountId,List<PersonnelRulesModel> roles)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(accountId.ToString());
            claims.AddRoles(roles.Select(x => x.ApplicationModule.ToString()).ToArray());
            return claims;
        }
    }
}
