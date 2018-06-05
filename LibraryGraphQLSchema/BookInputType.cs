using GraphQL.Types;

namespace LibraryGraphQLSchema
{
    public class BookInputType: InputObjectGraphType
    {
        public BookInputType()
        {
            Field<NonNullGraphType<StringGraphType>>("Title");
            Field<NonNullGraphType<StringGraphType>>("Language");
            Field<NonNullGraphType<IntGraphType>>("Year");
            Field<NonNullGraphType<ListGraphType<StringGraphType>>>("AuthorIds");
        }
    }
}
