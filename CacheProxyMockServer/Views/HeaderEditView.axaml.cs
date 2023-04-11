using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CacheProxyMockServer.ViewModels;
using System.Collections.Generic;

namespace CacheProxyMockServer.Views;

public partial class HeaderEditView : Window
{
	private HeaderItemViewModel header;

	public HeaderEditView() { }

	public HeaderEditView(HeaderItemViewModel header, bool editable)
    {
        InitializeComponent();
        this.header = header;
        //
        txtKey.Text = header.Key;
        txtValue.Text = header.Value;
        //
        if (!editable)
        {
            txtKey.IsReadOnly = true;
            txtValue.IsReadOnly = true;
            btnSave.IsEnabled = false;
        }
		//
		btnSave.Click += BtnSave_Click;
		btnClose.Click += BtnClose_Click;
    }

	private void BtnClose_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
	{
		Close();
	}

	private void BtnSave_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
	{
        header.Key = txtKey.Text;
        header.Value = txtKey.Text;    
		Close(header);
	}
}