# UrlBuilder
UrlBuilder is simple and powerful URL builder.

````c#
[TestMethod]
public void Test()
{
	var url = new UrlBuilder("http://www.shoutem.local/app");
	url.SetHost("www.shoutem.com").SetScheme(Uri.UriSchemeHttps);
	url.AddPathSegment("test");
	url.SetQueryParam("nid", 123);
	url.AppendQueryParam("nid", 321);

	Assert.AreEqual("https://www.shoutem.com/app/test?nid=123&nid=321", url.ToString());
}
````

Get it on NuGet:

`PM> Install-Package UrlBuilder`
