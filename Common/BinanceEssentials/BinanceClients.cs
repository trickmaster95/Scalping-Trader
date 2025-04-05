

namespace ScalperPlus.Common.BinanceEssentials;

public class BinanceClients
{
    private string key = "XnuKawNmPwkx6qiBputdFCWzK7YS4F8V8nnHMkN11aHX60o2y0myaQKT3Qzb65BU";
    private string secretKey = "ns8spLlx8CTUDZeKR5uwPuIEi638MNid9C4AcOOIZYYFZ7FwdcUmbXWQ6vJGYVeO";
    public BinanceRestClient Rest { get => new BinanceRestClient(options => { options.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(key, secretKey); }); }
    public BinanceSocketClient Socket { get => new BinanceSocketClient(options => { options.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(key, secretKey); }); }

}
