using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalperPlus.Common.TradingServices
{
    internal class MainSettings:IMainSettings
    {
        public BotSettings Settings {  get; private set; }
        public string Symbol => $"{Settings.BaseCurrency}{Settings.QuoteCurrency}";

        private readonly IDbContext db;
        private readonly Table<BotSettings> table;
        public MainSettings(IDbContext dbContext)
        {
            db = dbContext;
            table = db.GetTable<BotSettings>();
            if (table == null) throw new Exception("Not found table");
            BotSettings? settings = table.First().GetAwaiter().GetResult();
            if(settings != null)Settings = settings;
            else
            {
                Settings = new BotSettings()
                {
                    BaseCurrency = "BTC",
                    QuoteCurrency = "USDT",
                    Balance = 50,
                    Running = false
                };
                table.Add(Settings);
            }
        }
        public void Save()=>table.Update(Settings);
    }
}
