namespace UrlBuilderTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Se.Url;

    [TestClass]
    public class PortTest
    {
        [TestMethod]
        public void DefaultPortWithoutUrl()
        {
            var url = new UrlBuilder();

            Assert.AreEqual(-1, url.GetPort());
        }

        [TestMethod]
        public void DefaultPort()
        {
            var url = new UrlBuilder("www.shoutem.com");

            Assert.AreEqual(80, url.GetPort());
        }

        [TestMethod]
        public void SetPort()
        {
            var url = new UrlBuilder();
            url.SetPort(443);

            Assert.AreEqual(443, url.GetPort());
        }

        [TestMethod]
        public void ChangePort()
        {
            var url = new UrlBuilder("www.shoutem.com");
            url.SetPort(443);

            Assert.AreEqual(443, url.GetPort());
        }

        [TestMethod]
        public void DefaultPortsForSchemes()
        {
            var url1 = new UrlBuilder("http://www.shoutem.com/");
            var url2 = new UrlBuilder("https://www.shoutem.com/");
            var url3 = new UrlBuilder("ftp://www.shoutem.com/");

            Assert.AreEqual(80, url1.GetPort());
            Assert.AreEqual(443, url2.GetPort());
            Assert.AreEqual(21, url3.GetPort());
        }

        [TestMethod]
        public void DefaultPortForUnknownScheme()
        {
            var url = new UrlBuilder("test://www.shoutem.com");

            Assert.AreEqual(-1, url.GetPort());
        }
    }
}
