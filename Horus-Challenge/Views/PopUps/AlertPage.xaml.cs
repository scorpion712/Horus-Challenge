using Mopups.Pages;

namespace Horus_Challenge.Views.PopUps;

public partial class AlertPage : PopupPage
{
    public AlertPage(string message, string title = "Atenci�n")
    {
        InitializeComponent();
        TitleLabel.Text = title;
        MessageLabel.Text = message;
    }
}