using GraphQL;
using GraphQL.Types;

namespace DragonLibrary_.Models
{
    public class DragonSchema : Schema
    {
        public DragonSchema(DragonQuery dragonQuery, DragonMutation dragonMutation, IDependencyResolver dependencyResolver)
        {
            Query = dragonQuery;
            Mutation = dragonMutation;
            DependencyResolver = dependencyResolver;
        }
    }
}
