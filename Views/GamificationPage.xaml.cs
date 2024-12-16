using Horus_Challenge.Models;
using Horus_Challenge.ViewModels;

namespace Horus_Challenge.Views;

public partial class GamificationPage : ContentPage
{
	public GamificationPage(GamificationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as GamificationViewModel).OnAppearing();
    }

    private void OnTappedChallenge(object sender, TappedEventArgs e)
    {
        if (sender is Grid tappedElement)
            if (tappedElement.BindingContext is Challenge tappedItem)
                ((GamificationViewModel)BindingContext).ChallengeTappedCommand.Execute(tappedItem);
    }

    private void OnClickedArrowButton(object sender, EventArgs e)
    {
        if (sender is ImageButton tappedElement)
            if (tappedElement.BindingContext is Challenge tappedItem)
                ((GamificationViewModel)BindingContext).ChallengeTappedCommand.Execute(tappedItem);
    }
}