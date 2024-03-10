using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

public class WordEntry {
    [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
    public string word {get; set;} = null!;
    public List<Interpretation> interpretations {get; set;} = null!;
    public string complexity {get; set;} = null!;

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime createdAt {get; set;}
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime updatedAt {get; set;}
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime? deletedAt {get; set;} = null!;
}