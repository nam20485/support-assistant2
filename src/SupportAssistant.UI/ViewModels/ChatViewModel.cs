using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportAssistant.Core.Services;
using SupportAssistant.UI.Models;

namespace SupportAssistant.UI.ViewModels;

public partial class ChatViewModel : ViewModelBase
{
    private readonly IAIService _aiService;

    [ObservableProperty]
    private string _userInput = string.Empty;

    [ObservableProperty]
    private bool _isBusy = false;

    [ObservableProperty]
    private string _statusMessage = "Ready";

    public ObservableCollection<ChatMessage> Messages { get; } = new();

    public ChatViewModel(IAIService aiService)
    {
        _aiService = aiService;
        
        // Add welcome message
        Messages.Add(new ChatMessage
        {
            Content = "Welcome to Support Assistant! I'm here to help you with technical support questions. Type your question below and I'll search through the knowledge base to provide you with relevant information.",
            IsFromUser = false,
            Timestamp = DateTime.Now
        });
    }

    [RelayCommand]
    private async Task SendMessage()
    {
        if (string.IsNullOrWhiteSpace(UserInput) || IsBusy)
            return;

        var userMessage = UserInput.Trim();
        UserInput = string.Empty;

        // Add user message
        Messages.Add(new ChatMessage
        {
            Content = userMessage,
            IsFromUser = true,
            Timestamp = DateTime.Now
        });

        IsBusy = true;
        StatusMessage = "Thinking...";

        try
        {
            // Get AI response
            var response = await _aiService.ProcessQueryAsync(userMessage);

            // Add AI response
            Messages.Add(new ChatMessage
            {
                Content = response,
                IsFromUser = false,
                Timestamp = DateTime.Now
            });

            StatusMessage = "Ready";
        }
        catch (Exception ex)
        {
            Messages.Add(new ChatMessage
            {
                Content = $"Sorry, I encountered an error: {ex.Message}",
                IsFromUser = false,
                Timestamp = DateTime.Now
            });

            StatusMessage = "Error occurred";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void ClearMessages()
    {
        Messages.Clear();
        
        // Re-add welcome message
        Messages.Add(new ChatMessage
        {
            Content = "Chat cleared. How can I help you today?",
            IsFromUser = false,
            Timestamp = DateTime.Now
        });
    }
}