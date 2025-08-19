using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace SupportAssistant.Core.Services;

public class VectorStoreService : IVectorStoreService
{
    private readonly ILogger<VectorStoreService> _logger;
    private readonly List<VectorEntry> _vectors = new();
    private string? _storePath;

    public VectorStoreService(ILogger<VectorStoreService> logger)
    {
        _logger = logger;
    }

    public int VectorCount => _vectors.Count;

    public async Task<bool> InitializeAsync(string storePath)
    {
        try
        {
            _storePath = storePath;
            _logger.LogInformation("Initializing Vector Store at: {Path}", storePath);

            // Create directory if it doesn't exist
            var directory = Path.GetDirectoryName(storePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Load existing vectors if file exists
            if (File.Exists(storePath))
            {
                await LoadVectorsAsync();
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to initialize Vector Store");
            return false;
        }
    }

    public async Task<bool> AddVectorsAsync(IEnumerable<(float[] Vector, string Content, string Metadata)> vectors)
    {
        try
        {
            foreach (var (vector, content, metadata) in vectors)
            {
                _vectors.Add(new VectorEntry
                {
                    Vector = vector,
                    Content = content,
                    Metadata = metadata
                });
            }

            await SaveVectorsAsync();
            _logger.LogDebug("Added {Count} vectors to store", vectors.Count());
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to add vectors");
            return false;
        }
    }

    public async Task<IEnumerable<(float Score, string Content, string Metadata)>> SearchAsync(float[] queryVector, int maxResults = 5)
    {
        try
        {
            if (_vectors.Count == 0)
            {
                return Enumerable.Empty<(float, string, string)>();
            }

            // Calculate cosine similarity for each vector
            var scores = _vectors.Select(entry => new
            {
                Score = CosineSimilarity(queryVector, entry.Vector),
                Content = entry.Content,
                Metadata = entry.Metadata
            })
            .OrderByDescending(x => x.Score)
            .Take(maxResults)
            .Select(x => (x.Score, x.Content, x.Metadata));

            return scores;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during vector search");
            return Enumerable.Empty<(float, string, string)>();
        }
    }

    public async Task ClearAsync()
    {
        _vectors.Clear();
        if (!string.IsNullOrEmpty(_storePath) && File.Exists(_storePath))
        {
            File.Delete(_storePath);
        }
        _logger.LogInformation("Vector store cleared");
    }

    private async Task SaveVectorsAsync()
    {
        if (string.IsNullOrEmpty(_storePath))
            return;

        try
        {
            var json = JsonSerializer.Serialize(_vectors, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_storePath, json);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save vectors");
        }
    }

    private async Task LoadVectorsAsync()
    {
        if (string.IsNullOrEmpty(_storePath) || !File.Exists(_storePath))
            return;

        try
        {
            var json = await File.ReadAllTextAsync(_storePath);
            var vectors = JsonSerializer.Deserialize<List<VectorEntry>>(json);
            if (vectors != null)
            {
                _vectors.Clear();
                _vectors.AddRange(vectors);
                _logger.LogInformation("Loaded {Count} vectors from store", vectors.Count);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load vectors");
        }
    }

    private static float CosineSimilarity(float[] a, float[] b)
    {
        if (a.Length != b.Length)
            return 0f;

        float dotProduct = 0f;
        float normA = 0f;
        float normB = 0f;

        for (int i = 0; i < a.Length; i++)
        {
            dotProduct += a[i] * b[i];
            normA += a[i] * a[i];
            normB += b[i] * b[i];
        }

        return dotProduct / (MathF.Sqrt(normA) * MathF.Sqrt(normB));
    }

    private class VectorEntry
    {
        public float[] Vector { get; set; } = Array.Empty<float>();
        public string Content { get; set; } = string.Empty;
        public string Metadata { get; set; } = string.Empty;
    }
}