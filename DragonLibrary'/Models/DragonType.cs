using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonLibrary_.Models
{
    public class DragonType:ObjectGraphType<Dragon>
    {
        public DragonType()
        {
            Field(d => d.Id);
            Field(d => d.Name);
            Field(d => d.Hp);
            Field(d => d.MaxHp);
            Field(d => d.Created);
            Field(d => d.Died);

        }
    }
}
