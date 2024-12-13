using Horus_Challenge.Views;

namespace Horus_Challenge.ViewModels;

public partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    [ObservableProperty]
    private bool isRefreshing;

    public bool IsNotBusy => !IsBusy;

    [RelayCommand]
    private async Task GoBack() => await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
}