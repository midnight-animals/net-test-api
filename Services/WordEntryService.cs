using MongoDB.Bson;
using MongoDB.Driver;

public interface IWordEntryService
{
    Task<List<WordEntry>> GetAllWordsAsync();
    Task<WordEntry> GetWordAsync(string word);
    Task CreateWordAsync(WordEntry wordEntry);
    Task UpdateWordAsync(string word, WordEntry wordEntry);
    Task DeleteWordAsync(string word);
    Task AddInterpretationAsync(string word, Interpretation interpretation);
    
}
public class WordEntryService : IWordEntryService
{
    private readonly IMongoCollection<WordEntry> _wordEntriesCollection;
    public WordEntryService()
    {
        DotNetEnv.Env.Load();
        MongoClient client = new MongoClient(Environment.GetEnvironmentVariable("MONGODB_CONNECTION_URI"));
        IMongoDatabase database = client.GetDatabase("online_dictionary");
        database.CreateCollection("word_entries");
        _wordEntriesCollection = database.GetCollection<WordEntry>("word_entries");
    }

    public async Task<List<WordEntry>> GetAllWordsAsync()
    {
        return await _wordEntriesCollection.Find(new BsonDocument()).ToListAsync();
    }
    public async Task<WordEntry> GetWordAsync(string word)
    {
        var filter = Builders<WordEntry>.Filter.Eq(x => x.word, word);
        return await _wordEntriesCollection.Find(filter).FirstOrDefaultAsync();
    }
    public async Task CreateWordAsync(WordEntry wordEntry)
    {
        wordEntry.createdAt = DateTime.Now;
        wordEntry.updatedAt = DateTime.Now;
        await _wordEntriesCollection.InsertOneAsync(wordEntry);
    }
    public async Task UpdateWordAsync(string word, WordEntry wordEntry)
    {
        var filter = Builders<WordEntry>.Filter.Eq(x => x.word, word);
        var update = Builders<WordEntry>.Update
        .Set(x => x.interpretations, wordEntry.interpretations)
        .Set(x => x.complexity, wordEntry.complexity)
        .Set(x => x.updatedAt, DateTime.Now);
        await _wordEntriesCollection.UpdateOneAsync(filter, update);
    }
    public async Task DeleteWordAsync(string word)
    {
        var filter = Builders<WordEntry>.Filter.Eq(x => x.word, word);
        var update = Builders<WordEntry>.Update
        .Set(x => x.deletedAt, DateTime.Now);
        await _wordEntriesCollection.UpdateOneAsync(filter, update);
    }

    public async Task AddInterpretationAsync(string word, Interpretation interpretation)
    {
        WordEntry wordEntry = await GetWordAsync(word);
        if (wordEntry != null)
        {
            if (wordEntry.interpretations == null)
            {
                wordEntry.interpretations = new List<Interpretation>();
            }
            wordEntry.interpretations.Add(interpretation);
            await UpdateWordAsync(word, wordEntry);
        }
        else
        {
            throw new KeyNotFoundException($"Word '{word}' not found.");
        }
    }
}