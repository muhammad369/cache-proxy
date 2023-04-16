using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Shared.PlatformSupport;
using CacheProxyMockServer.ViewModels;
using System;

namespace CacheProxyMockServer.Views;

public partial class RuleItemView : UserControl
{
	private RuleItemViewModel vm;
	private static IAssetLoader? assets;
	private static Bitmap checkImg, uncheckImg;

	static RuleItemView()
	{
		// assets
		assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
		checkImg = new Bitmap(assets.Open(new Uri("avares://CacheProxyMockServer/Assets/check-64.png")));
		uncheckImg = new Bitmap(assets.Open(new Uri("avares://CacheProxyMockServer/Assets/uncheck-64.png")));
	}

	public RuleItemView()
    {
        InitializeComponent();
    }

    public RuleItemView(RuleItemViewModel vm, Action deleteCallback, Action editCallback)
    {
        InitializeComponent();
        this.vm = vm;
		
		// btn events
		btnActivate.Click += BtnActivate_Click;
		btnDelete.Click += delegate { deleteCallback(); };
		btnEdit.Click += delegate { editCallback(); };
		// display
		display();
	}

	private void display()
	{
		txtMethod.Text = vm.Method;
		txtUrl.Text = vm.Url;
		txtBody.Text = vm.RequestBody;
		btnActivate.Content = vm.IsActive ? "Deactivate" : "Activate";
		imgActive.Source = vm.IsActive ? checkImg : uncheckImg;

	}



	private void BtnActivate_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
	{
		var model = MainWindow.Instance.uow.RulesRepo.GetById(vm.Id);
		model.IsActive = !model.IsActive;
		MainWindow.Instance.uow.RulesRepo.Update(model);
		MainWindow.Instance.uow.Save();
		//
		vm.IsActive = model.IsActive;
		display();
	}
}