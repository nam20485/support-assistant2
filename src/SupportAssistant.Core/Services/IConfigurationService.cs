namespace SupportAssistant.Core.Services;

/// <summary>
/// Service for application configuration management
/// </summary>
public interface IConfigurationService
{
    /// <summary>
    /// Load configuration from file
    /// </summary>
    Task LoadConfigurationAsync();

    /// <summary>
    /// Save configuration to file
    /// </summary>
    Task SaveConfigurationAsync();

    /// <summary>
    /// Get configuration value
    /// </summary>
    T? GetValue<T>(string key);

    /// <summary>
    /// Set configuration value
    /// </summary>
    void SetValue<T>(string key, T value);

    // Application specific settings
    string? ModelPath { get; set; }
    string? EmbeddingModelPath { get; set; }
    string? KnowledgeBasePath { get; set; }
    bool UseDirectML { get; set; }
    string? PreferredDevice { get; set; }
}