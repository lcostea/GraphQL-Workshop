using GraphQL.Types;
using LibraryModel.Domain;
using LibraryModel.Services;
using Microsoft.Extensions.Configuration;

namespace LibraryGraphQLSchema
{
    public class LibraryMutation: ObjectGraphType<object>
    {
        public LibraryMutation(IConfiguration configuration)
        {
            var mongoConnectionString = configuration.GetConnectionString("mongo");
            Field<AuthorType>("createAuthor", arguments: new QueryArguments(new QueryArgument<NonNullGraphType<AuthorInputType>> { Name = "author" }), resolve: context =>
            {
                var authorInput = context.GetArgument<Author>("author");
                var authorService = new AuthorService(mongoConnectionString);

                authorService.AddAuthor(authorInput);

                return authorInput;
            });

            Field<BookType>("createBook", arguments: new QueryArguments(new QueryArgument<NonNullGraphType<BookInputType>> { Name = "book" }), resolve: context =>
            {
                var bookInput = context.GetArgument<Book>("book");

                var book = context.GetArgument<Book>("book");
                var bookService = new BookService(mongoConnectionString);

                bookService.AddBook(book);

                return book;

            });
        }
    }
}
