using DragonLibrary_.Services;
using GraphQL.Types;

namespace DragonLibrary_.Models
{
    public class HeroType :ObjectGraphType<Hero>
    {
        public HeroType(IHeroService heroService)
        {
            Field(h => h.Id);
            Field(h => h.Name);
            Field(h => h.Created);
            Field(h => h.Weapon);
        }
    }
}
