

namespace ScalperPlus.Common;

internal static class Extensions
{
    public static Candle ToCandle(this IBinanceKline value)
    {
        return new Candle()
        {
            ClosePrice = value.ClosePrice,
            CloseTime = value.CloseTime,
            TradeCount = value.TradeCount,
            HighPrice = value.HighPrice,
            LowPrice = value.LowPrice,
            OpenPrice = value.OpenPrice,
            OpenTime = value.OpenTime,
            QuoteVolume = value.QuoteVolume,
            TakerBuyQuoteVolume = value.TakerBuyQuoteVolume,
            Volume = value.Volume,
            TakerBuyBaseVolume = value.TakerBuyBaseVolume
        };
    }
    public static BinanceBalance ToBinanceBalance(this BinanceStreamBalance value)
    {
        return new BinanceBalance() { Asset = value.Asset, Available = value.Available, Locked = value.Locked };
    }
    public static BinanceOrder ToBinanceOrder(this BinancePlacedOrder value)
    {
        return new BinanceOrder()
        {
            CreateTime = value.CreateTime,
            ClientOrderId = value.ClientOrderId,
            OriginalClientOrderId = value.OriginalClientOrderId,
            IcebergQuantity = value.IcebergQuantity,
            Id = value.Id,
            SelfTradePreventionMode = value.SelfTradePreventionMode,
            Side = value.Side,
            Status = value.Status,
            StopPrice = value.StopPrice,
            Symbol = value.Symbol,
            IsIsolated = value.IsIsolated,
            IsWorking = value.IsWorking,
            OrderListId = value.OrderListId,
            Price = value.Price,
            Quantity = value.Quantity,
            QuantityFilled = value.QuantityFilled,
            QuoteQuantity = value.QuoteQuantity,
            QuoteQuantityFilled = value.QuoteQuantityFilled,
            TimeInForce = value.TimeInForce,
            TransactTime = value.TransactTime,
            Type = value.Type,
            UpdateTime = value.UpdateTime,
            WorkingTime = value.WorkingTime
        };
    }
    public static BinanceOrder ToBinanceOrder(this BinanceStreamOrderUpdate value)
    {
        return new BinanceOrder()
        {
            CreateTime = value.CreateTime,
            ClientOrderId = value.ClientOrderId,
            OriginalClientOrderId = value.OriginalClientOrderId,
            IcebergQuantity = value.IcebergQuantity,
            Id = value.Id,
            Side = value.Side,
            Status = value.Status,
            StopPrice = value.StopPrice,
            Symbol = value.Symbol,
            IsWorking = value.IsWorking,
            OrderListId = value.OrderListId,
            Price = value.Price,
            Quantity = value.Quantity,
            QuantityFilled = value.QuantityFilled,
            QuoteQuantity = value.QuoteQuantity,
            QuoteQuantityFilled = value.QuoteQuantityFilled,
            TimeInForce = value.TimeInForce,
            Type = value.Type,
            UpdateTime = value.UpdateTime,
            WorkingTime = value.WorkingTime
        };
    }
}
