using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CacheProxyMockServer.ViewModels;
using System;

namespace CacheProxyMockServer.Views;

public partial class HeaderListItemView : UserControl
{
	private HeaderItemViewModel header;

	public HeaderListItemView() { }

	public HeaderListItemView(HeaderItemViewModel header, bool editable, 
        Action<HeaderItemViewModel> editCallback, Action<HeaderItemViewModel> removeCallback)
    {
        InitializeComponent();
        //
        this.header = header;
        txtKey.Text = header.Key;
        txtValue.Text = header.Value;
        if (editable) btnShow.Content = "Edit";
        else btnRemove.IsVisible = false;
        btnShow.Click += delegate 
        {
            if (editCallback != null) editCallback(header);
        };
        btnRemove.Click += delegate 
        {
            if(removeCallback != null) removeCallback(header);
        };
    }
}