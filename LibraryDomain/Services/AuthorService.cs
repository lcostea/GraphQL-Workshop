using LibraryModel.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace LibraryModel.Services
{
    public class AuthorService
    {
        private readonly string connectionString;

        public AuthorService(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public List<Author> GetAuthors()
        {
            List<Author> authors;

            var client = new MongoClient(connectionString);
            var mongoDb = client.GetDatabase("library");
            authors = mongoDb.GetCollection<Author>("authors").AsQueryable().ToList();
            return authors;
        }

        public List<Author> GetAuthorsByIds(List<string> authorIds)
        {
            List<Author> authors;

            var client = new MongoClient(connectionString);
            var mongoDb = client.GetDatabase("library");

            var filterDef = new FilterDefinitionBuilder<Author>();
            var filter = filterDef.In("_id", authorIds.ConvertAll(authorId => ObjectId.Parse(authorId)));
            authors = mongoDb.GetCollection<Author>("authors").Find(filter).ToList();

            return authors;

        }

        public void AddAuthor(Author author)
        {
            var client = new MongoClient(connectionString);
            var mongoDb = client.GetDatabase("library");
            var authorCollection = mongoDb.GetCollection<Author>("authors");

            authorCollection.InsertOne(author);

        }

        internal void AddBookToAuthors(Book book)
        {
            var client = new MongoClient(connectionString);
            var mongoDb = client.GetDatabase("library");
            var authorCollection = mongoDb.GetCollection<Author>("authors");


            var filterDef = new FilterDefinitionBuilder<Author>();
            var filter = filterDef.In("_id", book.AuthorIds.ConvertAll(authorId => ObjectId.Parse(authorId)));
            var authorsCollection = mongoDb.GetCollection<Author>("authors");
            var authors = authorsCollection.Find(filter).ToList();

            authors.ForEach(author => 
            {
                var authorFilter = Builders<Author>.Filter.Where(a => a.Id == author.Id);
                var update = Builders<Author>.Update.Set(a => a.BookIds[author.BookIds.Count], book.Id);
                var result = authorsCollection.UpdateOne(authorFilter, update);
            });

        }

    }
}
