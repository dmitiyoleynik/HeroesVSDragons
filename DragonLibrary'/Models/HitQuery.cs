using DragonLibrary_.Services;
using GraphQL.Types;

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
                "getHeroDamageStatistic",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "token" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "pageNumber" },
                    new QueryArgument<BooleanGraphType> { Name = "sortNameAsc" },
                    new QueryArgument<BooleanGraphType> { Name = "sortNameDesc" },
                    new QueryArgument<BooleanGraphType> { Name = "sortDamageAsc" },
                    new QueryArgument<BooleanGraphType> { Name = "sortDamageDesc" }
                    ),
                resolve: context =>
                {
                    var token = context.GetArgument<string>("token");
                    var pageNumber = context.GetArgument<int>("pageNumber");
                    var sortNameAsc = context.GetArgument<bool>("sortNameAsc");
                    var sortNameDesc = context.GetArgument<bool>("sortNameDesc");
                    var sortDamageAsc = context.GetArgument<bool>("sortDamageAsc");
                    var sortDamageDesc = context.GetArgument<bool>("sortDamageDesc");

                    validatorService.ValidateToken(token);

                    var heroName = jWTService.GetHeroNameFromToken(token);
                    var heroId = heroService.GetHeroByName(heroName).Id;
                    var damageStatistic = hitService.GetHeroDamageStatistic(heroId).Result;

                    damageStatistic = hitService.GetPageWithDamagesAsync(damageStatistic, pageNumber).Result;
                    if (sortNameAsc)
                    {
                        damageStatistic = hitService.SortByNameAsc(damageStatistic).Result;
                    }

                    if (sortNameDesc)
                    {
                        damageStatistic = hitService.SortByNameDesc(damageStatistic).Result;
                    }

                    if (sortDamageAsc)
                    {
                        damageStatistic = hitService.SortByDamageAsc(damageStatistic).Result;
                    }

                    if (sortDamageDesc)
                    {
                        damageStatistic = hitService.SortByDamageDesc(damageStatistic).Result;
                    }

                    return damageStatistic;
                });
        }
    }
}
