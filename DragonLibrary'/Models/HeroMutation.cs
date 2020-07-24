using DragonLibrary_.Services;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonLibrary_.Models
{
    public class HeroMutation : ObjectGraphType<object>
    {
        public HeroMutation(IHeroService heroService)
        {
            Name = "Mutation";
            Field<StringGraphType>(
                "createHero",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "heroName" }),
                resolve: context =>
                {
                    var heroName = context.GetArgument<string>("heroName");
                    return heroService.CreateHeroAsync(heroName);
                });
        }
    }
}
