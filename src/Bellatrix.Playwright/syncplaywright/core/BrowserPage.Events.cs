namespace Bellatrix.Playwright.SyncPlaywright;
public partial class BrowserPage
{

    public event EventHandler<BrowserPage> OnClose
    {
        add => WrappedPage.Close += (sender, e) => value(sender, this);
        remove => WrappedPage.Close -= (sender, e) => value(sender, this);
    }

    public event EventHandler<IConsoleMessage> OnConsole
    {
        add => WrappedPage.Console += value;
        remove => WrappedPage.Console -= value;
    }

    public event EventHandler<BrowserPage> OnCrash
    {
        add => WrappedPage.Crash += (sender, e) => value(sender, this);
        remove => WrappedPage.Crash -= (sender, e) => value(sender, this);
    }

    public event EventHandler<Dialog> OnDialog
    {
        add => WrappedPage.Dialog += (sender, e) => value(sender, new Dialog(e));
        remove => WrappedPage.Dialog -= (sender, e) => value(sender, new Dialog(e));
    }

    public event EventHandler<BrowserPage> OnDOMContentLoaded
    {
        add => WrappedPage.DOMContentLoaded += (sender, e) => value(sender, this);
        remove => WrappedPage.DOMContentLoaded -= (sender, e) => value(sender, this);
    }

    public event EventHandler<IDownload> OnDownload
    {
        add => WrappedPage.Download += value;
        remove => WrappedPage.Download -= value;
    }

    public event EventHandler<IFileChooser> OnFileChooser
    {
        add => WrappedPage.FileChooser += value;
        remove => WrappedPage.FileChooser -= value;
    }

    public event EventHandler<IFrame> OnFrameAttached
    {
        add => WrappedPage.FrameAttached += value;
        remove => WrappedPage.FrameAttached -= value;
    }

    public event EventHandler<IFrame> OnFrameDetached
    {
        add => WrappedPage.FrameDetached += value;
        remove => WrappedPage.FrameDetached -= value;
    }

    public event EventHandler<IFrame> OnFrameNavigated
    {
        add => WrappedPage.FrameNavigated += value;
        remove => WrappedPage.FrameNavigated -= value;
    }

    public event EventHandler<BrowserPage> OnLoad
    {
        add => WrappedPage.Load += (sender, e) => value(sender, this);
        remove => WrappedPage.Load -= (sender, e) => value(sender, this);
    }

    public event EventHandler<string> OnPageError
    {
        add => WrappedPage.PageError += value;
        remove => WrappedPage.PageError -= value;
    }

    public event EventHandler<BrowserPage> OnPopup
    {
        add => WrappedPage.Popup += (sender, e) => value(sender, this);
        remove => WrappedPage.Popup -= (sender, e) => value(sender, this);
    }

    public event EventHandler<IRequest> OnRequest
    {
        add => WrappedPage.Request += value;
        remove => WrappedPage.Request -= value;
    }

    public event EventHandler<IRequest> OnRequestFailed
    {
        add => WrappedPage.RequestFailed += value;
        remove => WrappedPage.RequestFailed -= value;
    }

    public event EventHandler<IRequest> OnRequestFinished
    {
        add => WrappedPage.RequestFinished += value;
        remove => WrappedPage.RequestFinished -= value;
    }

    public event EventHandler<IResponse> OnResponse
    {
        add => WrappedPage.Response += value;
        remove => WrappedPage.Response -= value;
    }

    public event EventHandler<IWebSocket> OnWebSocket
    {
        add => WrappedPage.WebSocket += value;
        remove => WrappedPage.WebSocket -= value;
    }

    public event EventHandler<IWorker> OnWorker
    {
        add => WrappedPage.Worker += value;
        remove => WrappedPage.Worker -= value;
    }


}
