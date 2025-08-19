using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace SupportAssistant.Core.Services;

public class ConfigurationService : IConfigurationService
{
    private readonly ILogger<ConfigurationService> _logger;
    private readonly Dictionary<string, object> _configuration = new();
    private readonly string _configPath;

    public ConfigurationService(ILogger<ConfigurationService> logger)
    {
        _logger = logger;
        _configPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "SupportAssistant",
            "config.json"
        );
    }

    public string? ModelPath
    {
        get => GetValue<string>("ModelPath");
        set => SetValue("ModelPath", value);
    }

    public string? EmbeddingModelPath
    {
        get => GetValue<string>("EmbeddingModelPath");
        set => SetValue("EmbeddingModelPath", value);
    }

    public string? KnowledgeBasePath
    {
        get => GetValue<string>("KnowledgeBasePath");
        set => SetValue("KnowledgeBasePath", value);
    }

    public bool UseDirectML
    {
        get => GetValue<bool>("UseDirectML");
        set => SetValue("UseDirectML", value);
    }

    public string? PreferredDevice
    {
        get => GetValue<string>("PreferredDevice");
        set => SetValue("PreferredDevice", value);
    }

    public async Task LoadConfigurationAsync()
    {
        try
        {
            if (!File.Exists(_configPath))
            {
                // Set default values
                SetDefaults();
                await SaveConfigurationAsync();
                return;
            }

            var json = await File.ReadAllTextAsync(_configPath);
            var config = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);

            if (config != null)
            {
                _configuration.Clear();
                foreach (var kvp in config)
                {
                    _configuration[kvp.Key] = ConvertJsonElement(kvp.Value);
                }
            }

            _logger.LogInformation("Configuration loaded from: {Path}", _configPath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load configuration, using defaults");
            SetDefaults();
        }
    }

    public async Task SaveConfigurationAsync()
    {
        try
        {
            // Ensure directory exists
            var directory = Path.GetDirectoryName(_configPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var json = JsonSerializer.Serialize(_configuration, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_configPath, json);
            
            _logger.LogInformation("Configuration saved to: {Path}", _configPath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save configuration");
        }
    }

    public T? GetValue<T>(string key)
    {
        if (_configuration.TryGetValue(key, out var value))
        {
            try
            {
                if (value is T directValue)
                    return directValue;

                // Try to convert
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to convert configuration value for key: {Key}", key);
            }
        }

        return default;
    }

    public void SetValue<T>(string key, T value)
    {
        _configuration[key] = value!;
    }

    private void SetDefaults()
    {
        UseDirectML = true;
        PreferredDevice = "GPU";
        
        // Set default paths relative to application directory
        var appDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "SupportAssistant"
        );
        
        KnowledgeBasePath = Path.Combine(appDataPath, "KnowledgeBase");
        
        _logger.LogInformation("Default configuration values set");
    }

    private static object ConvertJsonElement(JsonElement element)
    {
        return element.ValueKind switch
        {
            JsonValueKind.String => element.GetString() ?? string.Empty,
            JsonValueKind.Number => element.TryGetInt32(out var intValue) ? intValue : element.GetDouble(),
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            JsonValueKind.Null => null!,
            _ => element.ToString()
        };
    }
}