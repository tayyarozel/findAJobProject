using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Extensions
{
    //var olan bir nesneye kendi methodlarımızı yazma ve ekleme için Extensions denir
    //Biz burda .NET'TE yer alan "Claim" nesnesine kendi methodlarımızı ekledik
    public static class ClaimExtensions
    {
        //this ICollection<Claim> claims => eğer böyle bir yapı görürsen bilgi boyle bir nesne var biz ona kendi methodlarımızı ekliyoruz
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email,email));
        }

        public static void AddName(this ICollection<Claim> claims, string name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }

        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
        }

        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        {
            roles.ToList().ForEach(role=>claims.Add(new Claim(ClaimTypes.Role, role)));
        }
    }
}
