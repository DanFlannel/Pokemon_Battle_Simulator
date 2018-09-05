using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Xml;
//using System.Configuration;

using free.localweather;
using free.locationsearch;
using free.timezone;
using free.marineweather;

using Newtonsoft.Json;

/// <summary>
/// WWO free API.
/// FAQ/Limitation: https://developer.worldweatheronline.com/api/faq.aspx || 
/// Apply your API key: https://developer.worldweatheronline.com/signup.aspx
/// </summary>
public class FreeAPI
{
	public string ApiBaseURL = "http://api.worldweatheronline.com/free/v1/";
	public string FreeApiKey = "xkq544hkar4m69qujdgujn7w";	//<-- This is a test Api Key, please apply your key for production app.

	public string cachedResult;

	public FreeAPI(string freeApiKey)
	{
		FreeApiKey = freeApiKey;
	}

    public LocalWeather GetLocalWeather(LocalWeatherInput input)
    {
        // create URL based on input paramters
        string apiURL = ApiBaseURL + "weather.ashx?q=" + input.query + "&format=" + input.format + "&extra=" + input.extra + "&num_of_days=" + input.num_of_days + "&date=" + input.date + "&fx=" + input.fx + "&cc=" + input.cc + "&includelocation=" + input.includelocation + "&show_comments=" + input.show_comments + "&callback=" + input.callback + "&key="+FreeApiKey;

        // get the web response
		string result = _CheckAndConvertXmlToJson(input.format, RequestHandler.Process(apiURL));
		cachedResult = result;

        // serialize the json output and parse in the helper class
		LocalWeather lWeather = JsonConvert.DeserializeObject<LocalWeather>(result);
        return lWeather;
    }

    public LocationSearch SearchLocation(LocationSearchInput input)
    {
        // create URL based on input paramters
        string apiURL = ApiBaseURL + "search.ashx?q=" + input.query + "&format=" + input.format + "&timezone=" + input.timezone + "&popular=" + input.popular + "&num_of_results=" + input.num_of_results + "&callback=" + input.callback + "&key=" + FreeApiKey;

        // get the web response
		string result = _CheckAndConvertXmlToJson(input.format, RequestHandler.Process(apiURL));
		cachedResult = result;

        // serialize the json output and parse in the helper class
		LocationSearch locationSearch = JsonConvert.DeserializeObject<LocationSearch>(result);
        return locationSearch;
    }

    public Timezone GetTimeZone(TimeZoneInput input)
    {
        // create URL based on input paramters
        string apiURL = ApiBaseURL + "tz.ashx?q=" + input.query + "&format=" + input.format + "&callback=" + input.callback + "&key=" + FreeApiKey;

        // get the web response
		string result = _CheckAndConvertXmlToJson(input.format, RequestHandler.Process(apiURL));
		cachedResult = result;

        // serialize the json output and parse in the helper class
		Timezone timeZone = JsonConvert.DeserializeObject<Timezone>(result);
        return timeZone;
    }

	public MarineWeather GetMarineWeather(MarineWeatherInput input)
    {
        // create URL based on input paramters
        string apiURL = ApiBaseURL + "marine.ashx?q=" + input.query + "&format=" + input.format + "&fx=" + input.fx + "&callback=" + input.callback + "&key=" + FreeApiKey;

        // get the web response
		string result = _CheckAndConvertXmlToJson(input.format, RequestHandler.Process(apiURL));
		cachedResult = result;

        // serialize the json output and parse in the helper class
		MarineWeather mWeather = JsonConvert.DeserializeObject<MarineWeather>(result);
        return mWeather;
    }

	#region ----- WWW -----
	public void GetLocalWeather(LocalWeatherInput input, Action<bool, LocalWeather> onComplete)
	{
		// create URL based on input paramters
		string apiURL = ApiBaseURL + "weather.ashx?q=" + input.query + "&format=" + input.format + "&extra=" + input.extra + "&num_of_days=" + input.num_of_days + "&date=" + input.date + "&fx=" + input.fx + "&cc=" + input.cc + "&includelocation=" + input.includelocation + "&show_comments=" + input.show_comments + "&callback=" + input.callback + "&key="+FreeApiKey;

		RequestHandler.Process(apiURL, (success, result)=>{
			if(success) // Success
			{
				cachedResult = result;
				// serialize the json output and parse in the helper class
				LocalWeather lWeather = JsonConvert.DeserializeObject<LocalWeather>(result);

				onComplete(true, lWeather);
			}
			else // Fail
			{
				onComplete(false, null);
			}
		});
	}

	public void SearchLocation(LocationSearchInput input, Action<bool, LocationSearch> onComplete)
	{
		// create URL based on input paramters
		string apiURL = ApiBaseURL + "search.ashx?q=" + input.query + "&format=" + input.format + "&timezone=" + input.timezone + "&popular=" + input.popular + "&num_of_results=" + input.num_of_results + "&callback=" + input.callback + "&key=" + FreeApiKey;

		RequestHandler.Process(apiURL, (success, result)=>{
			if(success) // Success
			{
				cachedResult = result;
				// serialize the json output and parse in the helper class
				LocationSearch locationSearch = JsonConvert.DeserializeObject<LocationSearch>(result);

				onComplete(true, locationSearch);
			}
			else // Fail
			{
				onComplete(false, null);
			}
		});
	}

	public void GetTimeZone(TimeZoneInput input, Action<bool, Timezone> onComplete)
	{
		// create URL based on input paramters
		string apiURL = ApiBaseURL + "tz.ashx?q=" + input.query + "&format=" + input.format + "&callback=" + input.callback + "&key=" + FreeApiKey;

		RequestHandler.Process(apiURL, (success, result)=>{
			if(success) // Success
			{
				cachedResult = result;
				// serialize the json output and parse in the helper class
				Timezone timeZone = JsonConvert.DeserializeObject<Timezone>(result);

				onComplete(true, timeZone);
			}
			else // Fail
			{
				onComplete(false, null);
			}
		});
	}

	public void GetMarineWeather(MarineWeatherInput input, Action<bool, MarineWeather> onComplete)
	{
		// create URL based on input paramters
		string apiURL = ApiBaseURL + "marine.ashx?q=" + input.query + "&format=" + input.format + "&fx=" + input.fx + "&callback=" + input.callback + "&key=" + FreeApiKey;

		RequestHandler.Process(apiURL, (success, result)=>{
			if(success) // Success
			{
				cachedResult = result;
				// serialize the json output and parse in the helper class
				MarineWeather mWeather = JsonConvert.DeserializeObject<MarineWeather>(result);

				onComplete(true, mWeather);
			}
			else // Fail
			{
				onComplete(false, null);
			}
		});
	}
	#endregion

	private string _CheckAndConvertXmlToJson(string format, string result)
	{
//		if(format.ToUpper() == "XML")
//		{
//			XmlDocument xml = new XmlDocument();
//			xml.LoadXml(result);
//			string jsonString = JsonConvert.SerializeXmlNode(xml);
//			return jsonString;
//		}
		return result;
	}
}