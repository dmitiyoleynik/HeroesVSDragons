using DragonLibrary_.Services;
using GraphQL.Types;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonLibrary_.Models
{
    public class HeroQuery : ObjectGraphType<object>
    {
        public HeroQuery(IHeroService heroService)
        {
            Name = "Query";
            Field<ListGraphType<HeroType>>("heroes",
                resolve: context => heroService.GetHeroesAsync());
            Field<ListGraphType<HeroType>>("sort",
                arguments: new QueryArguments(
                     new QueryArgument<IdGraphType> { Name = "id" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return heroService.GetSortedHeroesAsync(id);
                });
        }
    }
}
