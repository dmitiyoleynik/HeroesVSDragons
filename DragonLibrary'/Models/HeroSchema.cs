using GraphQL;
using GraphQL.Types;

namespace DragonLibrary_.Models
{
    public class HeroSchema : Schema
    {
        public  HeroSchema(HeroQuery heroQuery, HeroMutation heroMutation,IDependencyResolver dependencyResolver)
        {
            Query = heroQuery;
            Mutation = heroMutation;
            DependencyResolver = dependencyResolver;
        }
    }
}
