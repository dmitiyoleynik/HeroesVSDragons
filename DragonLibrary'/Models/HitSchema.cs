using GraphQL;
using GraphQL.Types;

namespace DragonLibrary_.Models
{
    public class HitSchema : Schema
    {
        public HitSchema(HitQuery hitQuery, HitMutation hitMutation, IDependencyResolver dependencyResolver)
        {
            Query = hitQuery;
            Mutation = hitMutation;
            DependencyResolver = dependencyResolver;
        }
    }
}
