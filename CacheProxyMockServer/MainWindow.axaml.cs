using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using Avalonia.Interactivity;
using CacheProxyMockServer.Repositories;
using CacheProxyMockServer.ViewModels;
using CacheProxyMockServer.Views;
using Microsoft.Extensions.DependencyInjection;
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

		public void RefreshHistory()
		{
			var historyItems = uow.HistoryItemsRepo.GetPage(0, 10);
			historyItemsList.Items = historyItems.Select(h => new HistoryItemView(new HistoryItemViewModel(h)));
		}

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