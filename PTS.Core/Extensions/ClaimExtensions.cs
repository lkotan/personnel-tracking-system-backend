using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PTS.Core.Extenstions
{
    public static class ClaimExtensions
    {
        public static void AddName(this ICollection<Claim> claims,string name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }

        public static void AddNameIdentifier(this ICollection<Claim> claims,string nameIdentifier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
        }

        public static void AddUserData(this ICollection<Claim> claims, string userData)
        {
            claims.Add(new Claim(ClaimTypes.UserData, userData));
        }

        public static void AddRoles(this ICollection<Claim> claims,string[] rules)
        {
            rules.ToList().ForEach(x => claims.Add(new Claim(ClaimTypes.Role, x)));
        }
    }
}
