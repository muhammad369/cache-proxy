using Avalonia.Controls;
using Avalonia.Interactivity;
using CacheProxyMockServer.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;

namespace CacheProxyMockServer
{
	public partial class MainWindow : Window
	{
		public static MainWindow Instance;

		//

		public BindingList<HistoryItemViewModel> historyItems = new BindingList<HistoryItemViewModel>();

		public MainWindow()
		{
			Instance = this;
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
			//
			historyItems.Add(new HistoryItemViewModel() { Id = 34, Time = "Now", Method = "Get", Url = "http://fgf.com/df/dfdf/dfd", FromCache = true });
			historyItems.Add(new HistoryItemViewModel() { Id = 44, Time = "Now", Method = "Get", Url = "http://fgf.com/df/dfdf/dfd", FromCache = true });
			historyItems.Add(new HistoryItemViewModel() { Id = 54, Time = "Now", Method = "Get", Url = "http://fgf.com/df/dfdf/dfd", FromCache = true });
			historyItems.Add(new HistoryItemViewModel() { Id = 74, Time = "Now", Method = "Get", Url = "http://fgf.com/df/dfdf/dfd", FromCache = true });

		}

		private void DelayNumeric_ValueChanged(object? sender, NumericUpDownValueChangedEventArgs e)
		{

		}

		private void ModeCombo_SelectionChanged(object? sender, SelectionChangedEventArgs e)
		{

		}

		private void MainWindow_Initialized(object? sender, System.EventArgs e)
		{
			//ModeCombo.SelectedIndex = 
		}

		private void RulesBtnClick(object sender, RoutedEventArgs e)
		{

		}
	}
}