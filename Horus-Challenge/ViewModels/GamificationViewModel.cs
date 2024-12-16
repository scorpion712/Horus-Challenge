using Horus_Challenge.Helpers.Utils;
using Horus_Challenge.Models;
using Horus_Challenge.Services.Interfaces;
using Horus_Challenge.Views.PopUps;
using Mopups.Services;
using System.Collections.ObjectModel;

namespace Horus_Challenge.ViewModels;

public partial class GamificationViewModel(IChallengesService challengesService) : ViewModelBase
{
    private readonly IChallengesService challengesService = challengesService;

    #region Properties
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalChallenges))]
    [NotifyPropertyChangedFor(nameof(CompletedChallenges))]
    ObservableCollection<Challenge> challenges = [];
    public int TotalChallenges => Challenges.Count;
    public int CompletedChallenges => Challenges.Count(c => c.CurrentPoints == c.TotalPoints);

    #endregion

    #region Commands
    [RelayCommand]
    private async Task OnChallengeTapped(Challenge challenge)
   => await MopupService.Instance.PushAsync(new AlertPage($"{challenge.Title}: {challenge.Description}", "Reto elegido"));

    [RelayCommand]
    public async Task Refresh()
    {
        try
        {
            IsRefreshing = true;
            Challenges = [];
            await FetchChallenges();
        }
        finally
        {
            IsRefreshing = false;
        }
    }
    #endregion

    #region Methods
    public async Task FetchChallenges()
    {
        #region Testing

        if (MainThreadHelper.IsTestThread)
        {
            await MainThreadHelper.BeginInvokeOnMainThread(async () =>
            {
                var challengesResponse = await challengesService.GetAll();

                Challenges = new ObservableCollection<Challenge>(challengesResponse ?? []);
            });
            return;
        }
        #endregion
     
        await LoadingAsync(async () =>
        {
            var challengesResponse = await challengesService.GetAll();

            Challenges = new ObservableCollection<Challenge>(challengesResponse ?? []);

        }, showLoading: true);
    }

    public async Task OnAppearing() => await FetchChallenges();
    #endregion
}

