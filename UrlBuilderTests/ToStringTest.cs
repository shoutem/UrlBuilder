namespace UrlBuilderTests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Se.Url;

    [TestClass]
    public class ToStringTest
    {
        [TestMethod]
        public void TestToString()
        {
            var url = new UrlBuilder("http://www.shoutem.local/app");
            url.SetHost("www.shoutem.com");
            url.SetScheme(Uri.UriSchemeHttps);
            url.AddPathSegment("test");
            url.SetQueryParam("nid", 123);
            url.AppendQueryParam("nid", 321);

            Assert.AreEqual("https://www.shoutem.com/app/test?nid=123&nid=321", url.ToString());
        }

        [TestMethod]
        public void TestToRelativeString()
        {
            var url = new UrlBuilder("http://www.shoutem.com/app?nid=123&role=admin");

            Assert.AreEqual("app?nid=123&role=admin", url.ToRelativeString());
        }
    }
}
