using LibraryModel.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryModel.Services
{
    public class BookService
    {
        private readonly string connectionString;

        public BookService(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public List<Book> GetBooks()
        {
            List<Book> books;

            var client = new MongoClient(connectionString);
            var mongoDb = client.GetDatabase("library");
            books = mongoDb.GetCollection<Book>("books").AsQueryable().ToList();
            return books;
        }

        public List<Book> GetBooksByIds(List<string> bookIds)
        {
            List<Book> books;

            var client = new MongoClient(connectionString);
            var mongoDb = client.GetDatabase("library");

            var filterDef = new FilterDefinitionBuilder<Book>();
            var filter = filterDef.In("_id", bookIds.ConvertAll( bookId => ObjectId.Parse(bookId)));
            books = mongoDb.GetCollection<Book>("books").Find(filter).ToList();

            return books;

        }

        public void AddBook(Book book)
        {
            var client = new MongoClient(connectionString);
            var mongoDb = client.GetDatabase("library");
            var booksCollection = mongoDb.GetCollection<Book>("books");

            booksCollection.InsertOne(book);

            var authorService = new AuthorService(connectionString);
            authorService.AddBookToAuthors(book);

        }

    }
}
