using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using premium.localweather;
using premium.locationsearch;
using premium.timezone;
using premium.marineweather;
using premium.pastweather;

/// <summary>
/// WWO premium API demo.
/// FAQ/Limitation: https://developer.worldweatheronline.com/api/faq.aspx
/// Apply your API key: https://developer.worldweatheronline.com/signup.aspx
/// </summary>
public class WWOPremiumApiDemo : MonoBehaviour
{
	public string m_PremiumApiKey = "Your_Premium_Api_Key";

	public InputField m_InputField_Display;
	public InputField m_InputField_ResultText;

	private string _resultText = "";

	private void _SetDisplay(string result)
	{
		m_InputField_Display.text = result;
	}

	private void _SetResult(string result)
	{
		#if UNITY_EDITOR
		Debug.Log(result);
		#endif
		m_InputField_ResultText.text = result;
	}

	public void OnButtonLocalWeatherPremiumClick()
	{
		// set input parameters for the API
		LocalWeatherInput input = new LocalWeatherInput();
		input.query = "HongKong";
		input.num_of_days = "3";
		input.format = "JSON";

		#if DEPRECATED
		// call the local weather method with input parameters
		PremiumAPI api = new PremiumAPI(m_PremiumApiKey);
		LocalWeather localWeather = api.GetLocalWeather(input);

		// printing a few values to show how to get the output
		_resultText = "LocalWeather Days=" + input.num_of_days + " City=" + input.query + ": " + "\n\nCurrent:";

		for(int i=0; i<localWeather.data.current_Condition.Count; i++)
		{
		_resultText += "\r\n Cloud Cover: " + localWeather.data.current_Condition[i].cloudcover;
		_resultText += "\r\n Humidity: " + localWeather.data.current_Condition[i].humidity;
		_resultText += "\r\n Temp C: " + localWeather.data.current_Condition[i].temp_C;
		_resultText += "\r\n Visibility: " + localWeather.data.current_Condition[i].weatherDesc[0].value;
		_resultText += "\r\n Observation Time: " + localWeather.data.current_Condition[i].observation_time;
		_resultText += "\r\n Pressue: " + localWeather.data.current_Condition[i].pressure;
		}

		_resultText += "\n";

		for(int i=0; i<localWeather.data.weather.Count; i++)
		{
		_resultText += "\n Date: " + localWeather.data.weather[i].date.ToString();
		_resultText += "\r\n Sunrise: " + localWeather.data.weather[i].astronomy[0].sunrise.ToString() + "\n Sunset: " + 
		localWeather.data.weather[i].astronomy[0].sunset.ToString();
		_resultText += "\r\n Temp. C Low:" + localWeather.data.weather[i].mintempC + " High: " + localWeather.data.weather[i].maxtempC;
		if(i < localWeather.data.weather.Count - 1) _resultText += "\n";
		}

		_SetDisplay(_resultText);
		_SetResult(api.cachedResult);


		#else
		// call the local weather method with input parameters
		PremiumAPI api = new PremiumAPI(m_PremiumApiKey);
		api.GetLocalWeather(input, (success, localWeather)=>{
			if(success)
			{
				// printing a few values to show how to get the output
				_resultText = "LocalWeather Days=" + input.num_of_days + " City=" + input.query + ": " + "\n\nCurrent:";

				for(int i=0; i<localWeather.data.current_Condition.Count; i++)
				{
					_resultText += "\r\n Cloud Cover: " + localWeather.data.current_Condition[i].cloudcover;
					_resultText += "\r\n Humidity: " + localWeather.data.current_Condition[i].humidity;
					_resultText += "\r\n Temp C: " + localWeather.data.current_Condition[i].temp_C;
					_resultText += "\r\n Visibility: " + localWeather.data.current_Condition[i].weatherDesc[0].value;
					_resultText += "\r\n Observation Time: " + localWeather.data.current_Condition[i].observation_time;
					_resultText += "\r\n Pressue: " + localWeather.data.current_Condition[i].pressure;
				}

				_resultText += "\n";

				for(int i=0; i<localWeather.data.weather.Count; i++)
				{
					_resultText += "\n Date: " + localWeather.data.weather[i].date.ToString();
					_resultText += "\r\n Sunrise: " + localWeather.data.weather[i].astronomy[0].sunrise.ToString() + "\n Sunset: " + 
						localWeather.data.weather[i].astronomy[0].sunset.ToString();
					_resultText += "\r\n Temp. C Low:" + localWeather.data.weather[i].mintempC + " High: " + localWeather.data.weather[i].maxtempC;
					if(i < localWeather.data.weather.Count - 1) _resultText += "\n";
				}

				_SetDisplay(_resultText);
				_SetResult(api.cachedResult);
			}
		});

		#endif
	}

	public void OnButtonLocationSearchPremiumClick()
	{
		// set input parameters for the API
		LocationSearchInput input = new LocationSearchInput();
		input.query = "Karachi";
		input.num_of_results = "3";
		input.format = "JSON";

		#if DEPRECATED
		// call the location Search method with input parameters
		PremiumAPI api = new PremiumAPI(m_PremiumApiKey);
		LocationSearch locationSearch = api.SearchLocation(input);

		// printing a few values to show how to get the output
		_resultText =  "\r\n Area Name: " + locationSearch.search_API.result[0].areaName[0].value;
		_resultText += "\r\n Country: " + locationSearch.search_API.result[0].country[0].value;
		_resultText += "\r\n Latitude: " + locationSearch.search_API.result[0].latitude;
		_resultText += "\r\n Longitude: " + locationSearch.search_API.result[0].longitude;
		_resultText += "\r\n Population: " + locationSearch.search_API.result[0].population;
		_resultText += "\r\n Region: " + locationSearch.search_API.result[0].region[0].value;
		_SetDisplay(_resultText);
		_SetResult(api.cachedResult);

		#else
		// call the location Search method with input parameters
		PremiumAPI api = new PremiumAPI(m_PremiumApiKey);
		api.SearchLocation(input, (success, locationSearch)=>{
			if(success){
				// printing a few values to show how to get the output
				_resultText = "\r\n Area Name: " + locationSearch.search_API.result [0].areaName [0].value;
				_resultText += "\r\n Country: " + locationSearch.search_API.result [0].country [0].value;
				_resultText += "\r\n Latitude: " + locationSearch.search_API.result [0].latitude;
				_resultText += "\r\n Longitude: " + locationSearch.search_API.result [0].longitude;
				_resultText += "\r\n Population: " + locationSearch.search_API.result [0].population;
				_resultText += "\r\n Region: " + locationSearch.search_API.result [0].region [0].value;
				_SetDisplay (_resultText);
				_SetResult (api.cachedResult);
			}
		});

		#endif
	}

	public void OnButtonTimeZonePremiumClick()
	{
		// set input parameters for the API
		TimeZoneInput input = new TimeZoneInput();
		input.query = "Karachi";
		input.format = "JSON";

		#if DEPRECATED
		// call the location Search method with input parameters
		PremiumAPI api = new PremiumAPI(m_PremiumApiKey);
		Timezone timeZone = api.GetTimeZone(input);

		// printing a few values to show how to get the output
		_resultText =  "\r\n Local Time: " + timeZone.data.time_zone[0].localtime;
		_resultText += "\r\n Time Offset: " + timeZone.data.time_zone[0].utcOffset;
		_SetDisplay(_resultText);
		_SetResult(api.cachedResult);

		#else
		// call the location Search method with input parameters
		PremiumAPI api = new PremiumAPI(m_PremiumApiKey);
		api.GetTimeZone(input, (success, timeZone)=>{
			if(success){
				// printing a few values to show how to get the output
				_resultText = "\r\n Local Time: " + timeZone.data.time_zone [0].localtime;
				_resultText += "\r\n Time Offset: " + timeZone.data.time_zone [0].utcOffset;
				_SetDisplay (_resultText);
				_SetResult (api.cachedResult);
			}
		});

		#endif
	}

	public void OnButtonMarineWeatherPremiumClick()
	{
		// set input parameters for the API
		MarineWeatherInput input = new MarineWeatherInput();
		input.query = "45,-2";
		input.format = "JSON";

		#if DEPRECATED
		// call the location Search method with input parameters
		PremiumAPI api = new PremiumAPI(m_PremiumApiKey);
		MarineWeather marineWeather = api.GetMarineWeather(input);

		// printing a few values to show how to get the output
		_resultText  = "\r\n Date: " + marineWeather.data.weather[0].date;
		_resultText += "\r\n Min Temp (c): " + marineWeather.data.weather[0].mintempC;
		_resultText += "\r\n Max Temp (c): " + marineWeather.data.weather[0].maxtempC;
		_resultText += "\r\n Cloud Cover: " + marineWeather.data.weather[0].hourly[0].cloudcover;
		_SetDisplay(_resultText);
		_SetResult(api.cachedResult);

		#else
		// call the location Search method with input parameters
		PremiumAPI api = new PremiumAPI(m_PremiumApiKey);
		api.GetMarineWeather(input, (success, marineWeather)=>{
			if(success){
				// printing a few values to show how to get the output
				_resultText = "\r\n Date: " + marineWeather.data.weather [0].date;
				_resultText += "\r\n Min Temp (c): " + marineWeather.data.weather [0].mintempC;
				_resultText += "\r\n Max Temp (c): " + marineWeather.data.weather [0].maxtempC;
				_resultText += "\r\n Cloud Cover: " + marineWeather.data.weather [0].hourly [0].cloudcover;
				_SetDisplay (_resultText);
				_SetResult (api.cachedResult);
			}
		});

		#endif
	}

	public void OnButtonPastWeatherPremiumClick()
	{
		// set input parameters for the API
		PastWeatherInput input = new PastWeatherInput();
		input.query = "Karachi";
		input.date = "2017-12-01";
		input.enddate = "2017-12-03";
		input.format = "JSON";

		#if DEPRECATED
		// call the past weather method with input parameters
		PremiumAPI api = new PremiumAPI(m_PremiumApiKey);
		PastWeather pastWeather = api.GetPastWeather(input);

		// printing a few values to show how to get the output
		_resultText =  "\r\n Date: " + pastWeather.data.weather[0].date;
		_resultText += "\r\n Max Temp(C): " + pastWeather.data.weather[0].maxtempC;
		_resultText += "\r\n Max Temp(F): " + pastWeather.data.weather[0].maxtempF;
		_resultText += "\r\n Min Temp(C): " + pastWeather.data.weather[0].mintempC;
		_resultText += "\r\n Min Temp(F): " + pastWeather.data.weather[0].mintempF;
		_resultText += "\r\n Cloud Cover: " + pastWeather.data.weather[0].hourly[0].cloudcover ;
		_SetDisplay(_resultText);
		_SetResult(api.cachedResult);

		#else
		// call the past weather method with input parameters
		PremiumAPI api = new PremiumAPI(m_PremiumApiKey);
		api.GetPastWeather(input, (success, pastWeather)=>{
			if(success){
				// printing a few values to show how to get the output
				_resultText = "\r\n Date: " + pastWeather.data.weather [0].date;
				_resultText += "\r\n Max Temp(C): " + pastWeather.data.weather [0].maxtempC;
				_resultText += "\r\n Max Temp(F): " + pastWeather.data.weather [0].maxtempF;
				_resultText += "\r\n Min Temp(C): " + pastWeather.data.weather [0].mintempC;
				_resultText += "\r\n Min Temp(F): " + pastWeather.data.weather [0].mintempF;
				_resultText += "\r\n Cloud Cover: " + pastWeather.data.weather [0].hourly [0].cloudcover;
				_SetDisplay (_resultText);
				_SetResult (api.cachedResult);
			}
		});

		#endif
	}

}
