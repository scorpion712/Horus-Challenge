using Horus_Challenge.Views;
using Horus_Challenge.Views.PopUps;
using Microsoft.Extensions.Logging;
using Mopups.Services;
using System.Diagnostics;

namespace Horus_Challenge.ViewModels;

public partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    [ObservableProperty]
    private bool isRefreshing;

    [ObservableProperty]
    private string busyMessage = string.Empty;

    public bool IsNotBusy => !IsBusy;

    [RelayCommand]
    private async Task GoBack() => await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");

    public async Task LoadingAsync(Func<Task> action, string message = "Aguarde...", bool showLoading = true)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            this.BusyMessage = message;
            this.IsBusy = true; 
            await CloseLoadingPageAsync(); 
            if (showLoading)
                await MopupService.Instance.PushAsync(new LoadingPage(message, (Color)Application.Current.Resources.MergedDictionaries.First()["Primary"]));
        });

        try
        {
            await action();
        }
        catch (UnauthorizedAccessException ex)
        {
            Debug.WriteLine($"{ex}");
        }
        catch (Exception ex)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                this.IsBusy = false;
                this.IsRefreshing = false;
                await CloseLoadingPageAsync();
                await MopupService.Instance.PushAsync(new AlertPage(ex.Message));
            });

            Debug.WriteLine($"{ex}");
        }
        finally
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                this.IsBusy = false;
                this.IsRefreshing = false;
                await CloseLoadingPageAsync();
            });
        }
    }
    private async Task CloseLoadingPageAsync()
    {
        try
        {
            await Task.WhenAll(
                MopupService.Instance.PopupStack
                    .Where(popUp => popUp is LoadingPage)
                    .Select(popUp => MopupService.Instance.RemovePageAsync(popUp))
                    .ToArray()
            );
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Handled Error: {ex.Message}: {ex.StackTrace}");
        }
    }
}