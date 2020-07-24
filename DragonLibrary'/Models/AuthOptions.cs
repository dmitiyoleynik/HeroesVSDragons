﻿using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DragonLibrary_.Models
{
    public class AuthOptions
    {
        const string KEY = "mysupersecret_secretkey!123";   
        public const int LIFETIME = 20; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
