using net_test_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace net_test_api.Services;

public class MongoDBService
{
    private readonly IMongoCollection<Post> _postCollection;

    public MongoDBService()
    {
        DotNetEnv.Env.Load();

        // Retrieve values from configuration
        MongoClient client = new MongoClient(Environment.GetEnvironmentVariable("MONGODB_CONNECTION_URI"));
        IMongoDatabase database = client.GetDatabase(Environment.GetEnvironmentVariable("MONGODB_DATABASE"));
        _postCollection = database.GetCollection<Post>("posts");
    }

    public async Task CreateAsync(Post post)
    {
        post.createdAt = DateTime.Now;
        await _postCollection.InsertOneAsync(post);
        return;
    }

    public async Task<List<Post>> GetPostsAsync()
    {
        return await _postCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdatePostAsync(string id, Post post)
    {
        FilterDefinition<Post> filter = Builders<Post>.Filter.Eq("Id", id);
        UpdateDefinition<Post> update = Builders<Post>.Update
        .Set("tilte", post.title)
        .Set("author", post.author)
        .Set("content", post.content)
        .Set("createdAt", post.createdAt);

        await _postCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeletePostAsync(string id)
    {
        FilterDefinition<Post> filter = Builders<Post>.Filter.Eq("Id", id);
        await _postCollection.DeleteOneAsync(filter);
        return;
    }
}