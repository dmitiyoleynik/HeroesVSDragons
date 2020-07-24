using DragonLibrary_.Services;
using GraphQL.Types;

namespace DragonLibrary_.Models
{
    public class HitQuery : ObjectGraphType<object>
    {
        public HitQuery(IHitService hitService)
        {
            Name = "Query";

            Field<ListGraphType<HitType>>(
                "getHits",
                resolve: context => hitService.GetHitsAsync());
        }
    }
}
