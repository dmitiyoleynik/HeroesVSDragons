using GraphQL.Types;

namespace DragonLibrary_.Models
{
    public class DamageStatisticType : ObjectGraphType<DamageStatistic>
    {
        public DamageStatisticType()
        {
            Field(d => d.DragonName);
            Field(d => d.SummDamage);
        }
    }
}
