using Horus_Challenge.Helpers.Utils;
using Newtonsoft.Json;
using System.Windows.Input;

namespace Horus_Challenge.Helpers.UI;

public partial class CustomEntry : Grid
{
	public CustomEntry()
	{
		InitializeComponent();
        Entry.SetValue(Grid.ColumnSpanProperty, 2);
	}

    #region Bindable Props
    public static readonly BindableProperty LabelProperty = BindableProperty.Create(nameof(InputLabel),
        typeof(string),
        typeof(CustomEntry),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (CustomEntry)bindable;
            control.InputLabel.Text = newValue?.ToString();
        });

    public static readonly BindableProperty IconClickedCommandProperty =
            BindableProperty.Create(nameof(IconClickedCommand),
                typeof(ICommand),
                typeof(CustomEntry));
    

    public static readonly BindableProperty UnfocusedCommandProperty =
            BindableProperty.Create(nameof(UnfocusedCommand),
                typeof(ICommand),
                typeof(CustomEntry));

    public static readonly BindableProperty FocusedCommandProperty =
        BindableProperty.Create(nameof(FocusedCommand),
            typeof(ICommand),
            typeof(CustomEntry));

    public static readonly BindableProperty TextChangedCommandProperty =
        BindableProperty.Create(nameof(TextChangedCommand),
            typeof(ICommand),
            typeof(CustomEntry));


    public static readonly BindableProperty ErrorLabelTextProperty =
    BindableProperty.Create(nameof(ErrorLabelText),
        typeof(string),
        typeof(CustomEntry),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (CustomEntry)bindable;
            control.ErrorInfoText.Text = newValue?.ToString();
            control.ErrorInfoText.IsVisible = !string.IsNullOrEmpty(newValue?.ToString()); 
            control.SecondaryVerticalStack.IsVisible = !string.IsNullOrWhiteSpace(newValue?.ToString());
        });

    public static readonly BindableProperty IconSrcProperty =
        BindableProperty.Create(nameof(Icon),
            typeof(string),
            typeof(CustomEntry),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var control = (CustomEntry)bindable;
                control.Icon.Source = newValue?.ToString();
                bool iconVisible = !string.IsNullOrEmpty(newValue?.ToString());
                control.Icon.IsVisible = iconVisible;
                control.Entry.SetValue(Grid.ColumnSpanProperty, iconVisible ? 1 : 2);
            });

    public static readonly BindableProperty PasswordProperty =
        BindableProperty.Create(nameof(Entry),
            typeof(bool),
            typeof(CustomEntry),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var control = (CustomEntry)bindable;
                control.Entry.IsPassword = (bool)newValue;
            });

    public static readonly BindableProperty EntryTextProperty =
        BindableProperty.Create(nameof(Entry),
            typeof(string),
            typeof(CustomEntry),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var control = (CustomEntry)bindable;
                control.Entry.Text = newValue?.ToString();
            });

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(Entry),
            typeof(string),
            typeof(CustomEntry),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var control = (CustomEntry)bindable;
                control.Entry.Placeholder = newValue?.ToString();
            });


    #endregion

    #region Props
    public string LabelText
    {
        get { return (string)GetValue(LabelProperty); }
        set { SetValue(LabelProperty, value); }
    }
    public string IconSrc
    {
        get { return (string)GetValue(IconSrcProperty); }
        set { SetValue(IconSrcProperty, value); }
    }
    public string EntryText
    {
        get { return (string)GetValue(EntryTextProperty); }
        set { SetValue(EntryTextProperty, value); }
    }

    public string Placeholder
    {
        get { return (string)GetValue(PlaceholderProperty); }
        set { SetValue(PlaceholderProperty, value); }
    }

    public bool Password
    {
        get { return (bool)GetValue(PasswordProperty); }
        set { SetValue(PasswordProperty, value); }
    }

    public ICommand FocusedCommand
    {
        get { return (ICommand)GetValue(FocusedCommandProperty); }
        set { SetValue(FocusedCommandProperty, value); }
    }

    public ICommand UnfocusedCommand
    {
        get { return (ICommand)GetValue(UnfocusedCommandProperty); }
        set { SetValue(UnfocusedCommandProperty, value); }
    }

    public ICommand TextChangedCommand
    {
        get { return (ICommand)GetValue(TextChangedCommandProperty); }
        set { SetValue(TextChangedCommandProperty, value); }
    }

    public ICommand IconClickedCommand
    {
        get { return (ICommand)GetValue(IconClickedCommandProperty); }
        set { SetValue(IconClickedCommandProperty, value); }
    }

    public string ErrorLabelText
    {
        get { return (string)GetValue(ErrorLabelTextProperty); }
        set { SetValue(ErrorLabelTextProperty, value); }
    }
    #endregion Props

    private void ShowPassword_Clicked(object sender, EventArgs e)
    {
        if (Password)
            Entry.IsPassword = !Entry.IsPassword;
        if (IconClickedCommand != null && IconClickedCommand.CanExecute(null))
            IconClickedCommand.Execute(null);
    }

    private void Entry_Unfocused(object sender, FocusEventArgs e)
    {
        if (!IsRequiredFieldValid()) return;

        if (this.UnfocusedCommand != null && this.UnfocusedCommand.CanExecute(null))
            this.UnfocusedCommand.Execute(null);
    }

    private void Entry_Focused(object sender, FocusEventArgs e)
    {
        if (this.FocusedCommand != null && this.FocusedCommand.CanExecute(null))
            this.FocusedCommand.Execute(null);
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(Entry.Text)) IsRequiredFieldValid();

        if (this.TextChangedCommand != null && this.TextChangedCommand.CanExecute(null))
            this.TextChangedCommand.Execute(null);
    }
    private bool IsRequiredFieldValid()
    {
        bool isEmpty = string.IsNullOrEmpty(Entry.Text.Trim());

        ErrorInfoText.Text = isEmpty
                        ? Constants.FormValidator.RequieredField
                        : string.Empty;

        ErrorInfoText.IsVisible = isEmpty;

        return !isEmpty;
    }
}