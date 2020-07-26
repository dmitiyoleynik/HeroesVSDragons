using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonLibrary_.Models
{
    public class DamageStatisticType : ObjectGraphType<DamageStatistic>
    {
        public DamageStatisticType()
        {
            Field(d => d.DragonId);
            Field(d => d.SummDamage);
        }
    }
}
