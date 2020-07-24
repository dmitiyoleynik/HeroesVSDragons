using DragonLibrary_.Services;
using GraphQL.Types;

namespace DragonLibrary_.Models
{
    public class HeroQuery : ObjectGraphType<object>
    {
        public HeroQuery(IHeroService heroService,IJWTService jWTService)
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
