using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CacheProxyMockServer.Http;
using CacheProxyMockServer.Models;
using CacheProxyMockServer.ViewModels;
using System.Collections.Generic;
using CacheProxyMockServer.Http;

namespace CacheProxyMockServer.Views;

public partial class RuleDetailsView : Window
{
	private Rule model;
	//private List<HeaderItemViewModel> responseHeaders;
	private bool addMode = false;

	public RuleDetailsView()
	{
		InitializeComponent();
	}

	public RuleDetailsView(Rule? m)
    {
        InitializeComponent();
		//
		addMode = m == null;
        this.model = m ?? new Rule() { ResponseStatus=200 ,ResponseReason="OK" };
		//
		txtMethod.Items = new string[] 
		{ 
			HttpMethods.Get, HttpMethods.Post, 
			HttpMethods.Put, HttpMethods.Head, 
			HttpMethods.Options, HttpMethods.Delete, 
			HttpMethods.Patch , HttpMethods.Trace
		};
		// events
		btnClose.Click += BtnClose_Click;
		btnSave.Click += BtnSave_Click;
		btnAddHeader.Click += BtnAddHeader_Click;
		//
		this.txtMethod.SelectedItem = model.Method ?? HttpMethods.Get;
		this.txtUrl.Text = model.Url;
		this.txtReqBody.Text = model.RequestBody;
		//
		this.txtResStatus.Text = model.ResponseStatus.ToString();
		this.txtResReason.Text = model.ResponseReason;

		this.headersRes.SetItems(model.ResponseHeaders.ToHeadersList(), true, this);
		this.txtResBody.Text = model.ResponseContent;
		this.txtContentType.Text = model.ResponseContentType;
	}

	private async void BtnAddHeader_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
	{
		var header = await (new HeaderEditView(null, true).ShowDialog<HeaderItemViewModel>(this));
		headersRes.AddHeader(header);
	}

	private void BtnSave_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
	{
		FillModel();
		if (addMode)
		{
			MainWindow.Instance.uow.RulesRepo.Add(model);
		}
		else
		{
			MainWindow.Instance.uow.RulesRepo.Update(model);
		}
		MainWindow.Instance.uow.Save();
		//
		Close(model);
	}

	private void BtnClose_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
	{
		Close(null);
	}

	void FillModel()
	{
		model.Method = txtMethod.SelectedItem as string; 
		model.Url = txtUrl.Text; 
		model.RequestBody = txtReqBody.Text;
		//
		model.ResponseStatus = int.Parse(txtResStatus.Text);
		model.ResponseReason = txtResReason.Text;
		model.ResponseContent = txtResBody.Text;
		model.ResponseHeaders = headersRes.Items.GetHeadersString();
		model.ResponseContentType = txtContentType.Text;
	}
}