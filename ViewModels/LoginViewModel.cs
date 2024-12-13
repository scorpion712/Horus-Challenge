using Horus_Challenge.Helpers.Utils;
using Horus_Challenge.Services.Interfaces;
using Horus_Challenge.Views;
using Horus_Challenge.Views.PopUps;
using Mopups.Services;
using System.Diagnostics;

namespace Horus_Challenge.ViewModels;

public partial class LoginViewModel(IAuthService authService) : ViewModelBase
{
    private readonly IAuthService authService = authService;
    #region Properties
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsButtonEnabled))]
    private string userEmail = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsButtonEnabled))]
    private string password = string.Empty;

    [ObservableProperty]
    string emailErrorMessage = string.Empty;

    [ObservableProperty]
    string passwordErrorMessage = string.Empty;

    [ObservableProperty]
    bool showPassword = true;

    public bool IsButtonEnabled => !string.IsNullOrEmpty(UserEmail) && EmailValidator.IsEmailValid(UserEmail) && !string.IsNullOrEmpty(Password);
    #endregion

    #region Commands
    [RelayCommand]
    void ShowHidePassword() => ShowPassword = !ShowPassword;

    [RelayCommand]
    void ValidateEmail()
    {
        EmailErrorMessage = string.Empty;
        EmailErrorMessage = string.IsNullOrWhiteSpace(UserEmail) ? Constants.FormValidator.RequieredField
                            : !EmailValidator.IsEmailValid(UserEmail) ? Constants.FormValidator.InvalidFormat
                            : string.Empty;
    }

    [RelayCommand]
    void ValidatePassword()
    {
        PasswordErrorMessage = string.Empty;
        PasswordErrorMessage = string.IsNullOrWhiteSpace(Password) ? Constants.FormValidator.RequieredField
                            : string.Empty;
    }

    [RelayCommand]
    private async Task LogIn()
    {
        if (IsBusy) return;

        var user = await authService.SignIn(UserEmail, Password);

        if (user == null)
        {
            await MopupService.Instance.PushAsync(new AlertPage("Ha ocurrido un error al iniciar sesión. Revise sus credenciales y aseguresé de estar conectado a internet."));
            return;
        }

        await Shell.Current.GoToAsync($"//{nameof(GamificationPage)}", true);
    }

    [RelayCommand]
    private async Task LogInFacebook() => await MopupService.Instance.PushAsync(new AlertPage("Facebook"));
    [RelayCommand]
    private async Task LogInInstagram() => await MopupService.Instance.PushAsync(new AlertPage("Instagram"));

    [RelayCommand]
    private async Task SignUp() => await MopupService.Instance.PushAsync(new AlertPage("Registrarme"));

    [RelayCommand]
    private async Task ForgotPassword() => await MopupService.Instance.PushAsync(new AlertPage("Olvidaste tu contraseña?"));
    #endregion

}
