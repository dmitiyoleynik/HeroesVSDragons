using GraphQL.Types;

namespace DragonLibrary_.Models
{
    public class HitType: ObjectGraphType<Hit>
    {
        public HitType()
        {
            Field(h => h.HeroId);
            Field(h => h.DragonId);
            Field(h => h.Power);
            Field(h => h.ExecutionTime);
        }
    }
}
