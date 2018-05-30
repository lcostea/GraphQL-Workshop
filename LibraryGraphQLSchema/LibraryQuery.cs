using GraphQL.Types;
using LibraryModel.Services;
using Microsoft.Extensions.Configuration;
namespace LibraryGraphQLSchema
{
    public class LibraryQuery : ObjectGraphType<object>
    {
        public LibraryQuery(IConfiguration configuration)
        {
            var mongoConnectionString = configuration.GetConnectionString("mongo");
            Field<ListGraphType<AuthorType>>("authors",
             resolve: context => new AuthorService(mongoConnectionString).GetAuthors());
            Field<ListGraphType<BookType>>("books",
             resolve: context => new BookService(mongoConnectionString).GetBooks());

        }
    }
}
