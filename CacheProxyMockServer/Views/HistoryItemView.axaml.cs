using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using CacheProxyMockServer.ViewModels;
using System;

namespace CacheProxyMockServer.Views;

public partial class HistoryItemView : UserControl
{
	private readonly HistoryItemViewModel model;

	public HistoryItemView()
    {
        InitializeComponent();
    }

	public HistoryItemView(HistoryItemViewModel model): this()
    {
        this.model = model;
        FillControls();
    }

	void FillControls()
	{
		txtMethod.Text = model.Method;
		txtUrl.Text = model.Url;
		txtTime.Text = model.Time;
		txtLiveOrCache.Text = model.FromCache ? "Cached" : "Live";
		//
		btnRule.Click += delegate { };
		btnDetails.Click += delegate { };
	}

    
	
}