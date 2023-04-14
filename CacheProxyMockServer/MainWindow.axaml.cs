using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using Avalonia.Interactivity;
using CacheProxyMockServer.Repositories;
using CacheProxyMockServer.ViewModels;
using CacheProxyMockServer.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CacheProxyMockServer
{
	public partial class MainWindow : Window
	{
		public static MainWindow Instance;
		public UnitOfWork uow;
		//

		public MainWindow()
		{
			Instance = this;
			//
			uow = new UnitOfWork(new Models.AppDbContext());
			//
			InitializeComponent();
			this.Closing += (s, e) =>
			{
				this.Hide();
				e.Cancel = true;
			};
			pagingView.SetHandlers(pagingNextBtnClick, pagingPrevBtnClick);
			//
			this.Initialized += MainWindow_Initialized;
			ModeCombo.SelectionChanged += ModeCombo_SelectionChanged;
			DelayNumeric.ValueChanged += DelayNumeric_ValueChanged;
			DelayNumeric.TextInput += DelayNumeric_TextInput;
			DelayNumeric.ParsingNumberStyle = System.Globalization.NumberStyles.Integer;
			DelayNumeric.Focusable = false;
			Title = "Cache Proxy Mock Server";
			this.ShowInTaskbar = true;

			// display
			RefreshHistory();
			DelayNumeric.Value = uow.SettingsRepo.getDelay();
			ModeCombo.SelectedIndex = uow.SettingsRepo.getMode()? 0: 1;
		}

		#region paging
		int pageSize = 10;
		int pageNumber = 1;
		int total = 0;
		public void RefreshHistory()
		{
			total = uow.HistoryItemsRepo.GetCount();
			var historyItems = uow.HistoryItemsRepo.GetPage(1, 10);
			historyItemsList.Items = historyItems.Select(h => new HistoryItemView(new HistoryItemViewModel(h)));
			pagingView.SetData(1, 10, total);
		}

		private void pagingPrevBtnClick()
		{
			if (pageNumber == 1) return;
			//
			var historyItems = uow.HistoryItemsRepo.GetPage(pageNumber-1, 10);
			historyItemsList.Items = historyItems.Select(h => new HistoryItemView(new HistoryItemViewModel(h)));
			pagingView.SetData(pageNumber-1, 10, total);
			pageNumber--;
		}

		private void pagingNextBtnClick()
		{
			var lastPage = total % pageSize ==0 ? total/pageSize : (total/pageSize) + 1;
			if (pageNumber == lastPage) return;
			//
			var historyItems = uow.HistoryItemsRepo.GetPage(pageNumber+1, 10);
			historyItemsList.Items = historyItems.Select(h => new HistoryItemView(new HistoryItemViewModel(h)));
			pagingView.SetData(pageNumber + 1, 10, total);
			pageNumber++;
		}
		#endregion

		private void DelayNumeric_TextInput(object? sender, Avalonia.Input.TextInputEventArgs e)
		{
			e.Text = DelayNumeric.Value.ToString();
		}

		private void DelayNumeric_ValueChanged(object? sender, NumericUpDownValueChangedEventArgs e)
		{
			if(e.NewValue > DelayNumeric.Maximum)
			{
				DelayNumeric.Value = e.OldValue;
				return;
			}
			//
			uow.SettingsRepo.setDelay((int)e.NewValue);
			uow.Save();
		}

		private void ModeCombo_SelectionChanged(object? sender, SelectionChangedEventArgs e)
		{
			uow.SettingsRepo.setMode(ModeCombo.SelectedIndex == 0);
			uow.Save();
		}

		private void MainWindow_Initialized(object? sender, System.EventArgs e)
		{
			
		}

		private void RulesBtnClick(object sender, RoutedEventArgs e)
		{

		}
	}
}