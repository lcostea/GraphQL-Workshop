using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryModel.Domain
{
    public class Book
    {
        public Book()
        {
            Authors = new List<Author>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("language")]
        public string Language { get; set; }
        [BsonElement("year")]
        public int Year { get; set; }

        [BsonElement("authors")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> AuthorIds { get; set; }
        
        [BsonIgnore]
        public List<Author> Authors { get; private set; }
    }
}
