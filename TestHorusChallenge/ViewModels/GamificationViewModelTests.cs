using Horus_Challenge.Models;
using Horus_Challenge.Services.Interfaces;
using Horus_Challenge.ViewModels;
using System.Collections.ObjectModel;

namespace TestHorusChallenge.ViewModels;

public class GamificationViewModelTests
{
    private readonly IChallengesService _challengesService;
    private readonly GamificationViewModel _viewModel;

    public GamificationViewModelTests()
    {
        _challengesService = Substitute.For<IChallengesService>();
        _viewModel = new GamificationViewModel(_challengesService);
    }

    [Fact]
    public void TotalChallenges_ShouldReturnCorrectCount()
    {
        // Arrange
        _viewModel.Challenges = new ObservableCollection<Challenge>
        {
            new Challenge(),
            new Challenge()
        };

        // Act
        var totalChallenges = _viewModel.TotalChallenges;

        // Assert
        totalChallenges.Should().Be(2);
    }

    [Fact]
    public void CompletedChallenges_ShouldReturnCorrectCount()
    {
        // Arrange
        _viewModel.Challenges = new ObservableCollection<Challenge>
        {
            new Challenge { CurrentPoints = 10, TotalPoints = 10 },
            new Challenge { CurrentPoints = 5, TotalPoints = 10 }
        };

        // Act
        var completedChallenges = _viewModel.CompletedChallenges;

        // Assert
        completedChallenges.Should().Be(1);
    }

    [Fact]
    public async Task Refresh_ShouldCallFetchChallengesAndUpdateChallengesList()
    {
        // Arrange
        var challenges = new[] { new Challenge { Title = "Test Challenge" } };

        _challengesService.GetAll().Returns(challenges);

        // Act
        await _viewModel.Refresh();

        // Assert
        _viewModel.IsRefreshing.Should().BeFalse();
        _viewModel.Challenges.Should().HaveCount(1);
        _viewModel.Challenges[0].Title.Should().Be("Test Challenge");
    }

    [Fact]
    public async Task OnAppearing_ShouldCallFetchChallenges()
    {
        // Arrange
        var challenges = new[] { new Challenge { Title = "Test Challenge" } };
        _challengesService.GetAll().Returns(challenges);

        // Act
        await _viewModel.OnAppearing();

        // Assert
        _viewModel.Challenges.Should().HaveCount(1);
        _viewModel.Challenges[0].Title.Should().Be("Test Challenge");
    } 
}
