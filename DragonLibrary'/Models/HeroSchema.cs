using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonLibrary_.Models
{
    public class HeroSchema : GraphQL.Types.Schema
    {
        public  HeroSchema(HeroQuery heroQuery,IDependencyResolver dependencyResolver)
        {
            Query = heroQuery;
            DependencyResolver = dependencyResolver;
        }
    }
}
