using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportAssistant.Core.Services;

namespace SupportAssistant.UI.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
    private readonly IConfigurationService _configurationService;
    private readonly IKnowledgeBaseService _knowledgeBaseService;

    [ObservableProperty]
    private string _modelPath = string.Empty;

    [ObservableProperty]
    private string _embeddingModelPath = string.Empty;

    [ObservableProperty]
    private string _knowledgeBasePath = string.Empty;

    [ObservableProperty]
    private bool _useDirectML = true;

    [ObservableProperty]
    private string _preferredDevice = "GPU";

    [ObservableProperty]
    private bool _isBusy = false;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    [ObservableProperty]
    private int _knowledgeBaseDocumentCount = 0;

    public SettingsViewModel(IConfigurationService configurationService, IKnowledgeBaseService knowledgeBaseService)
    {
        _configurationService = configurationService;
        _knowledgeBaseService = knowledgeBaseService;
        
        LoadSettings();
    }

    [RelayCommand]
    private async Task BrowseModelPath()
    {
        // In a real implementation, this would open a file dialog
        // For now, we'll just show a placeholder
        StatusMessage = "File dialog would open here to select model file";
    }

    [RelayCommand]
    private async Task BrowseEmbeddingModelPath()
    {
        StatusMessage = "File dialog would open here to select embedding model file";
    }

    [RelayCommand]
    private async Task BrowseKnowledgeBasePath()
    {
        StatusMessage = "Folder dialog would open here to select knowledge base folder";
    }

    [RelayCommand]
    private async Task ProcessKnowledgeBase()
    {
        if (string.IsNullOrWhiteSpace(KnowledgeBasePath))
        {
            StatusMessage = "Please select a knowledge base path first";
            return;
        }

        IsBusy = true;
        StatusMessage = "Processing knowledge base...";

        try
        {
            var progress = new Progress<string>(message => StatusMessage = message);
            var documentCount = await _knowledgeBaseService.ProcessKnowledgeBaseAsync(KnowledgeBasePath, progress);
            
            KnowledgeBaseDocumentCount = documentCount;
            StatusMessage = $"Successfully processed {documentCount} documents";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error processing knowledge base: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task SaveSettings()
    {
        IsBusy = true;
        StatusMessage = "Saving settings...";

        try
        {
            _configurationService.ModelPath = ModelPath;
            _configurationService.EmbeddingModelPath = EmbeddingModelPath;
            _configurationService.KnowledgeBasePath = KnowledgeBasePath;
            _configurationService.UseDirectML = UseDirectML;
            _configurationService.PreferredDevice = PreferredDevice;

            await _configurationService.SaveConfigurationAsync();
            StatusMessage = "Settings saved successfully";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error saving settings: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void LoadSettings()
    {
        ModelPath = _configurationService.ModelPath ?? string.Empty;
        EmbeddingModelPath = _configurationService.EmbeddingModelPath ?? string.Empty;
        KnowledgeBasePath = _configurationService.KnowledgeBasePath ?? string.Empty;
        UseDirectML = _configurationService.UseDirectML;
        PreferredDevice = _configurationService.PreferredDevice ?? "GPU";
        KnowledgeBaseDocumentCount = _knowledgeBaseService.DocumentCount;
        
        StatusMessage = "Settings loaded";
    }
}