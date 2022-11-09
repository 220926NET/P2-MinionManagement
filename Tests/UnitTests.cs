using DataAccess;

namespace Services.Tests;
public class TransactionTests
{
    private readonly Mock<ITransactionRepo> _mockRepo;
    private readonly TransactionService _service;

    public TransactionTests() {
        _mockRepo = new Mock<ITransactionRepo>();
        _service = new TransactionService(_mockRepo.Object);
    }

    [Fact]
    public void TransferMoney_ActionExecutes_ReturnsBoolean()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.UpdateAccountAmount(It.IsAny<int>(), It.IsAny<decimal>()))
                .Returns(true);
        _mockRepo.Setup(repo => repo.NewTransaction(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>()))
                .Returns(true);

        // Act
        bool? result = _service.TransferMoney(1, 2, 20.00m);

        // Assert
        Assert.IsType<bool>(result);
    }

    [Fact]
    public void TransferMoney_ActionExecutes_WithValidDbParams_ReturnsTrue()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.UpdateAccountAmount(1, -20.00m))
                .Returns(true);
        _mockRepo.Setup(repo => repo.UpdateAccountAmount(2, 20.00m))
                .Returns(true);
        _mockRepo.Setup(repo => repo.NewTransaction(1, 2, 20.00m))
                .Returns(true);

        // Act
        bool? result = _service.TransferMoney(1, 2, 20.00m);

        // Assert
        Assert.IsType<bool>(result);
        Assert.Equal(true, result);
    }

    [Fact]
    public void TransferMoney_ActionExecutes_WithSenderNotInDb_ReturnsNull()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.UpdateAccountAmount(-1, It.IsAny<decimal>()))
                .Returns(false);

        // Act
        bool? result = _service.TransferMoney(-1, 2, 20.00m);

        // Assert
        Assert.Equal(null, result);
    }

    [Fact]
    public void TransferMoney_ActionExecutes_WithReceiverNotInDb_ReturnsFalse()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.UpdateAccountAmount(1, It.IsAny<decimal>()))
                .Returns(true);
        _mockRepo.Setup(repo => repo.UpdateAccountAmount(-2, It.IsAny<decimal>()))
                .Returns(true);

        // Act
        bool? result = _service.TransferMoney(1, -2, 20.00m);

        // Assert
        Assert.IsType<bool>(result);
        Assert.Equal(false, result);
    }

    // [Fact]
    // public void TransferMoney_ActionExecutes_WithInvalidAmount_ReturnsNull()
    // {
    //     // Arrange
    //     _mockRepo.Setup(repo => repo.UpdateAccountAmount(1, -20.00m))
    //             .Returns(true);
    //     _mockRepo.Setup(repo => repo.UpdateAccountAmount(2, 20.00m))
    //             .Returns(true);
    //     _mockRepo.Setup(repo => repo.NewTransaction(1, 2, 20.00m))
    //             .Returns(true);

    //     // Act
    //     bool? result = _service.TransferMoney(1, 2, 20.00m);

    //     // Assert
    //     Assert.IsType<bool>(result);
    // }
}

public class AuthenticationTests
{
    private readonly Mock<IAuthenticationRepo> _mockRepo;
    private readonly AuthenticationService _service;

    public AuthenticationTests() {
        _mockRepo = new Mock<IAuthenticationRepo>();
        _service = new AuthenticationService(_mockRepo.Object);
    }


}

public class AdminTests
{
    private readonly Mock<IAuthenticationRepo> _mockRepo;
    private readonly AuthenticationService _service;

    public AuthenticationTests() {
        _mockRepo = new Mock<IAuthenticationRepo>();
        _service = new AuthenticationService(_mockRepo.Object);
    }
}