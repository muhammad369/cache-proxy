using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CacheProxyMockServer.Http;
using CacheProxyMockServer.ViewModels;

namespace CacheProxyMockServer.Views;

public partial class RequestDetailsView : Window
{
	private RequestDetailsViewModel model;

	public RequestDetailsView()
    {
        InitializeComponent();
    }

    public RequestDetailsView(RequestDetailsViewModel model)
    {
        InitializeComponent();
        this.model = model;
        //
        txtMethod.IsReadOnly = txtUrl.IsReadOnly = txtReqBody.IsReadOnly = true;
        txtResStatus.IsReadOnly = txtResBody.IsReadOnly = txtResReason.IsReadOnly = true; 
        //
        this.txtMethod.Text = model.Method;
        this.txtUrl.Text = model.Url;
        this.headersReq.SetChildren( model.RequestHeaders.ToHeadersList(), false, this);
        this.txtReqBody.Text = model.RequestBody;
        //
        this.txtResStatus.Text = model.ResponseStatus.ToString();
        this.txtResReason.Text = model.ResponseReason;
        this.headersRes.SetChildren(model.ResponseHeaders.ToHeadersList(), false, this);
        this.txtResBody.Text = model.ResponseContent;
    }
}