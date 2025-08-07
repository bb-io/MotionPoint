using Apps.MotionPoint.Services;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.MotionPoint.Base;

namespace Tests.MotionPoint;

[TestClass]
public class LanguageMappingServiceTests : TestBase
{
    private LanguageMappingService? _languageMappingService;

    [TestInitialize]
    public void Setup()
    {
        _languageMappingService = new LanguageMappingService(InvocationContext);
    }

    [TestMethod]
    public async Task GetQueueIdentifier_WithValidEnEsMxConfiguration_ShouldReturnQueue()
    {
        // Arrange
        var sourceLanguage = "EN";
        var targetLanguage = "ES";
        var country = "MX";

        // Act
        var result = await _languageMappingService!.GetQueueIdentifierAsync(sourceLanguage, targetLanguage, country);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(string.IsNullOrEmpty(result));
        Console.WriteLine($"Successfully found queue: {result}");
    }

    [TestMethod]
    public async Task GetQueueIdentifier_WithInvalidConfiguration_ShouldThrowExceptionWithErrorMessage()
    {
        // Arrange
        var sourceLanguage = "FR";
        var targetLanguage = "DE";
        var country = "AT";

        // Act & Assert
        var exception = await Assert.ThrowsExceptionAsync<PluginApplicationException>(
            () => _languageMappingService!.GetQueueIdentifierAsync(sourceLanguage, targetLanguage, country));

        Assert.IsNotNull(exception.Message);
        Assert.IsFalse(string.IsNullOrEmpty(exception.Message));
        Console.WriteLine($"Exception message: {exception.Message}");
    }
}
