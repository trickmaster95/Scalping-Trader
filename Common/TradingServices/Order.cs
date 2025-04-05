using CryptoExchange.Net.CommonObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalperPlus.Common.TradingServices
{
    internal class Order:IOrder
    {
        private readonly IMainSettings _botSettings;
        private readonly ILiveAccount _liveAccount;
        public Order(IMainSettings botSettings , ILiveAccount liveAccount)
        {
            _botSettings = botSettings;
            _liveAccount = liveAccount;
        }
        public BinanceOrder BuyMarket()
        {
            if (_liveAccount.QuoteBalance != null)
            {
                decimal minBalance = Math.Round(Math.Min(_botSettings.Settings.Balance, _liveAccount.QuoteBalance.Available) * 0.98m, 2);
                if(minBalance > 12)
                {
                    BinanceClients clients = new BinanceClients();
                    BinancePlacedOrder order = clients.Rest.SpotApi.Trading.PlaceOrderAsync(
                        _botSettings.Symbol,
                        OrderSide.Buy,
                        SpotOrderType.Market,
                        quoteQuantity: minBalance
                    ).GetAwaiter().GetResult().Data;
                    return order.ToBinanceOrder();
                }throw new Exception("No Enough Balance");
            } throw new Exception("No Account Data");
        }
        public BinanceOrderOcoList? SellOco( decimal quantity ,decimal takeProfitLimit , decimal stopLoseLimit)
        {
            BinanceOrderOcoList sellOrder = new BinanceClients().Rest.SpotApi.Trading.PlaceOcoOrderAsync(
                _botSettings.Symbol,
                OrderSide.Sell,
                quantity:Math.Floor(quantity * 100000) / 100000,
                price: Math.Floor(takeProfitLimit),
                stopPrice: Math.Floor(stopLoseLimit),
                stopLimitPrice: Math.Floor(stopLoseLimit*0.95m),
                stopLimitTimeInForce: TimeInForce.GoodTillCanceled
            ).GetAwaiter().GetResult().Data;
            return sellOrder;
        }
        public void cancleOrders(List<long> orderIds)
        {
            Task.Run(() => { 
                BinanceClients clients = new BinanceClients();
                foreach (long id in orderIds) {
                    clients.Rest.SpotApi.Trading.CancelOrderAsync(_botSettings.Symbol, id);
                }
            });
        }
    }
}
