using DragonLibrary_.Services;
using GraphQL.Types;

namespace DragonLibrary_.Models
{
    public class DragonMutation : ObjectGraphType<object>
    {
        public DragonMutation(IDragonService dragonService,
            IValidatorService validator)
        {
            Name = "Mutation";

            Field<IdGraphType>("createDragon",
                arguments: new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "token" }
                },
                resolve: context =>
                {
                    var token = context.GetArgument<string>("token");
                    validator.ValidateToken(token);

                    return dragonService.CreateDragonAsync();
                });
        }
    }
}
