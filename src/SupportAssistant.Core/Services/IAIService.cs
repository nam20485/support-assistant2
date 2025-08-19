namespace SupportAssistant.Core.Services;

/// <summary>
/// Service for AI inference and chat functionality
/// </summary>
public interface IAIService
{
    /// <summary>
    /// Initialize the AI service with model paths
    /// </summary>
    Task<bool> InitializeAsync(string modelPath, string embeddingModelPath);

    /// <summary>
    /// Process a user query and return a response using RAG
    /// </summary>
    Task<string> ProcessQueryAsync(string query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Check if the AI service is ready
    /// </summary>
    bool IsInitialized { get; }

    /// <summary>
    /// Get the status of the AI service
    /// </summary>
    string Status { get; }
}