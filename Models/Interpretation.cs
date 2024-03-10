using MongoDB.Bson.Serialization.Attributes;

public class Interpretation
{
    public string meaning { get; set; } = null!;
    public string type { get; set; } = null!;
    public List<string> examples { get; set; } = null!;
    public List<string> synonyms { get; set; } = null!;
    
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime createdAt { get; set; }
}