namespace SupportAssistant.Core.Services;

/// <summary>
/// Service for vector storage and similarity search
/// </summary>
public interface IVectorStoreService
{
    /// <summary>
    /// Initialize the vector store
    /// </summary>
    Task<bool> InitializeAsync(string storePath);

    /// <summary>
    /// Add vectors to the store
    /// </summary>
    Task<bool> AddVectorsAsync(IEnumerable<(float[] Vector, string Content, string Metadata)> vectors);

    /// <summary>
    /// Search for similar vectors
    /// </summary>
    Task<IEnumerable<(float Score, string Content, string Metadata)>> SearchAsync(float[] queryVector, int maxResults = 5);

    /// <summary>
    /// Get vector count
    /// </summary>
    int VectorCount { get; }

    /// <summary>
    /// Clear all vectors
    /// </summary>
    Task ClearAsync();
}