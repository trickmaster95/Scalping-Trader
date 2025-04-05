using Microsoft.Extensions.Logging;
using ScalperPlus.Common.Services;

namespace ScalperPlus
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            builder.Services.AddSingleton<IDbContext, DbContext>();
            builder.Services.AddSingleton<ILiveCandle , LiveCandle>();
            builder.Services.AddSingleton<ILiveAccount , LiveAccount>();
            builder.Services.AddTransient<IIndicators , Indicators>();
            builder.Services.AddSingleton<IChart,Chart>();
            builder.Services.AddScoped<IMainSettings,MainSettings>();
            builder.Services.AddScoped<ITransactionManager, TransactionManager>();

            builder.Services.AddScoped<IOrder, Order>();
            builder.Services.AddScoped<IMarketState, MarketState>();
            builder.Services.AddScoped<IChanceDetector, ChanceDetector>();
            builder.Services.AddScoped<ITradeTester, TradeTester>();
            builder.Services.AddScoped<ITradeObserver, TradeObserver>();
            builder.Services.AddScoped<ITraillingObserver, TraillingObserver>();    


#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
