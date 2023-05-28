using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Avalonia.VisualTree;
using CacheProxyMockServer.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

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

	private async void removeHeader(HeaderItemViewModel h)
	{
		items.Remove(h);
		await Dispatcher.UIThread.InvokeAsync(async () => _refeshItems());
	}

	private async void editHeader(HeaderItemViewModel h)
	{
		new HeaderEditView(h, editable).ShowDialog(window);
		await Dispatcher.UIThread.InvokeAsync(async () => _refeshItems());
	}

	public async void AddHeader(HeaderItemViewModel h)
	{
		this.items.Add(h);
		await Dispatcher.UIThread.InvokeAsync(async () => _refeshItems());
	}

	async void _refeshItems()
	{
		headersList.Items = items
			.Select(h => new HeaderListItemView(h, editable, editHeader, removeHeader));
	}
}