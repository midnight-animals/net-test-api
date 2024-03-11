namespace OnlineDictionary.Models;

public class MongoDBSettings {
    public string ConnectionUri {set; get;} = null!;
    public string DatabaseName {set; get;} = null!;
    public string CollectionName {set; get;} = null!;
}