using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.WebApp
{
    public class AuthOptions
    {
        public string ISSUER;

        public string AUDIENCE;

        public string KEY;

        public int LIFETIME;

        public static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }


    }
}
