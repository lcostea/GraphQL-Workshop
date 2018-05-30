using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace LibraryModel.Domain
{
    public class Author
    {
        public Author()
        {
            Books = new List<Book>();
            BookIds = new List<string>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("born")]
        public string Born { get; set; }

        [BsonElement("genre")]
        public string Genre { get; set; }

        [BsonElement("nationality")]
        public string Nationality { get; set; }

        [BsonElement("books")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> BookIds { get; set; }

        [BsonIgnore]
        public List<Book> Books { get; private set; }

    }
}
