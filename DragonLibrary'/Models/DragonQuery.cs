using DragonLibrary_.Services;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonLibrary_.Models
{
    public class DragonQuery :ObjectGraphType<object>
    {
        public DragonQuery(IDragonService dragonService)
        {
            Name = "Query";

            Field<ListGraphType<DragonType>>(
                "dragons",
                resolve: context => dragonService.GetDragonsAsync()
                );
            
        }
    }
}
