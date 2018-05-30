using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryGraphQLSchema
{
    public class LibrarySchema: Schema
    {
        public LibrarySchema(LibraryQuery query, IDependencyResolver dependencyResolver)
        {
            Query = query;
            DependencyResolver = dependencyResolver;

        }
    }
}
