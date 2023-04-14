using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CacheProxyMockServer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CacheProxyMockServer.Views;

public partial class HeadersView : UserControl
{
    public HeadersView()
    {
        InitializeComponent();
    }

	public void SetChildren(List<HeaderItemViewModel> headerItemViewModels, bool editable)
	{
		this.headersList.Items = headerItemViewModels.Select(h => new HeaderListItemView(h, editable));
	}
}