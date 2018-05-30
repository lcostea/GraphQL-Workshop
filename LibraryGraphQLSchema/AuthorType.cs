using GraphQL.Types;
using LibraryModel.Domain;
using LibraryModel.Services;
using Microsoft.Extensions.Configuration;

namespace LibraryGraphQLSchema
{
    public class AuthorType : ObjectGraphType<Author>
    {
        public AuthorType(IConfiguration configuration)
        {
            var mongoConnectionString = configuration.GetConnectionString("mongo");
            Field(f => f.Id);
            Field(f => f.FirstName);
            Field(f => f.LastName);
            Field(f => f.Nationality);
            Field(f => f.Born);
            Field(f => f.Genre);
            Field<ListGraphType<BookType>>("books", resolve: context => new BookService(mongoConnectionString).GetBooksByIds(context.Source.BookIds));

        }
    }
}
