using net_test_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace net_test_api.Services;

public class MongoDBService {
    private readonly IMongoCollection<Post> _postCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings) {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _postCollection = database.GetCollection<Post>(mongoDBSettings.Value.CollectionName);
    }

    public async Task CreateAsync(Post post) {
        post.createdAt = DateTime.Now;
        await _postCollection.InsertOneAsync(post);
        return;
    }

    public async Task<List<Post>> GetPostsAsync() {
        return await _postCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdatePostAsync(string id, Post post) {
        FilterDefinition<Post> filter = Builders<Post>.Filter.Eq("Id", id);
        UpdateDefinition<Post> update = Builders<Post>.Update
        .Set("tilte", post.title)
        .Set("author", post.author)
        .Set("content", post.content)
        .Set("createdAt", post.createdAt);

        await _postCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeletePostAsync(string id) {
        FilterDefinition<Post> filter = Builders<Post>.Filter.Eq("Id", id);
        await _postCollection.DeleteOneAsync(filter);
        return;
    }
}