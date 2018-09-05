using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using CryptoCurrency;
using Newtonsoft.Json;

public class CoinMarketCapAPI
{
	private string _CoinIdToString(CMC_CoinId coinId)
	{
		string id = coinId.ToString().Replace("_", "-");
		return id;
	}

	private string _CurrencyToString(CMC_Currency currency)
	{
		string cur = (currency == CMC_Currency.NONE)? "":currency.ToString();
		return cur;
	}

	public enum CMC_CoinId
	{
		Bitcoin = 0,
		Ethereum,
		Ripple,
		Bitcoin_Cash,
		Litecoin,
		Cardano,
		Stellar,
		NEO,
		EOS,
		IOTA,
		Dash,
		NEM,
		Monero,
		Ethereum_Classic,
		Lisk,
		TRON,
		Qtum,
		Tether,
		Bitcoin_Gold,
		VeChain,
	}

	public enum CMC_Currency
	{
		NONE = 0,
		AUD,
		BRL,
		CAD,
		CHF,
		CLP,
		CNY,
		CZK,
		DKK,
		EUR,
		GBP,
		HKD,
		HUF,
		IDR,
		ILS,
		INR,
		JPY,
		KRW,
		MXN,
		MYR,
		NOK,
		NZD,
		PHP,
		PKR,
		PLN,
		RUB,
		SEK,
		SGD,
		THB,
		TRY,
		TWD,
		ZAR,
	}

	public CMC_Ticker[] GetTicker(CMC_Currency currency = CMC_Currency.NONE, int limit = 0, int start = 0)
	{
		return GetTicker(_CurrencyToString(currency), limit, start);
	}

	public CMC_Ticker[] GetTicker(string currency, int limit, int start)
	{
		string apiBaseUrl = "https://api.coinmarketcap.com/v1/ticker/";

		// create URL
		string apiURL = apiBaseUrl + "?limit=" + limit + "&start=" + start + ((string.IsNullOrEmpty(currency))? "":("&convert=" + currency));

		// get the web response
		string result = RequestHandler.Process(apiURL);

		// correct invalid names in the json string, get the convert curreny values
		result = _TickerJsonStringFix(result, currency);

		// serialize the json output and parse in the helper class
		CMC_Ticker[] tickers = JsonConvert.DeserializeObject<CMC_Ticker[]>(result);

		return tickers;
	}


	public CMC_Ticker[] GetTicker(CMC_CoinId coinId, CMC_Currency currency = CMC_Currency.NONE)
	{
		return GetTicker(_CoinIdToString(coinId), _CurrencyToString(currency));
	}

	public CMC_Ticker[] GetTicker(string coinId, string currency)
	{
		string apiBaseUrl = "https://api.coinmarketcap.com/v1/ticker/";

		// create URL
		string apiURL = apiBaseUrl + coinId + "/" + ((string.IsNullOrEmpty(currency))? "":("?convert=" + currency));

		// get the web response
		string result = RequestHandler.Process(apiURL);

		// correct invalid names in the json string, get the convert curreny values
		result = _TickerJsonStringFix(result, currency);

		// serialize the json output and parse in the helper class
		CMC_Ticker[] tickers = JsonConvert.DeserializeObject<CMC_Ticker[]>(result);

		return tickers;
	}


	public CMC_Global GetGlobal(CMC_Currency currency = CMC_Currency.NONE)
	{
		return GetGlobal(_CurrencyToString(currency));
	}

	public CMC_Global GetGlobal(string currency)
	{
		string apiBaseUrl = "https://api.coinmarketcap.com/v1/global/";

		// create URL
		string apiURL = apiBaseUrl + ((string.IsNullOrEmpty(currency))? "":("?convert=" + currency));

		// get the web response
		string result = RequestHandler.Process(apiURL);

		// get the convert curreny values
		result = _GlobalJsonStringFix(result, currency);

		// serialize the json output and parse in the helper class
		CMC_Global global = JsonConvert.DeserializeObject<CMC_Global>(result);

		return global;
	}

	#region ----- WWW -----
	public void GetTicker(Action<bool, CMC_Ticker[]> onComplete, CMC_Currency currency = CMC_Currency.NONE, int limit = 0, int start = 0)
	{
		GetTicker(onComplete, _CurrencyToString(currency), limit, start);
	}

	public void GetTicker(Action<bool, CMC_Ticker[]> onComplete, string currency = "", int limit = 0, int start = 0)
	{
		string apiBaseUrl = "https://api.coinmarketcap.com/v1/ticker/";

		// create URL
		string apiURL = apiBaseUrl + "?limit=" + limit + "&start=" + start + ((string.IsNullOrEmpty(currency))? "":("&convert=" + currency));

		RequestHandler.Process(apiURL, 
			(success, result)=>{
				if(success) // Success
				{
					// correct invalid names in the json string, get the convert curreny values
					result = _TickerJsonStringFix(result, currency);

					// serialize the json output and parse in the helper class
					CMC_Ticker[] tickers = JsonConvert.DeserializeObject<CMC_Ticker[]>(result);

					onComplete(true, tickers);
				}
				else // Fail
				{
					onComplete(false, null);
				}
			}
		);
	}

	public void GetTicker(Action<bool, CMC_Ticker[]> onComplete, CMC_CoinId coinId, CMC_Currency currency = CMC_Currency.NONE)
	{
		GetTicker(onComplete, _CoinIdToString(coinId), _CurrencyToString(currency));
	}

	public void GetTicker(Action<bool, CMC_Ticker[]> onComplete, string coinId, string currency = "")
	{
		string apiBaseUrl = "https://api.coinmarketcap.com/v1/ticker/";

		// create URL
		string apiURL = apiBaseUrl + coinId + "/" + ((string.IsNullOrEmpty(currency))? "":("?convert=" + currency));

		RequestHandler.Process(apiURL, 
			(success, result)=>{
				if(success) // Success
				{
					// correct invalid names in the json string, get the convert curreny values
					result = _TickerJsonStringFix(result, currency);

					// serialize the json output and parse in the helper class
					CMC_Ticker[] tickers = JsonConvert.DeserializeObject<CMC_Ticker[]>(result);

					onComplete(true, tickers);
				}
				else // Fail
				{
					onComplete(false, null);
				}

			}
		);
	}

	public void GetGlobal(Action<bool, CMC_Global> onComplete, CMC_Currency currency = CMC_Currency.NONE)
	{
		GetGlobal(onComplete, _CurrencyToString(currency));
	}

	public void GetGlobal(Action<bool, CMC_Global> onComplete, string currency = "")
	{
		string apiBaseUrl = "https://api.coinmarketcap.com/v1/global/";

		// create URL
		string apiURL = apiBaseUrl + ((string.IsNullOrEmpty(currency))? "":("?convert=" + currency));

		RequestHandler.Process(apiURL, 
			(success, result)=>{ 
				if(success) // Success
				{
					// get the convert curreny values
					result = _GlobalJsonStringFix(result, currency);

					// serialize the json output and parse in the helper class
					CMC_Global cmcGlobal = JsonConvert.DeserializeObject<CMC_Global>(result);

					onComplete(true, cmcGlobal);
				}
				else // Fail
				{
					onComplete(false, null);
				}
			}
		);
	}
	#endregion


	private string _GlobalJsonStringFix(string result, string currency)
	{
		if(!string.IsNullOrEmpty(currency))
		{
			result = result.Replace("total_market_cap_" + currency.ToLower(), "total_market_cap_convert");
			result = result.Replace("total_24h_volume_" + currency.ToLower(), "total_24h_volume_convert");
		}
		return result;
	}

	private string _TickerJsonStringFix(string result, string currency)
	{
		result = result.Replace("24h_", "_24h_");

		if(!string.IsNullOrEmpty(currency))
		{
			result = result.Replace("price_" + currency.ToLower(), "price_convert");
			result = result.Replace("24h_volume_" + currency.ToLower(), "24h_volume_convert");
			result = result.Replace("market_cap_" + currency.ToLower(), "market_cap_convert");
		}
		return result;
	}

}
