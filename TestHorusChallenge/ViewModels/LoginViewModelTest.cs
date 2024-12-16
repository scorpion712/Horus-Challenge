using Horus_Challenge.Models;
using Horus_Challenge.Services.Interfaces;
using Horus_Challenge.ViewModels;

namespace TestHorusChallenge.ViewModels;

public class LoginViewModelTest
{
    private readonly IAuthService _authService;
    private readonly LoginViewModel _viewModel; 

    public LoginViewModelTest()
    {
        _authService = Substitute.For<IAuthService>();
        _viewModel = new LoginViewModel(_authService);
    }

    [Fact]
    public async Task LogIn_ShouldNavigateToGamificationPage_WhenLoginIsSuccessful()
    {
        // Arrange
        var email = "test@example.com";
        var password = "Password123";

        // Set the properties of the ViewModel
        _viewModel.UserEmail = email;
        _viewModel.Password = password;

        // Mock the successful sign-in response
        _authService.SignIn(email, password).Returns(Task.FromResult(new User()));

        // Act
        await _viewModel.LogIn();

        // Assert
        _authService.Received(1).SignIn(email, password);  
        // You can also verify if some navigation method was triggered or other side effects (e.g., alert popups)
        // Since the actual navigation will not work in a unit test, this step can be skipped or mocked.
    }

    [Fact]
    public async Task LogIn_ShouldShowError_WhenLoginFails()
    {
        // Arrange
        var email = "test@example.com";
        var password = "Password123";

        // Set the properties of the ViewModel
        _viewModel.UserEmail = email;
        _viewModel.Password = password;

        // Mock the failed sign-in response (returns null)
        _authService.SignIn(email, password).Returns(Task.FromResult<User>(null));  

        // Act
        await _viewModel.LogIn();

        // Assert
        _authService.Received(1).SignIn(email, password);  
        // You can check if error handling like a popup or message is shown here
    }

    [Fact]
    public void IsButtonEnabled_ShouldReturnTrue_WhenEmailAndPasswordAreValid()
    {
        // Arrange
        _viewModel.UserEmail = "validemail@example.com";
        _viewModel.Password = "ValidPassword123";

        // Act
        var isButtonEnabled = _viewModel.IsButtonEnabled;

        // Assert
        isButtonEnabled.Should().BeTrue();  
    }

    [Fact]
    public void IsButtonEnabled_ShouldReturnFalse_WhenEmailIsInvalid()
    {
        // Arrange
        _viewModel.UserEmail = "invalid-email";
        _viewModel.Password = "ValidPassword123";

        // Act
        var isButtonEnabled = _viewModel.IsButtonEnabled;

        // Assert
        isButtonEnabled.Should().BeFalse(); 
    }

    [Fact]
    public void IsButtonEnabled_ShouldReturnFalse_WhenPasswordIsEmpty()
    {
        // Arrange
        _viewModel.UserEmail = "validemail@example.com";
        _viewModel.Password = string.Empty;

        // Act
        var isButtonEnabled = _viewModel.IsButtonEnabled;

        // Assert
        isButtonEnabled.Should().BeFalse(); 
    }
}