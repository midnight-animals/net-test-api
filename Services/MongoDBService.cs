using OnlineDictionary.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace OnlineDictionary.Services;


public class MongoDBService
{
    private readonly IMongoCollection<Post> _postCollection;
    private readonly MongoClient client;

    public MongoDBService()
    {
        DotNetEnv.Env.Load();

        Console.WriteLine(Environment.GetEnvironmentVariable("MONGODB_CONNECTION_URI"));
        // Retrieve values from configuration
        client = new MongoClient(Environment.GetEnvironmentVariable("MONGODB_CONNECTION_URI"));
        IMongoDatabase database = client.GetDatabase("test");
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

    public async Task<Boolean> GetDatabaseStatus()
    {
        IMongoDatabase database = client.GetDatabase("admin");
        try
        {
            var pingCommand = new BsonDocument { { "ping", 1 } };
            var pingResult = await database.RunCommandAsync<BsonDocument>(pingCommand);

            if (pingResult["ok"] == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to MongoDB: {ex.Message}");
            return false;
        }
    }
}