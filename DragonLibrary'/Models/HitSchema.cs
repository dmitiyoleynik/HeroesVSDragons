using GraphQL;
using GraphQL.Types;

namespace DragonLibrary_.Models
{
    public class HitSchema:Schema
    {
        public HitSchema(HitQuery hitQuery,IDependencyResolver dependencyResolver)
        {
            Query = hitQuery;
            DependencyResolver = dependencyResolver;

        }
    }
}
