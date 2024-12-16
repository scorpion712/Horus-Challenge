using Mopups.Pages;

namespace Horus_Challenge.Views.PopUps;

public partial class AlertPage : PopupPage
{
    public AlertPage(string message, string title = "Atención")
    {
        InitializeComponent();
        TitleLabel.Text = title;
        MessageLabel.Text = message;
    }
}