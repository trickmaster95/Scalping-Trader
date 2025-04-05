

namespace ScalperPlus.Common.Services;

public class Indicators:IIndicators
{
    public decimal Ma(List<IBinanceKline> list, int period = 5, int shift = 0)
    {
        if ((shift + period) <= list.Count)
        {
            return Math.Round(list.SkipLast(shift).TakeLast(period).Average(item => item.ClosePrice), 4);
        }
        else return 0;
    }

    public List<Candle> Rsi(List<Candle> list)
    {
        if (list.Count > 7)
        {
            decimal Gains = 0;
            decimal Losses = 0;
            for (int i = 1; i <= 7; i++)
            {
                decimal dif = Math.Round((list[i].ClosePrice - list[i - 1].ClosePrice) / list[i - 1].ClosePrice, 4);
                Gains += dif > 0 ? dif : 0;
                Losses += dif > 0 ? 0 : (-1 * dif);
            }
            Gains = Math.Round(Gains / 7, 4);
            Losses = Math.Round(Losses / 7, 4);

            list[7].Rsi7 = Losses == 0 ? 100 : Math.Round((100 - (100 / (1 + (Gains / Losses)))), 2);

            if (list.Count > 8)
            {
                for (int i = 8; i < list.Count; i++)
                {

                    decimal dif = Math.Round((list[i].ClosePrice - list[i - 1].ClosePrice) / list[i - 1].ClosePrice, 4);

                    Gains = Math.Round(((Gains * 6) + (dif > 0 ? dif : 0)) / 7, 4);
                    Losses = Math.Round(((Losses * 6) + (dif > 0 ? 0 : (-1 * dif))) / 7, 4);
                    list[i].Rsi7 = Losses == 0 ? 100 : Math.Round((100 - (100 / (1 + (Gains / Losses)))), 2);
                }
            }
            if (list.Count > 14)
            {
                Gains = 0;
                Losses = 0;
                for (int i = 1; i <= 14; i++)
                {
                    decimal dif = Math.Round((list[i].ClosePrice - list[i - 1].ClosePrice) / list[i - 1].ClosePrice, 4);
                    Gains += dif > 0 ? dif : 0;
                    Losses += dif > 0 ? 0 : (-1 * dif);
                }
                Gains = Gains / 14;
                Losses = Losses / 14;

                list[14].Rsi14 = Losses == 0 ? 100 : Math.Round((100 - (100 / (1 + (Gains / Losses)))), 2);

                if (list.Count > 15)
                {
                    for (int i = 15; i < list.Count; i++)
                    {

                        decimal dif = Math.Round((list[i].ClosePrice - list[i - 1].ClosePrice) / list[i - 1].ClosePrice, 4);

                        Gains = Math.Round(((Gains * 13) + (dif > 0 ? dif : 0)) / 14, 4);
                        Losses = Math.Round(((Losses * 13) + (dif > 0 ? 0 : (-1 * dif))) / 14, 4);
                        list[i].Rsi14 = Losses == 0 ? 100 : Math.Round((100 - (100 / (1 + (Gains / Losses)))), 2);
                    }
                }

            }
        }
        return list;
    }
}
