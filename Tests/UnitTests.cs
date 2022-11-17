using DataAccess;
using System.Web.Helpers;

namespace Services.Tests;
public class TransactionTests
{
    private readonly Mock<IAccountRepo> _mockRepo;
    private readonly AccountService _service;

    public TransactionTests() {
        _mockRepo = new Mock<IAccountRepo>();
        _service = new AccountService(_mockRepo.Object);
    }

    [Fact]
    public void TransferMoney_ActionExecutes_ReturnsBoolean()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.ChangeAmount(It.IsAny<int>(), It.IsAny<decimal>()))
                .Returns(true);
        _mockRepo.Setup(repo => repo.NewTransaction(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>()))
                .Returns(true);

        // Act
        bool? result = _service.TransferMoney(1, 2, 20.00m);

        // Assert
        _mockRepo.Verify(repo => repo.ChangeAmount(It.IsAny<int>(), It.IsAny<decimal>()), Times.Exactly(2));
        _mockRepo.Verify(repo => repo.NewTransaction(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>()), Times.Once());
        Assert.IsType<bool>(result);
    }

    [Fact]
    public void TransferMoney_ActionExecutes_WithValidDbParams_ReturnsTrue()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.ChangeAmount(It.IsAny<int>(), It.IsAny<decimal>()))
                .Returns(true);
        _mockRepo.Setup(repo => repo.NewTransaction(1, 2, 20.00m))
                .Returns(true);

        // Act
        bool? result = _service.TransferMoney(1, 2, 20.00m);

        // Assert
        _mockRepo.Verify(repo => repo.ChangeAmount(1, -20.00m), Times.Once());
        _mockRepo.Verify(repo => repo.ChangeAmount(2, 20.00m), Times.Once());
        _mockRepo.Verify(repo => repo.NewTransaction(1, 2, 20.00m), Times.Once());
        Assert.IsType<bool>(result);
        Assert.Equal(true, result);
    }

    [Fact]
    public void TransferMoney_ActionExecutes_WithSenderNotInDb_ReturnsNull()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.ChangeAmount(-1, It.IsAny<decimal>()))
                .Returns(false);

        // Act
        bool? result = _service.TransferMoney(-1, 2, 20.00m);

        // Assert
        _mockRepo.Verify(repo => repo.ChangeAmount(It.IsAny<int>(), It.IsAny<decimal>()), Times.Once());
        _mockRepo.Verify(repo => repo.NewTransaction(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>()), Times.Never());
        Assert.Equal(null, result);
    }

    [Fact]
    public void TransferMoney_ActionExecutes_WithReceiverNotInDb_ReturnsFalse()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.ChangeAmount(1, It.IsAny<decimal>()))
                .Returns(true);
        _mockRepo.Setup(repo => repo.ChangeAmount(-2, It.IsAny<decimal>()))
                .Returns(false);

        // Act
        bool? result = _service.TransferMoney(1, -2, 20.00m);

        // Assert
        _mockRepo.Verify(repo => repo.ChangeAmount(1, -20.00m), Times.Once());
        _mockRepo.Verify(repo => repo.ChangeAmount(-2, 20.00m), Times.Once());
        _mockRepo.Verify(repo => repo.ChangeAmount(1, 20.00m), Times.Once());
        _mockRepo.Verify(repo => repo.NewTransaction(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>()), Times.Never());
        Assert.IsType<bool>(result);
        Assert.Equal(false, result);
    }

    // [Fact]
    // public void TransferMoney_ActionExecutes_WithInvalidAmount_ReturnsNull()
    // {
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

    [Fact]
    public void Register_ActionExecutes_ReturnsBoolean()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.UsernameExists(It.IsAny<string>()))
                .Returns(false);

        // Act
        bool result = _service.Register("Username", "Password");

        // Assert
        _mockRepo.Verify(repo => repo.UsernameExists(It.IsAny<string>()), Times.Once());
        _mockRepo.Verify(repo => repo.NewLogIn(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _mockRepo.Verify(repo => repo.NewProfile(It.IsAny<string>()), Times.Once());
        Assert.IsType<bool>(result);
    }

    [Fact]
    public void Register_ActionExecutes_WithValidDbParams_ReturnsTrue()
    {
        // Arrange
        string username = "NewUsername";
        string password = "Password";
        _mockRepo.Setup(repo => repo.UsernameExists(It.IsAny<string>()))
                .Returns(false);

        // Act
        bool result = _service.Register(username, password);

        // Assert
        _mockRepo.Verify(repo => repo.UsernameExists(username), Times.Once());
        _mockRepo.Verify(repo => repo.NewLogIn(username, It.IsAny<string>()), Times.Once());
        _mockRepo.Verify(repo => repo.NewLogIn(username, password), Times.Never());
        _mockRepo.Verify(repo => repo.NewProfile(username), Times.Once());
        Assert.IsType<bool>(result);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Register_ActionExecutes_WithUsernameInDb_ReturnsFalse()
    {
        // Arrange
        string username = "ExistingUsername";
        _mockRepo.Setup(repo => repo.UsernameExists(It.IsAny<string>()))
                .Returns(true);

        // Act
        bool result = _service.Register(username, "Password");

        // Assert
        _mockRepo.Verify(repo => repo.UsernameExists(username), Times.Once());
        _mockRepo.Verify(repo => repo.NewLogIn(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _mockRepo.Verify(repo => repo.NewProfile(It.IsAny<string>()), Times.Never());
        Assert.IsType<bool>(result);
        Assert.Equal(false, result);
    }

    // [Fact]
    // public void Register_ActionExecutes_WithInvalidPassword_ReturnsFalse()
    // {}

    [Fact]
    public void LogIn_ActionExecutes_ReturnsString()
    {
        // Arrange
        string password = "Password";
        _mockRepo.Setup(repo => repo.UsernameExists(It.IsAny<string>()))
                .Returns(true);
        _mockRepo.Setup(repo => repo.GetHash(It.IsAny<string>()))
                .Returns(Crypto.HashPassword(password));
        _mockRepo.Setup(repo => repo.UserId(It.IsAny<string>()))
                .Returns(1);

        // Act
        string? result = _service.LogIn("Username", password);

        // Assert
        _mockRepo.Verify(repo => repo.UsernameExists(It.IsAny<string>()), Times.Once());
        _mockRepo.Verify(repo => repo.GetHash(It.IsAny<string>()), Times.Once());
        _mockRepo.Verify(repo => repo.UserId(It.IsAny<string>()), Times.Once());
        Assert.IsType<string>(result);
    }

    [Fact]
    public void LogIn_ActionExecutes_WithCredentialsInDb_ReturnsString()
    {
        // Arrange
        string username = "ExistingUsername";
        string password = "MatchingPassword";
        _mockRepo.Setup(repo => repo.UsernameExists(It.IsAny<string>()))
                .Returns(true);
        _mockRepo.Setup(repo => repo.GetHash(username))
                .Returns(Crypto.HashPassword(password));
        _mockRepo.Setup(repo => repo.UserId(It.IsAny<string>()))
                .Returns(1);

        // Act
        string? result = _service.LogIn(username, password);

        // Assert
        _mockRepo.Verify(repo => repo.UsernameExists(username), Times.Once());
        _mockRepo.Verify(repo => repo.GetHash(username), Times.Once());
        _mockRepo.Verify(repo => repo.UserId(username), Times.Once());
        Assert.IsType<string>(result);
        Assert.Equal(false, String.IsNullOrWhiteSpace(result));
    }

    // [Fact]
    // public void LogIn_ActionExecutes_WithCredentialsInDb_ReturnsJwtToken()
    // {}

    [Fact]
    public void LogIn_ActionExecutes_WithUsernameNotInDb_ReturnsNull()
    {
        // Arrange
        string username = "Username";
        _mockRepo.Setup(repo => repo.UsernameExists(It.IsAny<string>()))
                .Returns(false);

        // Act
        string? result = _service.LogIn(username, "Password");

        // Assert
        _mockRepo.Verify(repo => repo.UsernameExists(username), Times.Once());
        _mockRepo.Verify(repo => repo.GetHash(It.IsAny<string>()), Times.Never());
        _mockRepo.Verify(repo => repo.UserId(It.IsAny<string>()), Times.Never());
        Assert.Equal(true, String.IsNullOrWhiteSpace(result));
        Assert.Equal(null, result);
    }

    [Fact]
    public void LogIn_ActionExecutes_WithWrongPassword_ReturnsNull()
    {
        // Arrange
        string username = "Username";
        string password = "Password";
        _mockRepo.Setup(repo => repo.UsernameExists(It.IsAny<string>()))
                .Returns(true);
        _mockRepo.Setup(repo => repo.GetHash(It.IsAny<string>()))
                .Returns(password);

        // Act
        string? result = _service.LogIn(username, password);

        // Assert
        _mockRepo.Verify(repo => repo.UsernameExists(username), Times.Once());
        _mockRepo.Verify(repo => repo.GetHash(username), Times.Once());
        _mockRepo.Verify(repo => repo.UserId(It.IsAny<string>()), Times.Never());
        Assert.Equal(true, String.IsNullOrWhiteSpace(result));
        Assert.Equal(null, result);
    }

    // [Fact]
    // public void LogIn_ActionExecutes_WithIdNotInDb_ReturnsNull()
    // {}
}

public class AdminTests
{
    private readonly Mock<IAdminRepo> _mockRepo;
    private readonly AdminService _service;

    public AdminTests() {
        _mockRepo = new Mock<IAdminRepo>();
        _service = new AdminService(_mockRepo.Object);
    }

    [Fact]
    public void AddMoneyToAllUsers_ActionExecutes_ReturnsInt()
    {
        // Arrange
        decimal amount = 20.00m;
        _mockRepo.Setup(repo => repo.AddMoney(It.IsAny<decimal>()))
                .Returns(10);

        // Act
        int result = _service.AddMoneyToAllUsers(amount);

        // Assert
        _mockRepo.Verify(repo => repo.AddMoney(amount), Times.Once());
        Assert.IsType<int>(result);
    }

    // [Fact]
    // public void AddMoneyToAllUsers_ActionExecutes_WithInvalidAmount_ReturnsNull()
    // {
    // }

    [Fact]
    public void RemoveMoneyFromAllUsers_ActionExecutes_ReturnsInt()
    {
        // Arrange
        decimal amount = 20.00m;
        _mockRepo.Setup(repo => repo.RemoveMoney(It.IsAny<decimal>()))
                .Returns(10);

        // Act
        int result = _service.RemoveMoneyFromAllUsers(amount);

        // Assert
        _mockRepo.Verify(repo => repo.RemoveMoney(amount), Times.Once());
        Assert.IsType<int>(result);
    }

    // [Fact]
    // public void RemoveMoneyFromAllUsers_ActionExecutes_WithInvalidAmount_ReturnsNull()
    // {}
}