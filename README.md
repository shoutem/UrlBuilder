# UrlBuilder
UrlBuilder is simple and powerful URL builder.

Get it on NuGet:

`PM> Install-Package UrlBuilder`

````c#
[TestMethod]
public void Test()
{
	var url = new UrlBuilder("http://www.shoutem.local/app");
	url.SetHost("www.shoutem.com").SetScheme(Uri.UriSchemeHttps);
	url.AddPathSegment("test");
	url.SetQueryParam("nid", 123);

	Assert.AreEqual("https://www.shoutem.com/app/test?nid=123", url.ToString());
}
````

Working with query params:
------------------------------
* Get query param
````c#
var url = new UrlBuilder("http://www.shoutem.com");
if (url.ContainsQueryParam("nid"))
{
	var param = url.GetQueryParam("nid");
}
````
* Add and remove query param
````c#
url.SetQueryParam("nid", 123);
url.RemoveQueryParam("nid");
````
* Add and remove query params
````c#
url.SetQueryParams(new Dictionary<string, object> { { "event", 1 }, { "role", "admin" } });
url.RemoveQueryParams(new List<string> { "event", "role" });
````
* Append the value if param already exists
````c#
url.AppendQueryParam("nid", 321);
````

Working with path segments:
-------------------------------
* Get and set path
````c#
url.SetPath("app/events");
url.GetPath();
````
* Add and remove path segment
````c#
url.AddPathSegment("test");
url.RemovePathSegment("test");
````
