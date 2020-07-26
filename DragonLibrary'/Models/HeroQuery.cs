using DragonLibrary_.Services;
using GraphQL.Types;
using System;

namespace DragonLibrary_.Models
{
    public class HeroQuery : ObjectGraphType<object>
    {
        public HeroQuery(IHeroService heroService,
            IJWTService jWTService,
            IValidatorService validator)
        {
            Name = "Query";
            Field<ListGraphType<HeroType>>(
                "heroes",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "token" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "pageNumber" },
                    new QueryArgument<StringGraphType> { Name = "beginningOfTheName" },
                    new QueryArgument<DateTimeGraphType> { Name = "before" },
                    new QueryArgument<DateTimeGraphType> { Name = "after" }
                    ),
                resolve: context =>
                {
                    var token = context.GetArgument<string>("token");
                    var pageNumber = context.GetArgument<int>("pageNumber");
                    var beginningOfTheName = context.GetArgument<string>("beginningOfTheName", defaultValue: null);
                    var before = context.GetArgument<DateTime?>("before", defaultValue: null);
                    var after = context.GetArgument<DateTime?>("after", defaultValue: null);

                    validator.ValidateToken(token);

                    var heroes = heroService.GetAllHeroes().Result;

                    if (beginningOfTheName != null)
                    {
                        heroes = heroService.FilterHeroesByNameAsync(heroes, beginningOfTheName).Result;
                    }

                    if (before != null)
                    {
                        heroes = heroService.FilterHeroesCreatedBeforeAsync(heroes, before.Value).Result;
                    }

                    if (after != null)
                    {
                        heroes = heroService.FilterHeroesCreatedAfterAsync(heroes, after.Value).Result;
                    }

                    return heroService.GetPageWithHeroesAsync(heroes, pageNumber);
                });

            Field<StringGraphType>(
                "getHeroName",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "token" }),
                resolve: context =>
                {
                    var token = context.GetArgument<string>("token");
                    return jWTService.GetHeroNameFromToken(token);
                }
                );
        }
    }
}
