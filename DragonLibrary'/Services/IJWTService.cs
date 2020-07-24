using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonLibrary_.Services
{
    public interface IJWTService
    {
        public string GetToken(string heroName);
        public string GetHeroFromToken(string token);
    }
}
