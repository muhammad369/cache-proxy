using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using CacheProxyMockServer.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CacheProxyMockServer.Views;

public partial class HeadersView : UserControl
{
	private List<HeaderItemViewModel> items;
	private Window window;
	private bool editable;

	public List<HeaderItemViewModel> Items { get { return items; } }

	public HeadersView()
    {
        InitializeComponent();
    }

	public void SetItems(List<HeaderItemViewModel> headerItemViewModels, bool editable, Window window)
	{
		this.headersList.Items = headerItemViewModels
			.Select(h => new HeaderListItemView(h, editable, editHeader, removeHeader));
		this.items = headerItemViewModels;
		this.window = window;
		this.editable = editable;
	}

	private void removeHeader(HeaderItemViewModel h)
	{
		items.Remove(h);
		headersList.Items = items
			.Select(h => new HeaderListItemView(h, editable, editHeader, removeHeader));
	}

	private void editHeader(HeaderItemViewModel h)
	{
		new HeaderEditView(h, editable).ShowDialog(window);
		headersList.Items = items
			.Select(h => new HeaderListItemView(h, editable, editHeader, removeHeader));
	}

	public void AddHeader(HeaderItemViewModel h)
	{
		this.items.Add(h);
		headersList.Items = items
			.Select(h => new HeaderListItemView(h, editable, editHeader, removeHeader));
	}
}