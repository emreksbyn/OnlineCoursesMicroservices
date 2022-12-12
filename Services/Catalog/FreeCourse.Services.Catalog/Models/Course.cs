using MongoDB.Bson.Serialization.Attributes;

namespace FreeCourse.Services.Catalog.Models
{
    public class Course : BaseModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        public decimal Price { get; set; }
        public string? UserId { get; set; }
        public string? Picture { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime CreatedTime { get; set; }

        public Feature? Feature { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? CategoryId { get; set; }

        // Tabloda olusturma
        [BsonIgnore]
        public Category? Category { get; set; }
    }
}