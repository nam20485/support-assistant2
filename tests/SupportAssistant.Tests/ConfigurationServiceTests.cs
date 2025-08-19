using Xunit;
using SupportAssistant.Core.Services;
using Microsoft.Extensions.Logging.Abstractions;

namespace SupportAssistant.Tests;

public class ConfigurationServiceTests
{
    [Fact]
    public async Task ConfigurationService_ShouldLoadDefaults()
    {
        // Arrange
        var logger = NullLogger<ConfigurationService>.Instance;
        var service = new ConfigurationService(logger);
        
        // Act
        await service.LoadConfigurationAsync();
        
        // Assert
        Assert.NotNull(service);
        Assert.True(service.UseDirectML); // Default should be true
        Assert.Equal("GPU", service.PreferredDevice); // Default should be GPU
    }

    [Fact]
    public void ConfigurationService_ShouldSetAndGetValues()
    {
        // Arrange
        var logger = NullLogger<ConfigurationService>.Instance;
        var service = new ConfigurationService(logger);
        
        // Act
        service.SetValue("TestKey", "TestValue");
        var result = service.GetValue<string>("TestKey");
        
        // Assert
        Assert.Equal("TestValue", result);
    }
}