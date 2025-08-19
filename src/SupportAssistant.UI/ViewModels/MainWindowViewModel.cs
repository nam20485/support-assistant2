using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportAssistant.Core.Services;

namespace SupportAssistant.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly IAIService _aiService;
    private readonly IConfigurationService _configurationService;

    [ObservableProperty]
    private string _title = "Support Assistant";

    [ObservableProperty]
    private ChatViewModel _chatViewModel;

    [ObservableProperty]
    private SettingsViewModel _settingsViewModel;

    [ObservableProperty]
    private bool _isSettingsVisible = false;

    public MainWindowViewModel(
        IAIService aiService, 
        IConfigurationService configurationService,
        ChatViewModel chatViewModel,
        SettingsViewModel settingsViewModel)
    {
        _aiService = aiService;
        _configurationService = configurationService;
        _chatViewModel = chatViewModel;
        _settingsViewModel = settingsViewModel;

        // Initialize configuration
        _ = InitializeAsync();
    }

    [RelayCommand]
    private void ToggleSettings()
    {
        IsSettingsVisible = !IsSettingsVisible;
    }

    [RelayCommand]
    private async Task InitializeAI()
    {
        await _aiService.InitializeAsync(
            _configurationService.ModelPath ?? string.Empty,
            _configurationService.EmbeddingModelPath ?? string.Empty);
    }

    private async Task InitializeAsync()
    {
        await _configurationService.LoadConfigurationAsync();
    }
}