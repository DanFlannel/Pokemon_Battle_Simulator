using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Xml;
//using System.Configuration;

using premium.localweather;
using premium.locationsearch;
using premium.timezone;
using premium.marineweather;
using premium.pastweather;

using Newtonsoft.Json;

/// <summary>
/// WWO premium API.
/// FAQ/Limitation: https://developer.worldweatheronline.com/api/faq.aspx || 
/// Apply your API key: https://developer.worldweatheronline.com/signup.aspx
/// </summary>
public class PremiumAPI
{
	public string ApiBaseURL = "http://api.worldweatheronline.com/premium/v1/";
	public string PremiumApiKey = "w9ve379xdu8etugm7e2ftxd6"; //<-- This is a test Api Key, please apply your key for production app.

	public string cachedResult;

	public PremiumAPI(string premiumApiKey)
	{
		PremiumApiKey = premiumApiKey;
	}

    public LocalWeather GetLocalWeather(LocalWeatherInput input)
    {
        // create URL based on input paramters
        string apiURL = ApiBaseURL + "weather.ashx?q=" + input.query + "&format=" + input.format + "&extra=" + input.extra + "&num_of_days=" + input.num_of_days + "&date=" + input.date + "&fx=" + input.fx + "&cc=" + input.cc + "&includelocation=" + input.includelocation + "&show_comments=" + input.show_comments + "&callback=" + input.callback + "&key=" + PremiumApiKey;

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
        string apiURL = ApiBaseURL + "search.ashx?q=" + input.query + "&format=" + input.format + "&timezone=" + input.timezone + "&popular=" + input.popular + "&num_of_results=" + input.num_of_results + "&callback=" + input.callback + "&key=" + PremiumApiKey;

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
        string apiURL = ApiBaseURL + "tz.ashx?q=" + input.query + "&format=" + input.format + "&callback=" + input.callback + "&key=" + PremiumApiKey;

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
        string apiURL = ApiBaseURL + "marine.ashx?q=" + input.query + "&format=" + input.format + "&fx=" + input.fx + "&callback=" + input.callback + "&key=" + PremiumApiKey;

        // get the web response
		string result = _CheckAndConvertXmlToJson(input.format, RequestHandler.Process(apiURL));
		cachedResult = result;

        // serialize the json output and parse in the helper class
		MarineWeather mWeather = JsonConvert.DeserializeObject<MarineWeather>(result);
        return mWeather;
    }

    public PastWeather GetPastWeather(PastWeatherInput input)
    {
        // create URL based on input paramters
        string apiURL = ApiBaseURL + "past-weather.ashx?q=" + input.query + "&format=" + input.format + "&extra=" + input.extra + "&enddate=" + input.enddate + "&date=" + input.date + "&includelocation=" + input.includelocation + "&callback=" + input.callback + "&key=" + PremiumApiKey;

        // get the web response
		string result = _CheckAndConvertXmlToJson(input.format, RequestHandler.Process(apiURL));
		cachedResult = result;

        // serialize the json output and parse in the helper class
		PastWeather pWeather = JsonConvert.DeserializeObject<PastWeather>(result);
        return pWeather;
    }

	#region ----- WWW -----
	public void GetLocalWeather(LocalWeatherInput input, Action<bool, LocalWeather> onComplete)
	{
		// create URL based on input paramters
		string apiURL = ApiBaseURL + "weather.ashx?q=" + input.query + "&format=" + input.format + "&extra=" + input.extra + "&num_of_days=" + input.num_of_days + "&date=" + input.date + "&fx=" + input.fx + "&cc=" + input.cc + "&includelocation=" + input.includelocation + "&show_comments=" + input.show_comments + "&callback=" + input.callback + "&key=" + PremiumApiKey;

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
		string apiURL = ApiBaseURL + "search.ashx?q=" + input.query + "&format=" + input.format + "&timezone=" + input.timezone + "&popular=" + input.popular + "&num_of_results=" + input.num_of_results + "&callback=" + input.callback + "&key=" + PremiumApiKey;

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
		string apiURL = ApiBaseURL + "tz.ashx?q=" + input.query + "&format=" + input.format + "&callback=" + input.callback + "&key=" + PremiumApiKey;

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
		string apiURL = ApiBaseURL + "marine.ashx?q=" + input.query + "&format=" + input.format + "&fx=" + input.fx + "&callback=" + input.callback + "&key=" + PremiumApiKey;

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

	public void GetPastWeather(PastWeatherInput input, Action<bool, PastWeather> onComplete)
	{
		// create URL based on input paramters
		string apiURL = ApiBaseURL + "past-weather.ashx?q=" + input.query + "&format=" + input.format + "&extra=" + input.extra + "&enddate=" + input.enddate + "&date=" + input.date + "&includelocation=" + input.includelocation + "&callback=" + input.callback + "&key=" + PremiumApiKey;

		RequestHandler.Process(apiURL, (success, result)=>{
			if(success) // Success
			{
				cachedResult = result;
				// serialize the json output and parse in the helper class
				PastWeather pWeather = JsonConvert.DeserializeObject<PastWeather>(result);

				onComplete(true, pWeather);
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
//
//			string json = JsonConvert.SerializeXmlNode(xml);
//			return json;
//		}
		return result;
	}
}