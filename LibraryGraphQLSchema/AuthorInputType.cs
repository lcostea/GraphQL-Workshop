using GraphQL.Types;

namespace LibraryGraphQLSchema
{
    public class AuthorInputType: InputObjectGraphType
    {
        public AuthorInputType()
        {
            Field<NonNullGraphType<StringGraphType>>("FirstName");
            Field<NonNullGraphType<StringGraphType>>("LastName");
            Field<NonNullGraphType<StringGraphType>>("Genre");
            Field<NonNullGraphType<StringGraphType>>("Nationality");
            Field<NonNullGraphType<StringGraphType>>("Born");

        }
}
}
