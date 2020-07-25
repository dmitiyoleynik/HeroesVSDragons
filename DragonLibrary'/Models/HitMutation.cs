using DragonLibrary_.Services;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonLibrary_.Models
{
    public class HitMutation : ObjectGraphType<object>
    {
        public HitMutation(IHitService hitService,
            IJWTService jWTService,
            IHeroService heroService,
            IValidatorService validatorService)
        {
            Name = "Mutation";

            Field<StringGraphType>(
                "createHit",
                arguments: new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "token" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "dragonId" }
                },
                resolve: context =>
                {
                    var token = context.GetArgument<string>("token");
                    var dragonId = context.GetArgument<int>("dragonId");
                    validatorService.ValidateToken(token);

                    var heroName = jWTService.GetHeroNameFromToken(token);
                    var hero = heroService.GetHeroByName(heroName);

                    hitService.CreateHit(hero, dragonId);

                    return null;
                });
        }
    }
}
