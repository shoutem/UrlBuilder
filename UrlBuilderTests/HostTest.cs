namespace UrlBuilderTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Se.Url;

    [TestClass]
    public class HostTest
    {
        [TestMethod]
        public void DefaultHostWithoutUrl()
        {
            var url = new UrlBuilder();

            Assert.AreEqual("localhost", url.GetHost());
        }

        [TestMethod]
        public void DefaultHost()
        {
            var url = new UrlBuilder("http://www.shoutem.com/");

            Assert.AreEqual("www.shoutem.com", url.GetHost());
        }

        [TestMethod]
        public void SetHost()
        {
            var url = new UrlBuilder();
            url.SetHost("www.shoutem.com");

            Assert.AreEqual("www.shoutem.com", url.GetHost());
        }

        [TestMethod]
        public void ChangeHost()
        {
            var url = new UrlBuilder("http://www.shoutem.com/");
            url.SetHost("www.shoutem.local");

            Assert.AreEqual("www.shoutem.local", url.GetHost());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetHostEmpty()
        {
            var url = new UrlBuilder("http://www.shoutem.com/");
            url.SetHost(string.Empty);

            Assert.AreEqual(string.Empty, url.GetHost());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetHostNull()
        {
            var url = new UrlBuilder("http://www.shoutem.com/");
            url.SetHost(null);

            Assert.AreEqual(null, url.GetHost());
        }
    }
}
