﻿using DragonLibrary_.Services;
using GraphQL.Types;
using System;

namespace DragonLibrary_.Models
{
    public class HeroQuery : ObjectGraphType<object>
    {
        public HeroQuery(IHeroService heroService,IJWTService jWTService)
        {
            Name = "Query";
            Field<ListGraphType<HeroType>>("allHeroes",
                resolve: context => heroService.GetAllHeroes());
            Field<ListGraphType<HeroType>>("sort",
                arguments: new QueryArguments(
                     new QueryArgument<IdGraphType> { Name = "id" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return heroService.GetSortedHeroesAsync(id);
                });
            Field<ListGraphType<HeroType>>(
                "heroes",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "pageNumber"},
                    new QueryArgument<StringGraphType> { Name = "beginningOfTheName" },
                    new QueryArgument<DateTimeGraphType> { Name = "before"},
                    new QueryArgument<DateTimeGraphType> { Name = "after"}
                    ),
                resolve: context =>
                {
                    var pageNumber = context.GetArgument<int>("pageNumber");
                    var beginningOfTheName = context.GetArgument<string>("beginningOfTheName",defaultValue:null);
                    var before = context.GetArgument<DateTime?>("before",defaultValue:null);
                    var after = context.GetArgument<DateTime?>("after", defaultValue: null);
                    
                    var heroes = heroService.GetAllHeroes().Result;
                    if (beginningOfTheName!=null)
                    {
                        heroes = heroService.FilterHeroesByNameAsync(heroes,beginningOfTheName).Result;
                    }

                    return heroService.GetPageWithHeroesAsync(heroes, pageNumber);
                });

            Field<StringGraphType>(
                "getHeroName",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "token"}),
                resolve: context =>
                {
                    var token = context.GetArgument<string>("token");
                    return jWTService.GetHeroNameFromToken(token);
                }
                );
        }
    }
}
