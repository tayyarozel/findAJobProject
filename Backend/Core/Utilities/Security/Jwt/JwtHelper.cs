using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encyption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Jwt
{
    //
    public class JwtHelper:ITokenHelper
    {
        public IConfiguration Configuration { get; } // WebApi katmanındaki "appsettings.json" dosyasını okumaya yariyor
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration; // token bitiş süresi
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();//appsettings.json dosyasındaki "TokenOptions"'si oku ve TokenOptions türünde maple
            
        }

        // Token oluşturma
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration); // token bitiş süresi
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey); //appsettings.json dosyasında yazan security keyi oku 
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);// hangi algoritma kullanıcaz ve anahtar ne onu veriyoruz
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims); //tokenin oluşması için gerekli olan parametreler
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler(); // token oluşturma için gerekli method
            var token = jwtSecurityTokenHandler.WriteToken(jwt); // token oluştu

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        //token üret
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, 
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer:tokenOptions.Issuer,
                audience:tokenOptions.Audience,
                expires:_accessTokenExpiration,
                notBefore:DateTime.Now,
                claims: SetClaims(user,operationClaims),// kullanici Operasyon Claimleri ve bunu liste seklinde oluştur.
                signingCredentials:signingCredentials
            );
            return jwt;
        }

        //Kullanıcı Operasyon claimlerini liste seklinde oluşturma
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            // burası .Net'de yer alan bir nesneye yeni methodlar eklenerek yazılmış hali bunları biz var olan bir nesneye kendi methodlarımız yazdık
            // Core katmanında yer alan Extensions klasörü içinde yer alıyor. oradan geliyorlar
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c=>c.Name).ToArray());
            
            return claims;
        }
    }
}
