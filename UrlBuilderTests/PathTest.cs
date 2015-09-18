namespace UrlBuilderTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Se.Url;

    [TestClass]
    public class PathTest
    {
        [TestMethod]
        public void DefaultPathWithoutUrl()
        {
            var url = new UrlBuilder();

            Assert.AreEqual(string.Empty, url.GetPath());
        }

        [TestMethod]
        public void DefaultPath()
        {
            var url = new UrlBuilder("http://www.shoutem.com/app");

            Assert.AreEqual("app", url.GetPath());
        }

        [TestMethod]
        public void SetPath()
        {
            var url = new UrlBuilder("http://www.shoutem.com/nid");
            url.SetPath("app/nid");

            Assert.AreEqual("app/nid", url.GetPath());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetPathToNull()
        {
            var url = new UrlBuilder("http://www.shoutem.com/");
            url.SetPath(null);

            Assert.AreEqual(null, url.GetPath());
        }

        [TestMethod]
        public void SetPathToEmpty()
        {
            var url = new UrlBuilder("http://www.shoutem.com/nid");
            url.SetPath(string.Empty);

            Assert.AreEqual(string.Empty, url.GetPath());
        }

        [TestMethod]
        public void AddPathSegment()
        {
            var url = new UrlBuilder("http://www.shoutem.com/app");
            url.AddPathSegment("nid");

            Assert.AreEqual("app/nid", url.GetPath());
        }

        [TestMethod]
        public void RemovePathSegment()
        {
            var url = new UrlBuilder("http://www.shoutem.com/app");
            url.RemovePathSegment("app");

            Assert.AreEqual(string.Empty, url.GetPath());
        }
    }
}
