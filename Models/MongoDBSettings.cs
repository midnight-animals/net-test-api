namespace net_test_api.Models;

public class MongoDBSettings {
    public string ConnectionURI {set; get;} = null!;
    public string DatabaseName {set; get;} = null!;
    public string CollectionName {set; get;} = null!;
}