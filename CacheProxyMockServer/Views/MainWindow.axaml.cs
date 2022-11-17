using Avalonia.Controls;
using Avalonia.Interactivity;

namespace CacheProxyMockServer.Views
{
	public partial class MainWindow : Window
	{

		public MainWindow()
		{
			InitializeComponent();
			this.Closing += (s, e) =>
			{
				this.Hide();
				e.Cancel = true;
			};
			this.Initialized += MainWindow_Initialized;
			ModeCombo.SelectionChanged += ModeCombo_SelectionChanged;
			DelayNumeric.ValueChanged += DelayNumeric_ValueChanged;

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