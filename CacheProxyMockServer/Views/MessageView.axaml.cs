using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CacheProxyMockServer.Views;

public partial class MessageView : Window
{
	public MessageView()
	{
		InitializeComponent();
		//
		//
		btnYes.Click += BtnYes_Click;
		btnNo.Click += BtnNo_Click;
	}

    public MessageView(string msg = "Are you sure?", string yes = "Yes", string no = "No")
    {
        InitializeComponent();
        //
        txtMessage.Text = msg;
        btnYes.Content = yes;
        btnNo.Content = no;
		//
		btnYes.Click += BtnYes_Click;
		btnNo.Click += BtnNo_Click;
    }

	private void BtnNo_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
	{
		Close(false);
	}

	private void BtnYes_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
	{
		Close(true);
	}
}