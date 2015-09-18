namespace UrlBuilderTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Se.Url;

    [TestClass]
    public class SchemeTest
    {
        [TestMethod]
        public void DefaultScheme()
        {
            var url = new UrlBuilder("www.shoutem.com");

            Assert.AreEqual(Uri.UriSchemeHttp, url.GetScheme());
        }

        [TestMethod]
        public void DefaultSchemeFromUrl()
        {
            var url1 = new UrlBuilder("http://www.shoutem.com");
            var url2 = new UrlBuilder("https://www.shoutem.com");
            var url3 = new UrlBuilder("ftp://www.shoutem.com");

            Assert.AreEqual(Uri.UriSchemeHttp, url1.GetScheme());
            Assert.AreEqual(Uri.UriSchemeHttps, url2.GetScheme());
            Assert.AreEqual(Uri.UriSchemeFtp, url3.GetScheme());
        }

        [TestMethod]
        public void SetScheme()
        {
            var url = new UrlBuilder("www.shoutem.com");
            url.SetScheme(Uri.UriSchemeHttp);

            Assert.AreEqual(Uri.UriSchemeHttp, url.GetScheme());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetSchemeToNull()
        {
            var url = new UrlBuilder("www.shoutem.com");
            url.SetScheme(null);

            Assert.AreEqual(null, url.GetScheme());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetSchemeToEmpty()
        {
            var url = new UrlBuilder("www.shoutem.com");
            url.SetScheme(string.Empty);

            Assert.AreEqual(string.Empty, url.GetScheme());
        }

        [TestMethod]
        public void ChangeScheme()
        {
            var url = new UrlBuilder("http://www.shoutem.com");
            url.SetScheme(Uri.UriSchemeHttps);

            Assert.AreEqual(Uri.UriSchemeHttps, url.GetScheme());
        }
    }
}
