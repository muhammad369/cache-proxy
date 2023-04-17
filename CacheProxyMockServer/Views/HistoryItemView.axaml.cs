using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using CacheProxyMockServer.Models;
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
		btnRule.Click += delegate { 
			var rule = MainWindow.Instance.uow.RulesRepo.GetById(model.MatchedRuleId);
			new RuleDetailsView(rule).ShowDialog(MainWindow.Instance);
		};
		btnDetails.Click += delegate {
			var reqDetails = MainWindow.Instance.uow.HistoryItemsRepo.GetById(model.Id);
			new RequestDetailsView(reqDetails!.ToViewModel()).ShowDialog(MainWindow.Instance);
		};
	}

    
	
}