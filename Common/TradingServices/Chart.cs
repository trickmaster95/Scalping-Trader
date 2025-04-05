
namespace ScalperPlus.Common.TradingServices;

internal class Chart:IChart
{
    public event VoidEventHandler? Initialized;
    public virtual void OnInitialized() { Initialized?.Invoke(); Initialized = null; }
    public event VoidEventHandler? ChartUpdated;
    protected virtual void OnChartUpdated() => ChartUpdated?.Invoke();

    private List<Candle> _candles = new List<Candle>();
    public List<Candle> Candles { get => _candles; set { _candles = value; } }

    private readonly IMainSettings _botSettings;
    private readonly GetCandlesOptions _options;
    private readonly IIndicators _indicators;
    private readonly ILiveCandle _liveCandle;
    public Chart(IMainSettings botSettings , IIndicators indicators, ILiveCandle liveCandle)
    {
        _botSettings = botSettings;
        _options = new GetCandlesOptions() { Symbol = _botSettings.Symbol , Interval = KlineInterval.FiveMinutes, EndTime = DateTime.UtcNow };
        _indicators = indicators;
        _liveCandle = liveCandle;
        _liveCandle.CandleOpened += Refresh;
    }
    public void Refresh()
    {
        Task.Run(() =>
        {
            LoadData().GetAwaiter().GetResult();
            if (_candles.Count > 0)
            {
                Candle? last = Candles.LastOrDefault();
                if (last != null) _liveCandle.LastCandle = last;
                if (Initialized != null) OnInitialized();
            }
        });
    }
    private async Task LoadData()
    {
        await Task.Delay(5000);
        _options.EndTime = DateTime.UtcNow;
        Exchange api = new Exchange();
        List<Candle> list = api.GetCandles(_options).SkipLast(1).Select(a => a.ToCandle()).ToList();
        if (list != null) CalculateList(list);

    }
    private void CalculateList(List<Candle> list)
    {
        int shift = 0;
        for (int i = list.Count - 1; i > -1; i--)
        {

            list[i].LocalCloseTime = list[i].CloseTime.ToLocalTime();
            list[i].CloseTime = list[i].OpenTime.ToLocalTime();
            list[i].Ma5 = _indicators.Ma(list.Cast<IBinanceKline>().ToList(), 5, shift);
            list[i].Ma25 = _indicators.Ma(list.Cast<IBinanceKline>().ToList(), 25, shift);
            list[i].Ma99 = _indicators.Ma(list.Cast<IBinanceKline>().ToList(), 99, shift);
            shift++;
        }
        list = _indicators.Rsi(list);
        Candles = list;
    }
}
