using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using CryptoCurrency;

public class CoinMarketCapApiDemo : MonoBehaviour
{
	public int m_Limit = 0;
	public int m_Start = 0;
	public string m_CoinId = "Bitcoin";
	public string m_Currency = "";

	[Header("[References]")]
	public InputField m_LimitInput;
	public InputField m_StartInput;
	public Dropdown m_CoinIdDropdown;
	public Dropdown m_CurrencyDropdown;

	public Text m_DisplayText;
	public Text m_ShowPageText;
	public Button m_ShowPageButton;
	public Button m_NextButton;
	public Button m_PrevButton;

	[Range(1, 50)] public int m_DisplayLimit = 20;
	private int _currentDisplayIndex = 0;
	private string _outputText = "";

	private CMC_Ticker[] cmcTickers;

	private void Awake()
	{
		OnButtonGlobal();
	}

	public void OnButtonNext()
	{
		if(cmcTickers == null) return;

		_currentDisplayIndex += m_DisplayLimit;

		if(_currentDisplayIndex >= cmcTickers.Length)
		{
			_currentDisplayIndex -= m_DisplayLimit;
		}

		if(_currentDisplayIndex % m_DisplayLimit != 0)
		{
			_currentDisplayIndex = Mathf.FloorToInt((float)_currentDisplayIndex/(float)m_DisplayLimit) * m_DisplayLimit;
		}

		_ShowText();
	}

	public void OnButtonPrevious()
	{
		if(cmcTickers == null) return;

		_currentDisplayIndex -= m_DisplayLimit;

		if(_currentDisplayIndex < 0)
		{
			_currentDisplayIndex = 0;
		}

		if(_currentDisplayIndex % m_DisplayLimit != 0)
		{
			_currentDisplayIndex = Mathf.FloorToInt((float)_currentDisplayIndex/(float)m_DisplayLimit) * m_DisplayLimit;
		}

		_ShowText();
	}

	private void _ShowText()
	{
		int toLimit = ((_currentDisplayIndex + m_DisplayLimit) > cmcTickers.Length)? cmcTickers.Length:_currentDisplayIndex + m_DisplayLimit;
		int toDisplayIndex = Mathf.Clamp(cmcTickers.Length, _currentDisplayIndex, toLimit);

		m_ShowPageText.text = (Mathf.CeilToInt((float)(_currentDisplayIndex + m_DisplayLimit)/(float)m_DisplayLimit) + 0) 
			+ " / " + Mathf.CeilToInt((float)cmcTickers.Length/(float)m_DisplayLimit);

		_outputText = "";
		for(int i=_currentDisplayIndex; i<toDisplayIndex; i++)
		{
			_outputText += "\n[" + i + "] " + cmcTickers[i].symbol + " - " + cmcTickers[i].name + " (id: " + cmcTickers[i].id + ") last updated: " + cmcTickers[i].last_updated;
			_outputText += "\n[Price] BTC : " + cmcTickers[i].price_btc + " USD : " + cmcTickers[i].price_usd + " Vol 24h: " + cmcTickers[i]._24h_volume_usd;
			_outputText += "\n[Cap] Market Cap.: " + cmcTickers[i].market_cap_usd + " Rank: " + cmcTickers[i].rank;
			if(!string.IsNullOrEmpty(m_Currency))
			{
				_outputText += "\n[Convert] " + m_Currency + ": " + cmcTickers[i].price_convert + " Cap: " + cmcTickers[i].market_cap_convert + 
					" Vol 24h: " + cmcTickers[i]._24h_volume_convert;
			}
			_outputText += "\n[Supply] Available: " + cmcTickers[i].available_supply + " Max: " + cmcTickers[i].max_supply + " Total: " + cmcTickers[i].total_supply;
			_outputText += "\n[Changes %] 1h: " + cmcTickers[i].percent_change_1h + " 24h: " + cmcTickers[i].percent_change_24h + 
				" 7d: " + cmcTickers[i].percent_change_7d + "\n";
		}

		m_DisplayText.text = _outputText;
		m_DisplayText.rectTransform.sizeDelta = new Vector2(m_DisplayText.rectTransform.sizeDelta.x, m_DisplayText.preferredHeight);
	}

	public void OnButtonTicker()
	{
		_PrepareValues();

		#if DEPRECATED
		CoinMarketCapAPI api = new CoinMarketCapAPI();
		cmcTickers = api.GetTicker(m_Currency, m_Limit, m_Start);

		_ShowText();

		#else
		CoinMarketCapAPI api = new CoinMarketCapAPI();
		api.GetTicker((success, result)=>{
			if(success)
			{
				cmcTickers = result;
				_ShowText();
			}
		}, m_Currency, m_Limit, m_Start);

		#endif
	}

	public void OnButtonTickerSpecificCoin()
	{
		_PrepareValues();

		#if DEPRECATED
		CoinMarketCapAPI api = new CoinMarketCapAPI();
		cmcTickers = api.GetTicker(m_CoinId, m_Currency);

		_ShowText();

		#else
		CoinMarketCapAPI api = new CoinMarketCapAPI();
		api.GetTicker((success, result)=>{
			if(success)
			{
				cmcTickers = result;
				_ShowText();
			}
		}, m_CoinId, m_Currency);

		#endif
	}

	public void OnButtonGlobal()
	{
		_PrepareValues();

		#if DEPRECATED
		CoinMarketCapAPI api = new CoinMarketCapAPI();
		CMC_Global result = api.GetGlobal(m_Currency);

		_outputText  = "";
		_outputText += "\n[Last Updated] " + result.last_updated;
		_outputText += "\n[Actives] Markets: " + result.active_markets + " Assets: " + result.active_assets + " Currencies: " + result.active_currencies 
			+ " BTC(%):" + result.bitcoin_percentage_of_market_cap;
		_outputText += "\n[Total Cap.] USD: " + result.total_market_cap_usd + " Vol 24h: " + result.total_24h_volume_usd;
		if(!string.IsNullOrEmpty(m_Currency))
		{
			_outputText += "\n[Convert] " + m_Currency + ": " + result.total_market_cap_convert + " Vol 24h: " + result.total_24h_volume_convert;
		}

		m_DisplayText.text = _outputText;
		m_DisplayText.rectTransform.sizeDelta = new Vector2(m_DisplayText.rectTransform.sizeDelta.x, m_DisplayText.preferredHeight);

		#else
		CoinMarketCapAPI api = new CoinMarketCapAPI();
		api.GetGlobal((success, result)=>{
			if(success)
			{
				_outputText  = "";
				_outputText += "\n[Last Updated] " + result.last_updated;
				_outputText += "\n[Actives] Markets: " + result.active_markets + " Assets: " + result.active_assets + " Currencies: " + result.active_currencies 
					+ " BTC(%):" + result.bitcoin_percentage_of_market_cap;
				_outputText += "\n[Total Cap.] USD: " + result.total_market_cap_usd + " Vol 24h: " + result.total_24h_volume_usd;
				if(!string.IsNullOrEmpty(m_Currency))
				{
					_outputText += "\n[Convert] " + m_Currency + ": " + result.total_market_cap_convert + " Vol 24h: " + result.total_24h_volume_convert;
				}

				m_DisplayText.text = _outputText;
				m_DisplayText.rectTransform.sizeDelta = new Vector2(m_DisplayText.rectTransform.sizeDelta.x, m_DisplayText.preferredHeight);
			}
		}, m_Currency);

		#endif
	}

	private void _PrepareValues()
	{
		m_ShowPageText.text = "1 / 1";
		m_DisplayText.rectTransform.anchoredPosition = new Vector2(0,0);
		cmcTickers = null;
		_currentDisplayIndex = 0;

		m_Limit = 1;
		m_Start = 0;
		int.TryParse(m_LimitInput.text, out m_Limit);
		int.TryParse(m_StartInput.text, out m_Start);
		m_CoinId = m_CoinIdDropdown.options[m_CoinIdDropdown.value].text;
		m_Currency = (m_CurrencyDropdown.value == 0)? "":m_CurrencyDropdown.options[m_CurrencyDropdown.value].text;
	}


	public void OnDropdownValueChange_CoinId(Dropdown dropdown)
	{
		Debug.Log("Selected Index: " + dropdown.value);
	}

	public void OnDropdownValueChange_Currency(Dropdown dropdown)
	{
		Debug.Log("Selected Index: " + dropdown.value);
	}

}
