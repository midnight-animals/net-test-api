using MongoDB.Bson;
using MongoDB.Driver;

namespace OnlineDictionary.Services
{
    public interface IWordEntryService
{
    Task<List<WordEntry>> GetAllWordsAsync();
    Task<WordEntry> GetWordAsync(string word);
    Interpretation? GetInterpretation(WordEntry wordEntry, string meaning);
    Task CreateWordAsync(WordEntry wordEntry);
    Task UpdateWordAsync(WordEntry oldWordEntry, WordEntry wordEntry);
    Task DeleteWordAsync(WordEntry wordEntry);
    Task AddInterpretationAsync(WordEntry wordEntry, Interpretation interpretation);
    Task RemoveInterpretationAsync(WordEntry wordEntry, Interpretation interpretation);
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

        // Create a compound index on the 'meaning' property of the 'Interpretation' subdocument
        var indexKeysDefinition = Builders<WordEntry>.IndexKeys.Ascending("interpretations.meaning");
        var indexModel = new CreateIndexModel<WordEntry>(indexKeysDefinition);
        _wordEntriesCollection.Indexes.CreateOne(indexModel);
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
    public Interpretation? GetInterpretation(WordEntry wordEntry, string meaning)
    {
        // Find the interpretation with the specified meaning in the interpretations list
        var interpretation = wordEntry.interpretations.Find(i => i.meaning == meaning);
        if (interpretation == null) return null;

        return interpretation;
    }
    public async Task CreateWordAsync(WordEntry wordEntry)
    {
        wordEntry.createdAt = DateTime.Now;
        wordEntry.updatedAt = DateTime.Now;
        await _wordEntriesCollection.InsertOneAsync(wordEntry);
    }
    public async Task UpdateWordAsync(WordEntry oldWordEntry, WordEntry newWordEntry)
    {
        var filter = Builders<WordEntry>.Filter.Eq(x => x.word, oldWordEntry.word);

        var update = Builders<WordEntry>.Update
            .Set(x => x.interpretations, newWordEntry.interpretations)
            .Set(x => x.complexity, newWordEntry.complexity)
            .Set(x => x.updatedAt, DateTime.UtcNow);

        await _wordEntriesCollection.UpdateOneAsync(filter, update);
    }
    public async Task DeleteWordAsync(WordEntry wordEntry)
    {
        var filter = Builders<WordEntry>.Filter.Eq(x => x.word, wordEntry.word);
        var update = Builders<WordEntry>.Update
        .Set(x => x.deletedAt, DateTime.Now);
        await _wordEntriesCollection.UpdateOneAsync(filter, update);
    }

    public async Task AddInterpretationAsync(WordEntry wordEntry, Interpretation interpretation)
    {
        if (wordEntry.interpretations == null) wordEntry.interpretations = new List<Interpretation>();

        wordEntry.interpretations.Add(interpretation);
        await UpdateWordAsync(wordEntry, wordEntry);
    }

    public async Task RemoveInterpretationAsync(WordEntry wordEntry, Interpretation interpretation)
    {
        // Create a filter to find the word entry by its word
        var filter = Builders<WordEntry>.Filter.Eq(x => x.word, wordEntry.word);

        // Remove the interpretation with the specified meaning from the interpretations list
        wordEntry.interpretations.Remove(interpretation);

        // Update the word entry in the database
        var update = Builders<WordEntry>.Update.Set(x => x.interpretations, wordEntry.interpretations);
        await _wordEntriesCollection.UpdateOneAsync(filter, update);
    }
}
}
