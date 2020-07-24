using DragonLibrary_.Services;
using GraphQL.Types;

namespace DragonLibrary_.Models
{
    public class DragonMutation :ObjectGraphType<object>
    {
        public DragonMutation(IDragonService dragonService)
        {
            Name = "Mutation";
            
            Field<StringGraphType>("createDragon",
                resolve: context => dragonService.CreateDragonAsync());

        }
    }
}
