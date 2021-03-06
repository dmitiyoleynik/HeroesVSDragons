﻿using DragonLibrary_.Services;
using GraphQL.Types;

namespace DragonLibrary_.Models
{
    public class HeroType : ObjectGraphType<Hero>
    {
        public HeroType()
        {
            Field(h => h.Id);
            Field(h => h.Name);
            Field<DateTimeGraphType>("Created");
            Field(h => h.Weapon);
        }
    }
}
