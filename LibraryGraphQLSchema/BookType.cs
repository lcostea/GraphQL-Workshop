using GraphQL.Types;
using LibraryModel.Domain;
using LibraryModel.Services;
using Microsoft.Extensions.Configuration;


namespace LibraryGraphQLSchema
{
    public class BookType: ObjectGraphType<Book>
    {
        public BookType(IConfiguration configuration)
        {
            var mongoConnectionString = configuration.GetConnectionString("mongo");
            Field(f => f.Title);
            Field(f => f.Language);
            Field(f => f.Year);
            Field<ListGraphType<AuthorType>>("authors", resolve: context => new AuthorService(mongoConnectionString).GetAuthorsByIds(context.Source.AuthorIds));
        }
    }
}
