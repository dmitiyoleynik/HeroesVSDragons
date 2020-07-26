using DragonLibrary_.Services;
using GraphQL.Types;

namespace DragonLibrary_.Models
{
    public class DragonQuery : ObjectGraphType<object>
    {
        public DragonQuery(IDragonService dragonService,
            IValidatorService validatorService)
        {
            Name = "Query";

            Field<DragonType>(
                "dragonById",
                arguments: new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "token" },
                    new QueryArgument<IdGraphType>{Name = "id"}
                },
                resolve: context =>
                {
                    var token = context.GetArgument<string>("token");
                    var id = context.GetArgument<int>("id");

                    validatorService.ValidateToken(token);

                    return dragonService.FindDragonByIdAsync(id);
                });

            Field<ListGraphType<DragonType>>(
                "dragons",
                arguments: new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "token" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "pageNumber" },
                    new QueryArgument<StringGraphType> { Name = "beginningOfTheName" },
                    new QueryArgument<IdGraphType> { Name = "maxHpMoreThen" },
                    new QueryArgument<IdGraphType> { Name = "maxHpLessThen" },
                    new QueryArgument<IdGraphType> { Name = "hpMoreThen" },
                    new QueryArgument<IdGraphType> { Name = "hpLessThen" },
                },
                resolve: context =>
                {
                    var token = context.GetArgument<string>("token");
                    var pageNumber = context.GetArgument<int>("pageNumber");
                    var beginningOfTheName = context.GetArgument<string>("beginningOfTheName", defaultValue: "");
                    var maxHpMoreThen = context.GetArgument<int>("maxHpMoreThen", defaultValue: 80);
                    var maxHpLessThen = context.GetArgument<int>("maxHpLessThen", defaultValue: 100);
                    var hpMoreThen = context.GetArgument<int>("hpMoreThen", defaultValue: 0);
                    var hpLessThen = context.GetArgument<int>("hpLessThen", defaultValue: 100);

                    validatorService.ValidateToken(token);

                    var dragons = dragonService.GetDragonsAsync().Result;
                    dragons = dragonService.GetPageWithDragonsAsync(dragons, pageNumber).Result;

                    dragons = dragonService.FilterDragonsByNameAsync(dragons, beginningOfTheName).Result;
                    dragons = dragonService.FilterDragonsByHp(dragons, hpMoreThen, hpLessThen).Result;
                    dragons = dragonService.FilterDragonsByMaxHp(dragons, maxHpMoreThen, maxHpLessThen).Result;

                    return dragons;
                }
                );
        }
    }
}
