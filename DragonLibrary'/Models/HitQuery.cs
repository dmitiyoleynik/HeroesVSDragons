using DragonLibrary_.Services;
using GraphQL.Types;
using System;

namespace DragonLibrary_.Models
{
    public class HitQuery : ObjectGraphType<object>
    {
        public HitQuery(IHitService hitService,
            IHeroService heroService,
            IJWTService jWTService,
            IValidatorService validatorService)
        {
            Name = "Query";
            Field<ListGraphType<DamageStatisticType>>(
                "getHits",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "token" }
                    ),
                resolve: context =>
                {
                    var token = context.GetArgument<string>("token");
                    validatorService.ValidateToken(token);

                    var heroName = jWTService.GetHeroNameFromToken(token);
                    var heroId = heroService.GetHeroByName(heroName).Id;

                    return hitService.GetHeroDamageStatistic(heroId);
                });
            //Field<ListGraphType<HitType>>(
            //    "getHits",
            //    resolve: context => hitService.GetHitsAsync());
        }
    }
}
