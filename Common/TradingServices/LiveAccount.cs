

namespace ScalperPlus.Common.TradingServices;

internal class LiveAccount:ILiveAccount
{
    public event VoidEventHandler? Initialized;
    public virtual void OnInitialized() { Initialized?.Invoke(); Initialized = null; }
    private BinanceBalance? _baseCurrencyBalance;
    public BinanceBalance? BaseBalance { get => _baseCurrencyBalance; set { _baseCurrencyBalance = value;  } }

    private BinanceBalance? _quoteCurrencyBalance;
    public BinanceBalance? QuoteBalance { get => _quoteCurrencyBalance; set { _quoteCurrencyBalance = value; } }
    private bool connection = false;
    private string listenKey = "";

    private IMainSettings _botSettings;
    private ITradeObserver _tradeObserver;

    public LiveAccount(IMainSettings botSettings, ITradeObserver tradeObserver)
    {
        _botSettings = botSettings;
        _tradeObserver = tradeObserver;
        Subscribe();

    }
    private void Subscribe()
    {
        connection = true;
        Exchange api = new Exchange();
        List<BinanceBalance> balances = api.GetBalances(new List<string>() { _botSettings.Settings.BaseCurrency, _botSettings.Settings.QuoteCurrency });
        BaseBalance = balances.Find(a => a.Asset == _botSettings.Settings.BaseCurrency);
        QuoteBalance = balances.Find(a => a.Asset == _botSettings.Settings.QuoteCurrency);

        listenKey = api.SubscribeLiveAccount(
            _botSettings.Symbol,
            a => { },
            onTradeChanged,
            onAccountChanged
        );
        OnInitialized();
        Connect(30);
    }
    private void onTradeChanged(BinanceStreamOrderList data)
    {
        _tradeObserver.ObserveOrder(data);
    }
    private void onAccountChanged(BinanceStreamPositionsUpdate account)
    {
        account.Balances = account.Balances.Where(a => a.Asset == _botSettings.Settings.BaseCurrency || a.Asset == _botSettings.Settings.QuoteCurrency);
        BaseBalance = account.Balances.FirstOrDefault(a => a.Asset == _botSettings.Settings.BaseCurrency)?.ToBinanceBalance();
        QuoteBalance = account.Balances.FirstOrDefault(a => a.Asset == _botSettings.Settings.QuoteCurrency)?.ToBinanceBalance();
    }
    private async void Connect(int interval)
    {
        Exchange api = new Exchange();
        do
        {
            api.KeepAlive(listenKey);
            await Task.Delay(interval * 60000);
        } while (connection);
    }
}
