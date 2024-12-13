using Mopups.Pages;

namespace Horus_Challenge.Views.PopUps;

public partial class LoadingPage : PopupPage
{
    /// <summary>
    /// Loading popup
    /// </summary>
    /// <param name="message">Message to show</param>
    /// <param name="indicatorColor">Color for loading indicator HEX</param>
	public LoadingPage(string message, Color indicatorColor = null)
    {
        InitializeComponent();
        Title.Text = message;
        Indicator.Color = indicatorColor ?? Color.FromRgba("#000");
    }
}