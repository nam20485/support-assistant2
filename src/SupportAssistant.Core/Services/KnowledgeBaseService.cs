using Microsoft.Extensions.Logging;

namespace SupportAssistant.Core.Services;

public class KnowledgeBaseService : IKnowledgeBaseService
{
    private readonly ILogger<KnowledgeBaseService> _logger;
    private readonly IVectorStoreService _vectorStoreService;
    private readonly List<string> _documents = new();
    private bool _isInitialized;

    public KnowledgeBaseService(ILogger<KnowledgeBaseService> logger, IVectorStoreService vectorStoreService)
    {
        _logger = logger;
        _vectorStoreService = vectorStoreService;
    }

    public bool IsInitialized => _isInitialized;

    public int DocumentCount => _documents.Count;

    public async Task<bool> InitializeAsync(string knowledgeBasePath)
    {
        try
        {
            _logger.LogInformation("Initializing Knowledge Base from: {Path}", knowledgeBasePath);

            if (!Directory.Exists(knowledgeBasePath))
            {
                _logger.LogWarning("Knowledge base path does not exist: {Path}", knowledgeBasePath);
                return false;
            }

            _isInitialized = true;
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to initialize Knowledge Base");
            return false;
        }
    }

    public async Task<int> ProcessKnowledgeBaseAsync(string path, IProgress<string>? progress = null)
    {
        try
        {
            _logger.LogInformation("Processing knowledge base at: {Path}", path);
            progress?.Report("Starting knowledge base processing...");

            var supportedExtensions = new[] { ".txt", ".md", ".html" };
            var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                .Where(f => supportedExtensions.Contains(Path.GetExtension(f).ToLower()))
                .ToList();

            _documents.Clear();

            foreach (var file in files)
            {
                try
                {
                    progress?.Report($"Processing: {Path.GetFileName(file)}");
                    
                    var content = await File.ReadAllTextAsync(file);
                    var chunks = ChunkText(content, 500); // 500 characters per chunk
                    
                    foreach (var chunk in chunks)
                    {
                        _documents.Add(chunk);
                    }

                    _logger.LogDebug("Processed file: {File}, Chunks: {ChunkCount}", file, chunks.Count);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to process file: {File}", file);
                }
            }

            progress?.Report($"Completed processing {files.Count} files, {_documents.Count} chunks");
            _logger.LogInformation("Knowledge base processing completed. Files: {FileCount}, Chunks: {ChunkCount}", 
                files.Count, _documents.Count);

            return _documents.Count;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing knowledge base");
            return 0;
        }
    }

    public async Task<IEnumerable<string>> SearchAsync(string query, int maxResults = 5)
    {
        try
        {
            if (!_isInitialized || _documents.Count == 0)
            {
                return Enumerable.Empty<string>();
            }

            // Simple keyword-based search for now
            // In real implementation, this would use vector similarity search
            var queryLower = query.ToLower();
            var results = _documents
                .Where(doc => doc.ToLower().Contains(queryLower))
                .Take(maxResults)
                .ToList();

            _logger.LogDebug("Search for '{Query}' returned {ResultCount} results", query, results.Count);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching knowledge base");
            return Enumerable.Empty<string>();
        }
    }

    private static List<string> ChunkText(string text, int chunkSize)
    {
        var chunks = new List<string>();
        
        if (string.IsNullOrWhiteSpace(text))
            return chunks;

        var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var currentChunk = new List<string>();
        var currentLength = 0;

        foreach (var word in words)
        {
            if (currentLength + word.Length + 1 > chunkSize && currentChunk.Count > 0)
            {
                chunks.Add(string.Join(" ", currentChunk));
                currentChunk.Clear();
                currentLength = 0;
            }

            currentChunk.Add(word);
            currentLength += word.Length + 1;
        }

        if (currentChunk.Count > 0)
        {
            chunks.Add(string.Join(" ", currentChunk));
        }

        return chunks;
    }
}