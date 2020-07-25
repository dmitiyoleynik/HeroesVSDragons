using DragonLibrary_.Services;
using GraphQL.Types;

namespace DragonLibrary_.Models
{
    public class HeroQuery : ObjectGraphType<object>
    {
        public HeroQuery(IHeroService heroService,IJWTService jWTService)
        {
            Name = "Query";
            Field<ListGraphType<HeroType>>("allHeroes",
                resolve: context => heroService.GetHeroes());
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
                    new QueryArgument<IdGraphType> { Name = "pageNumber"}
                    ),
                resolve: context =>
                {
                    var pageNumber = context.GetArgument<int>("pageNumber");
                    return heroService.GetPageWithHeroesAsync(pageNumber);
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
