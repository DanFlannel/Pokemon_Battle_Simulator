using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using free.localweather;
using free.locationsearch;
using free.timezone;
using free.marineweather;

/// <summary>
/// WWO free API demo.
/// FAQ/Limitation: https://developer.worldweatheronline.com/api/faq.aspx || 
/// Apply your API key: https://developer.worldweatheronline.com/signup.aspx
/// </summary>
public class WWOFreeApiDemo : MonoBehaviour
{
	public string m_FreeApiKey = "Your_Free_Api_Key";

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

	public void OnButtonLocalWeatherFreeClick()
	{
		// set input parameters for the API
		LocalWeatherInput input = new LocalWeatherInput();
		input.query = "Karachi";
		input.num_of_days = "2";
		input.format = "JSON";

		#if DEPRECATED
		// call the local weather method with input parameters
		FreeAPI api = new FreeAPI(m_FreeApiKey);
		LocalWeather localWeather = api.GetLocalWeather(input);

		// printing a few values to show how to get the output
		_resultText  = "\r\n Cloud Cover: " + localWeather.data.current_Condition[0].cloudcover;
		_resultText += "\r\n Humidity: " + localWeather.data.current_Condition[0].humidity;
		_resultText += "\r\n Temp C: " + localWeather.data.current_Condition[0].temp_C;
		_resultText += "\r\n Visibility: " + localWeather.data.current_Condition[0].weatherDesc[0].value;
		_resultText += "\r\n Observation Time: " + localWeather.data.current_Condition[0].observation_time;
		_resultText += "\r\n Pressue: " + localWeather.data.current_Condition[0].pressure;
		_SetDisplay(_resultText);
		_SetResult(api.cachedResult);

		#else
		// call the local weather method with input parameters
		FreeAPI api = new FreeAPI(m_FreeApiKey);
		api.GetLocalWeather(input, (success, localWeather)=>{
			if(success)
			{
				// printing a few values to show how to get the output
				_resultText  = "\r\n Cloud Cover: " + localWeather.data.current_Condition[0].cloudcover;
				_resultText += "\r\n Humidity: " + localWeather.data.current_Condition[0].humidity;
				_resultText += "\r\n Temp C: " + localWeather.data.current_Condition[0].temp_C;
				_resultText += "\r\n Visibility: " + localWeather.data.current_Condition[0].weatherDesc[0].value;
				_resultText += "\r\n Observation Time: " + localWeather.data.current_Condition[0].observation_time;
				_resultText += "\r\n Pressue: " + localWeather.data.current_Condition[0].pressure;
				_SetDisplay(_resultText);
				_SetResult(api.cachedResult);
			}
		});

		#endif
	}

	public void OnButtonLocationSearchFreeClick()
	{
		// set input parameters for the API
		LocationSearchInput input = new LocationSearchInput();
		input.query = "Karachi";
		input.num_of_results = "3";
		input.format = "JSON";


		#if DEPRECATED
		// call the location Search method with input parameters
		FreeAPI api = new FreeAPI(m_FreeApiKey);
		LocationSearch locationSearch = api.SearchLocation(input);

		// printing a few values to show how to get the output
		_resultText  = "\r\n Area Name: " + locationSearch.search_API.result[0].areaName[0].value;
		_resultText += "\r\n Country: " + locationSearch.search_API.result[0].country[0].value;
		_resultText += "\r\n Latitude: " + locationSearch.search_API.result[0].latitude;
		_resultText += "\r\n Longitude: " + locationSearch.search_API.result[0].longitude;
		_resultText += "\r\n Population: " + locationSearch.search_API.result[0].population;
		_resultText += "\r\n Region: " + locationSearch.search_API.result[0].region[0].value;
		_SetDisplay(_resultText);
		_SetResult(api.cachedResult);

		#else
		// call the location Search method with input parameters
		FreeAPI api = new FreeAPI(m_FreeApiKey);
		api.SearchLocation(input, (success, locationSearch)=>{
			if(success)
			{
				// printing a few values to show how to get the output
				_resultText  = "\r\n Area Name: " + locationSearch.search_API.result[0].areaName[0].value;
				_resultText += "\r\n Country: " + locationSearch.search_API.result[0].country[0].value;
				_resultText += "\r\n Latitude: " + locationSearch.search_API.result[0].latitude;
				_resultText += "\r\n Longitude: " + locationSearch.search_API.result[0].longitude;
				_resultText += "\r\n Population: " + locationSearch.search_API.result[0].population;
				_resultText += "\r\n Region: " + locationSearch.search_API.result[0].region[0].value;
				_SetDisplay(_resultText);
				_SetResult(api.cachedResult);
			}
		});

		#endif
	}

	public void OnButtonTimeZoneFreeClick()
	{
		// set input parameters for the API
		TimeZoneInput input = new TimeZoneInput();
		input.query = "Karachi";
		input.format = "JSON";

		#if DEPRECATED
		// call the location Search method with input parameters
		FreeAPI api = new FreeAPI(m_FreeApiKey);
		Timezone timeZone = api.GetTimeZone(input);

		// printing a few values to show how to get the output
		_resultText  = "\r\n Local Time: " + timeZone.data.time_zone[0].localtime;
		_resultText += "\r\n Time Offset: " + timeZone.data.time_zone[0].utcOffset;
		_SetDisplay(_resultText);
		_SetResult(api.cachedResult);

		#else
		// call the location Search method with input parameters
		FreeAPI api = new FreeAPI(m_FreeApiKey);
		api.GetTimeZone(input, (success, timeZone)=>{
			if(success)
			{
				// printing a few values to show how to get the output
				_resultText = "\r\n Local Time: " + timeZone.data.time_zone [0].localtime;
				_resultText += "\r\n Time Offset: " + timeZone.data.time_zone [0].utcOffset;
				_SetDisplay (_resultText);
				_SetResult (api.cachedResult);
			}
		});

		#endif
	}

	public void OnButtonMarineWeatherFreeClick()
	{
		// set input parameters for the API
		MarineWeatherInput input = new MarineWeatherInput();
		input.query = "45,-2";
		input.format = "JSON";

		#if DEPRECATED
		// call the location Search method with input parameters
		FreeAPI api = new FreeAPI(m_FreeApiKey);
		MarineWeather marineWeather = api.GetMarineWeather(input);

		// printing a few values to show how to get the output
		_resultText  = "\r\n Distance(miles): " + marineWeather.data.nearest_area[0].distance_miles;
		_resultText += "\r\n Latitude: " + marineWeather.data.nearest_area[0].latitude;
		_resultText += "\r\n Longitude: " + marineWeather.data.nearest_area[0].longitude;
		_resultText += "\r\n Date: " + marineWeather.data.weather[0].date;
		_resultText += "\r\n Min Temp (c): " + marineWeather.data.weather[0].mintempC;
		_resultText += "\r\n Max Temp (c): " + marineWeather.data.weather[0].maxtempC;
		_resultText += "\r\n Cloud Cover: " + marineWeather.data.weather[0].hourly[0].cloudcover;
		_SetDisplay(_resultText);
		_SetResult(api.cachedResult);

		#else
		// call the location Search method with input parameters
		FreeAPI api = new FreeAPI(m_FreeApiKey);
		api.GetMarineWeather(input, (success, marineWeather)=>{
			if(success){
				// printing a few values to show how to get the output
				_resultText = "\r\n Distance(miles): " + marineWeather.data.nearest_area [0].distance_miles;
				_resultText += "\r\n Latitude: " + marineWeather.data.nearest_area [0].latitude;
				_resultText += "\r\n Longitude: " + marineWeather.data.nearest_area [0].longitude;
				_resultText += "\r\n Date: " + marineWeather.data.weather [0].date;
				_resultText += "\r\n Min Temp (c): " + marineWeather.data.weather [0].mintempC;
				_resultText += "\r\n Max Temp (c): " + marineWeather.data.weather [0].maxtempC;
				_resultText += "\r\n Cloud Cover: " + marineWeather.data.weather [0].hourly [0].cloudcover;
				_SetDisplay (_resultText);
				_SetResult (api.cachedResult);
			}
		});

		#endif
	}

}
