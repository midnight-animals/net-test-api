using Microsoft.AspNetCore.Mvc;
using OnlineDictionary.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineDictionary.Controllers;

[Controller]
[Route("api/dictionary")]
public class WordEntryController : ControllerBase
{
    private readonly IWordEntryService _wordEntryService;

    public WordEntryController(IWordEntryService wordEntryService)
    {
        _wordEntryService = wordEntryService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<WordEntry>>> GetAllWords()
    {
        var words = await _wordEntryService.GetAllWordsAsync();
        if (words.Any())
        {
            return Ok(words);
        }
        return NotFound();
    }
    [HttpGet("word/{word}")]
    public async Task<ActionResult<WordEntry>> GetWord(string word)
    {
        var wordEntry = await _wordEntryService.GetWordAsync(word);
        if (wordEntry == null) return NotFound();
        return Ok(wordEntry);
    }
    [HttpGet("word/{word}/{meaning}")]
    public async Task<ActionResult<WordEntry>> GetInterpretationFromWord(string word, string meaning)
    {
        var wordEntry = await _wordEntryService.GetWordAsync(word);
        if (wordEntry == null) return NotFound();

        var interpretation = _wordEntryService.GetInterpretation(wordEntry, meaning);
        if (interpretation == null) return NotFound();

        return Ok(interpretation);
    }

    [HttpPost("word")]
    public async Task<IActionResult> AddNewWord([FromBody] WordEntry wordEntry)
    {
        await _wordEntryService.CreateWordAsync(wordEntry);
        return Ok(wordEntry);
    }
    [HttpPut("word/{word}")]
    public async Task<IActionResult> EditWord(string word, [FromBody] WordEntry wordEntry)
    {
        var existingWordEntry = await _wordEntryService.GetWordAsync(word);
        if (existingWordEntry == null) return NotFound();
        await _wordEntryService.UpdateWordAsync(existingWordEntry, wordEntry);
        return Ok(wordEntry);
    }

    [HttpPut("word/add-interpretation/{word}")]
    public async Task<IActionResult> AddInterpretationToWord(string word, [FromBody] Interpretation interpretation)
    {
        var existingWordEntry = await _wordEntryService.GetWordAsync(word);
        if (existingWordEntry == null) return NotFound();

        existingWordEntry.interpretations.Add(interpretation);
        await _wordEntryService.UpdateWordAsync(existingWordEntry, existingWordEntry);
        return Ok(existingWordEntry);
    }

    [HttpPut("word/remove-interpretation/{word}/{meaning}")]
    public async Task<IActionResult> RemoveInterpretationFromWord(string word, string meaning)
    {
        var existingWordEntry = await _wordEntryService.GetWordAsync(word);
        if (existingWordEntry == null) return NotFound();

        var existingInterpritation = _wordEntryService.GetInterpretation(existingWordEntry, meaning);
        if (existingInterpritation == null) return NotFound();

        await _wordEntryService.RemoveInterpretationAsync(existingWordEntry, existingInterpritation);
        return Ok(existingWordEntry);
    }

    [HttpDelete("word/{word}")]
    public async Task<IActionResult> DeleteWord(string word)
    {
        var existingWordEntry = await _wordEntryService.GetWordAsync(word);
        if (existingWordEntry == null) return NotFound();

        await _wordEntryService.DeleteWordAsync(existingWordEntry);
        return NoContent();
    }
}
