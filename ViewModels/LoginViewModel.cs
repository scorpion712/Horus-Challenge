using Horus_Challenge.Helpers.Utils;
using Horus_Challenge.Services.Interfaces;
using Horus_Challenge.Views.PopUps;
using Mopups.Services;

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
    bool showPassword = true;

    public bool IsButtonEnabled => !string.IsNullOrEmpty(UserEmail) && EmailValidator.IsEmailValid(UserEmail) && !string.IsNullOrEmpty(Password);
    #endregion

    #region Commands
    [RelayCommand]
    void ShowHidePassword() => ShowPassword = !ShowPassword;


    [RelayCommand]
    private async Task LogIn()
    {
        var user = await authService.SignIn(UserEmail, Password);

        if (user == null)
            await MopupService.Instance.PushAsync(new AlertPage("Ha ocurrido un error al iniciar sesión. Revise sus credenciales y aseguresé de estar conectado a internet."));

        // TO DO: handle navigation to GamificationPage
    }

    [RelayCommand]
    private Task LogInFacebook() => ShowNotAvailablePopUp();

    [RelayCommand]
    private Task LogInInstagram() => ShowNotAvailablePopUp();

    [RelayCommand]
    private Task SignUp() => ShowNotAvailablePopUp();

    [RelayCommand]
    private Task ForgotPassword() => ShowNotAvailablePopUp();
    #endregion

    #region Methods
    private async Task ShowNotAvailablePopUp() => await MopupService.Instance.PushAsync(new AlertPage("Esta función aún no está disponible"));
    #endregion
}
