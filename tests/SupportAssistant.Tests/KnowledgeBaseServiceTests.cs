using Xunit;
using SupportAssistant.Core.Services;
using Microsoft.Extensions.Logging.Abstractions;

namespace SupportAssistant.Tests;

public class KnowledgeBaseServiceTests
{
    [Fact]
    public void KnowledgeBaseService_ShouldInitialize()
    {
        // Arrange
        var logger = NullLogger<KnowledgeBaseService>.Instance;
        var vectorStore = new VectorStoreService(NullLogger<VectorStoreService>.Instance);
        
        // Act
        var service = new KnowledgeBaseService(logger, vectorStore);
        
        // Assert
        Assert.NotNull(service);
        Assert.False(service.IsInitialized);
        Assert.Equal(0, service.DocumentCount);
    }

    [Fact]
    public async Task KnowledgeBaseService_SearchAsync_ShouldReturnEmpty_WhenNotInitialized()
    {
        // Arrange
        var logger = NullLogger<KnowledgeBaseService>.Instance;
        var vectorStore = new VectorStoreService(NullLogger<VectorStoreService>.Instance);
        var service = new KnowledgeBaseService(logger, vectorStore);
        
        // Act
        var results = await service.SearchAsync("test query");
        
        // Assert
        Assert.Empty(results);
    }
}