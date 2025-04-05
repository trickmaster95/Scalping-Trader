

namespace ScalperPlus.Common.BinanceEssentials;

public class BinanceClients
{
    private string key = "...";
    private string secretKey = "...";
    public BinanceRestClient Rest { get => new BinanceRestClient(options => { options.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(key, secretKey); }); }
    public BinanceSocketClient Socket { get => new BinanceSocketClient(options => { options.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(key, secretKey); }); }

}
