using System;

namespace DragonLibrary_.Services
{
    public class ValidatorService : IValidatorService
    {
        private readonly IJWTService _jWTService;
        private readonly IHeroService _heroService;

        public ValidatorService(IJWTService jWTService,
            IHeroService heroService)
        {
            _heroService = heroService;
            _jWTService = jWTService;
        }
        public void ValidateToken(string token)
        {
            var tokenOwnerName = _jWTService.GetHeroNameFromToken(token);

            if (!_heroService.IsHeroExists(tokenOwnerName))
            {
                throw new Exception("Token invalid.");
            }
        }
    }
}
