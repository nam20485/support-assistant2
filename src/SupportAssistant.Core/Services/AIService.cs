using Microsoft.Extensions.Logging;
using Microsoft.ML.OnnxRuntime;

namespace SupportAssistant.Core.Services;

public class AIService : IAIService, IDisposable
{
    private readonly ILogger<AIService> _logger;
    private readonly IKnowledgeBaseService _knowledgeBaseService;
    private InferenceSession? _languageModelSession;
    private InferenceSession? _embeddingModelSession;
    private bool _isInitialized;

    public AIService(ILogger<AIService> logger, IKnowledgeBaseService knowledgeBaseService)
    {
        _logger = logger;
        _knowledgeBaseService = knowledgeBaseService;
    }

    public bool IsInitialized => _isInitialized;

    public string Status => _isInitialized ? "Ready" : "Not Initialized";

    public async Task<bool> InitializeAsync(string modelPath, string embeddingModelPath)
    {
        try
        {
            _logger.LogInformation("Initializing AI Service with models: {ModelPath}, {EmbeddingPath}", modelPath, embeddingModelPath);

            // Initialize ONNX Runtime with DirectML provider
            var sessionOptions = new SessionOptions();
            
            // Try to add DirectML provider, fallback to CPU if not available
            try
            {
                sessionOptions.AppendExecutionProvider_DML();
                _logger.LogInformation("DirectML provider enabled");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "DirectML not available, using CPU provider");
            }

            // For now, we'll simulate model loading since we don't have actual model files
            // In real implementation, these would load actual ONNX models
            _logger.LogInformation("AI models initialized successfully");
            _isInitialized = true;

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to initialize AI Service");
            return false;
        }
    }

    public async Task<string> ProcessQueryAsync(string query, CancellationToken cancellationToken = default)
    {
        if (!_isInitialized)
        {
            return "AI Service is not initialized. Please configure and initialize the models first.";
        }

        try
        {
            _logger.LogInformation("Processing query: {Query}", query);

            // Step 1: Search knowledge base for relevant context
            var relevantContent = await _knowledgeBaseService.SearchAsync(query, 3);
            var context = string.Join("\n\n", relevantContent);

            // Step 2: For now, return a simulated response
            // In real implementation, this would:
            // - Generate embeddings for the query
            // - Construct RAG prompt with context
            // - Run inference on the language model
            var response = $"Based on the available knowledge base, here's information related to your query about '{query}':\n\n";
            
            if (!string.IsNullOrEmpty(context))
            {
                response += $"Relevant information:\n{context}\n\n";
            }
            
            response += "This is a simulated response. The actual AI model will be integrated in the next phase.";

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing query");
            return "An error occurred while processing your query. Please try again.";
        }
    }

    public void Dispose()
    {
        _languageModelSession?.Dispose();
        _embeddingModelSession?.Dispose();
    }
}