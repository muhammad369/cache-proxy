using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CacheProxyMockServer.ViewModels;
using System;
using System.Linq;

namespace CacheProxyMockServer.Views;

public partial class RulesView : Window
{
    public RulesView()
    {
        InitializeComponent();
		// events
		btnSearch.Click += BtnSearch_Click;
		btnAddRule.Click += BtnAddRule_Click;
		pagingView.SetHandlers(pagingNextBtnClick, pagingPrevBtnClick);
		//
		refreshRulesList();
    }


	#region paging
	int pageSize = 10;
	int pageNumber = 1;
	int total = 0;
	public void refreshRulesList()
	{
		total = MainWindow.Instance.uow.RulesRepo.GetCount(txtSearch.Text ?? "");
		var rules = MainWindow.Instance.uow.RulesRepo.Search(txtSearch.Text ?? "",1, 10);
		rulesList.Items = rules.Select(r => new RuleItemView(new RuleItemViewModel(r), ()=> DeleteRule(r.Id)));
		pagingView.SetData(1, 10, total);
	}

	private void pagingPrevBtnClick()
	{
		if (pageNumber == 1) return;
		//
		total = MainWindow.Instance.uow.RulesRepo.GetCount(txtSearch.Text ?? "");
		var rules = MainWindow.Instance.uow.RulesRepo.Search(txtSearch.Text ?? "", pageNumber - 1, 10);
		rulesList.Items = rules.Select(r => new RuleItemView(new RuleItemViewModel(r), () => DeleteRule(r.Id)));
		//
		pagingView.SetData(pageNumber - 1, 10, total);
		pageNumber--;
	}

	private void pagingNextBtnClick()
	{
		var lastPage = total % pageSize == 0 ? total / pageSize : (total / pageSize) + 1;
		if (pageNumber >= lastPage) return;
		//
		total = MainWindow.Instance.uow.RulesRepo.GetCount(txtSearch.Text ?? "");
		var rules = MainWindow.Instance.uow.RulesRepo.Search(txtSearch.Text ?? "", pageNumber + 1, 10);
		rulesList.Items = rules.Select(r => new RuleItemView(new RuleItemViewModel(r), () => DeleteRule(r.Id)));
		//
		pagingView.SetData(pageNumber + 1, 10, total);
		pageNumber++;
	}
	#endregion

	private async void DeleteRule(int id)
	{
		bool yes = await( new MessageView().ShowDialog<bool>(this));
		if (!yes) return;
		//
		var e = MainWindow.Instance.uow.RulesRepo.GetById(id);
		MainWindow.Instance.uow.RulesRepo.Remove(e);
		MainWindow.Instance.uow.Save();
		//
		refreshRulesList();
	}

	private void BtnAddRule_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
	{
		new RuleDetailsView().ShowDialog(this);
	}

	private void BtnSearch_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
	{
		refreshRulesList();
	}
}