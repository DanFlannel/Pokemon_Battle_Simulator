using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GifSocialShare
{
	#region ----- Social Networks -----
	//*** Developers be aware that not all social network support preview and playback GIF with url. 
	//*** We have tested all links beblow and marked the results here (18/08/2017 by SwanDEV):
	//(A) Social networks those tested can preview and playback GIF: FB, Twitter(Set browser to Desktop Mode), Pinterest, Tumblr, Skype, Reddit(Set browser to Desktop Mode)
	//(B) Social networks those tested can preview GIF only: VK, LineMe, LinkedIn, 
	//(C) Social networks those tested cannot preview & playback GIF: Google+, Weibo, QQZone
	//(D) We cannot test on these social networks for some issues: odnoklassniki, Baidu, 
	//We would greatly appreciate if you can help to test the link in group(D) and report the results. We will mark down the results in the future updates.

	private string facebookTemplate = "https://www.facebook.com/sharer/sharer.php?u={3}";

	private string twitterTemplate = "https://giphy.com/gifs/{2}/tweet"; //"https://giphy.com/gifs/{2}/tweet?twit_auth=1&device=desktop"; //use gif ID (Set browser to Desktop Mode)

	private string twitterMobileTemplate = "https://twitter.com/intent/tweet?url={3}&text={1}&via={2}&hashtags={0}";

	private string tumblrTemplate = "https://www.tumblr.com/widgets/share/tool?canonicalUrl={3}&title={0}&caption={1}"; //"http://www.tumblr.com/share?v=3&u={3}&t={1}";

	private string vkTemplate = "http://vk.com/share.php?title={0}&description={1}&image={2}&url={3}";

	private string pinterestTemplate = "https://pinterest.com/pin/create/button/?url={3}&media={2}&description={1}";

	private string linkedInTemplate = "https://www.linkedin.com/shareArticle?mini=true&url={3}&title={0}&summary={1}";

	private string odnoklassnikiTemplate = "http://www.odnoklassniki.ru/dk?st.cmd=addShare&st.s=1&st._surl={3}&st.comments={1}";

	private string redditTemplate = "https://reddit.com/submit?url={3}&title={0}"; //(Set browser to Desktop Mode)

	private string googlePlusTemplate = "https://plus.google.com/share?url={3}";

	private string qqTemplate = "http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_onekey?url={3}&title{0}&description={1}";

	private string weiboTemplate = "http://service.weibo.com/share/share.php?url={3}&appkey=&title={1}";

	private string baiduTemplate = "http://cang.baidu.com/do/add?it={1}&iu={3}";

	private string mySpaceTemplate = "https://myspace.com/post?u={3}&t={0}&c={1}";	//share to MySpace Stream

	private string lineMeTemplate = "https://lineit.line.me/share/ui?url={3}";  //bitly gif url

	private string skypeTemplate = "https://web.skype.com/share?url={3}";	//url with .gif ext

	public enum Social
	{
		Facebook,
		Twitter,
		Twitter_Mobile,
		Tumblr,
		VK,
		Pinterest,
		LinkedIn,
		Odnoklassniki,
		Reddit,
		GooglePlus,
		QQZone,
		Weibo,
		Baidu,
		MySpace,
		LineMe,
		Skype,
	}

	private string _MakeUrl(Social social, string title = "", string description = "", string image = "", string shareUrl = "")
	{
		string template = string.Empty;
		switch (social)
		{
		case Social.Facebook:
			template = facebookTemplate;
			break;
		case Social.Twitter:
			template = twitterTemplate;
			break;
		case Social.Twitter_Mobile:
			template = twitterMobileTemplate;
			break;
		case Social.Tumblr:
			template = tumblrTemplate;
			break;
		case Social.VK:
			template = vkTemplate;
			break;
		case Social.Pinterest:
			template = pinterestTemplate;
			break;
		case Social.LinkedIn:
			template = linkedInTemplate;
			break;
		case Social.Odnoklassniki:
			template = odnoklassnikiTemplate;
			break;
		case Social.Reddit:
			template = redditTemplate;
			break;
		case Social.GooglePlus:
			template = googlePlusTemplate;
			break;
		case Social.QQZone:
			template = qqTemplate;
			break;
		case Social.Weibo:
			template = weiboTemplate;
			break;
		case Social.Baidu:
			template = baiduTemplate;
			break;
		case Social.MySpace:
			template = mySpaceTemplate;
			break;
		case Social.LineMe:
			template = lineMeTemplate;
			break;
		case Social.Skype:
			template = skypeTemplate;
			break;
		default:
			break;
		}

		return string.Format(template, _EscapeURL(title), _EscapeURL(description), _EscapeURL(image), _EscapeURL(shareUrl));
	}

	public void ShareTo(Social socialNetwork, string title = "", string description = "", string image = "", string shareUrl = "")
	{
		string url = _MakeUrl(socialNetwork, title, description, image, shareUrl);
		_Publish(url);
	}

	#endregion


	#region ----- Email -----
	public void SendEmail(string toMailAddress, string subject, string body) {
		string url = "mailto:" + toMailAddress +
			"?subject=" + _EscapeURL(subject) +
			"&body=" + _EscapeURL(body);
		_Publish(url);
	}

	#endregion


	#region ----- Common -----
	private string _EscapeURL(string url)
	{
		return WWW.EscapeURL(url).Replace("+", "%20");
	}

	private void _Publish(string url)
	{
		Application.OpenURL(url);
	}

	#endregion
}
