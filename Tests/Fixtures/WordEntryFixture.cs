namespace net_test_api.Tests.Fixture
{
    public static class WordEntryFixture
    {
        public static List<WordEntry> GetTestWordEntries() => new List<WordEntry>{
            new WordEntry
                {
                    word = "Test1",
                    interpretations = new List<Interpretation>
                    {
                        new Interpretation
                        {
                            meaning = "Meaning1",
                            type = "Noun",
                            examples = new List<string> { "Example1" },
                            synonyms = new List<string> { "Synonym1", "Synonym2" },
                            createdAt = DateTime.Now
                        }
                    },
                    complexity = "Low",
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now
                },
                new WordEntry
                {
                    word = "Test2",
                    interpretations = new List<Interpretation>
                    {
                        new Interpretation
                        {
                            meaning = "Meaning2",
                            type = "Adjective",
                            examples = new List<string> { "Example2" },
                            synonyms = new List<string> { "Synonym3", "Synonym4" },
                            createdAt = DateTime.Now
                        }
                    },
                    complexity = "High",
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now
                }
        };
    }
}