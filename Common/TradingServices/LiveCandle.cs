

namespace ScalperPlus.Common.TradingServices
{
    internal class LiveCandle:ILiveCandle
    {
        public event VoidEventHandler? Initialized;
        public virtual void OnInitialized() { Initialized?.Invoke();  Initialized = null; }
        public event VoidEventHandler? CandleOpened;
        public virtual void OnCandleOpened() => CandleOpened?.Invoke();
        public event ILiveCandle.CandleUpdatedEventHandler? CandleUpdated;
        public virtual void OnCandleUpdated(IBinanceStreamKlineData? candle) => CandleUpdated?.Invoke(candle);


        private IBinanceStreamKlineData? _candle;
        public IBinanceStreamKlineData? Candle
        {
            get => _candle;
            set
            {
                if(value != null)
                {
                    value.Data.ClosePrice = Math.Round(value.Data.ClosePrice, 4);

                    if (_candle == null) { _candle = value; OnCandleOpened(); }
                    else if (_candle.Data.OpenTime != value?.Data.OpenTime) { _candle = value; OnCandleOpened();  }
                    else _candle = value;
                    OnInitialized();
                    _trallingObserver.ObserveTrailling(value);
                    OnCandleUpdated(value);
                }
            }
        }

        private Candle lastCandle;
        public Candle LastCandle { get=>lastCandle; set { if (lastCandle != value) lastCandle = value; } }

        private IMainSettings _botSettings;
        private ITraillingObserver _trallingObserver;
        public LiveCandle(IMainSettings botSettings, ITraillingObserver tradeBillingObserver)
        {
            _botSettings = botSettings;
            Exchange exchange = new Exchange();
            exchange.SubscribeLiveCandle(_botSettings.Settings.BaseCurrency + _botSettings.Settings.QuoteCurrency, c => Candle = c);
            _trallingObserver = tradeBillingObserver;
        }
    }
}
