using MongoDB.Bson.Serialization.Attributes;

namespace FreeCourse.Services.Catalog.Models
{
    public class Category : BaseModel
    {
        public string? Name { get; set; }
    }
}