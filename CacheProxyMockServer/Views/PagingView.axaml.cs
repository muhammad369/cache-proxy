using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;

namespace CacheProxyMockServer.Views;

public partial class PagingView : UserControl
{
	private Action nextAction;
	private Action prevAction;

	public PagingView()
    {
        InitializeComponent();
		//
		txtPageNumber.IsReadOnly = true;
		btnNext.Click += BtnNext_Click;
		btnPrevious.Click += BtnPrevious_Click;
    }

	public void SetData(int pageNumber, int pageCapacity, int total)
    {
		var from = (pageNumber-1) * pageCapacity + 1;
		var to = (pageNumber) * pageCapacity;
		if (to > total) to = total;
		//
		txtPageNumber.Text = $"{from} - {to} / {total}";
    }

	public void SetHandlers(Action nextAction, Action prevAction) 
	{
		this.nextAction = nextAction;
		this.prevAction = prevAction;
	}

	private void BtnPrevious_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
	{
		if (prevAction != null) 
		{ 
			prevAction(); 
		}
	}

	private void BtnNext_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
	{
		if (nextAction != null)
		{
			nextAction();
		}
	}

}