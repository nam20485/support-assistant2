namespace SupportAssistant.Core.Services;

/// <summary>
/// Service for managing knowledge base content
/// </summary>
public interface IKnowledgeBaseService
{
    /// <summary>
    /// Initialize the knowledge base from a directory
    /// </summary>
    Task<bool> InitializeAsync(string knowledgeBasePath);

    /// <summary>
    /// Process and ingest knowledge base files
    /// </summary>
    Task<int> ProcessKnowledgeBaseAsync(string path, IProgress<string>? progress = null);

    /// <summary>
    /// Search for relevant content based on query
    /// </summary>
    Task<IEnumerable<string>> SearchAsync(string query, int maxResults = 5);

    /// <summary>
    /// Check if the knowledge base is ready
    /// </summary>
    bool IsInitialized { get; }

    /// <summary>
    /// Get the number of processed documents
    /// </summary>
    int DocumentCount { get; }
}