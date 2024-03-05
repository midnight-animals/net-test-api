using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace net_test_api.Models;

public class Post {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;}


    [BsonElement("body")]
    public string content {get; set;} = null!;

    public string permalink {get; set;} = null!;
    public string author {get; set;} = null!; 
    public string title {get; set;} = null!;
    public List<Object> comments {get; set;} = null!;
    public List<String> tags {get; set;} = null!;

    [BsonElement("date")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime createdAt {get; set;}

}