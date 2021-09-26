using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OpenCleanArchitectureAccount.OIDC.Models
{
    public class ClaimsHelpers
    {     /// <summary>
          /// Set the claims for the user
          /// </summary>
          /// <typeparam name="TUser"></typeparam>
          /// <param name="user"></param>
          /// <param name="displayName">Who is the property to show default like user name in the Identity</param>
          /// <returns></returns>
        public static List<Claim> SetClaims<TUser>(string displayName, TUser user)
        {
            List<Claim> claims = new List<Claim>();
            //use reflexion to get dynamic the properties about the user object
            PropertyInfo[] properties = typeof(TUser).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            int c = properties.Length;
            int i = 0;
            for (i = 0; i < c; i++)
            {
                //get property name
                string myType = properties[i].Name;
                //get value of the property
                string myValue = properties[i].GetValue(user).ToString();

                //if it's Authentication Ignore don't insert in the claims
                claims.Add(new Claim(myType, myValue));
            }
            bool foundName = false;
            i = 0;
            do
            {
                //get property name
                string myType = properties[i].Name;
                //get value of the property
                string myValue = properties[i].GetValue(user).ToString();

                if (myType.ToLower() == displayName.ToLower())
                {
                    claims.Add(new Claim(ClaimTypes.Name, myValue));
                    foundName = true;
                }
                i++;
            } while (!foundName && i < c);

            if (!foundName)
            {
                //not found DisplayName property, search a email
                Claim n = claims.Find(n => n.Value.Contains("@"));
                if (n != null) claims.Add(new Claim(ClaimTypes.Name, n.Value));
                else        //if nothing found set the first property like display name
                    claims.Add(new Claim(ClaimTypes.Name, claims[0].Value));
            }

            return claims;
        }

    }
}
