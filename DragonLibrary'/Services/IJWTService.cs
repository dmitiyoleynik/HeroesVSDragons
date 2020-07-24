namespace DragonLibrary_.Services
{
    public interface IJWTService
    {
        public string GetToken(string heroName);
        public string GetHeroNameFromToken(string token);
    }
}
