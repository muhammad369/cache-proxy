using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CacheProxyMockServer.ViewModels;

namespace CacheProxyMockServer.Views;

public partial class HeaderListItemView : UserControl
{
	private HeaderItemViewModel header;

	public HeaderListItemView() { }

	public HeaderListItemView(HeaderItemViewModel header, bool editable)
    {
        InitializeComponent();
        //
        this.header = header;
        txtKey.Text = header.Key;
        txtValue.Text = header.Value;
        if (editable) btnShow.Content = "Edit";
        btnShow.Click += delegate 
        {
            new HeaderEditView(header, editable).Show();
        };
    }
}