namespace UrlBuilderTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Se.Url;

    [TestClass]
    public class TrailingSlashTest
    {
        [TestMethod]
        public void UrlBuilderWithTrailingSlash()
        {
            var url = new UrlBuilder("http://www.shoutem.com/nid/");
            Assert.AreEqual("http://www.shoutem.com/nid/", url.ToString());
        }

        [TestMethod]
        public void SetTrailingSlash()
        {
            var url = new UrlBuilder("http://www.shoutem.com/nid");
            url.AddPathSegment("/");

            Assert.AreEqual("http://www.shoutem.com/nid/", url.ToString());
        }

        [TestMethod]
        public void SetPathWithTrailingSlash()
        {
            var url = new UrlBuilder("http://www.shoutem.com/nid");
            url.SetPath("app/nid/");

            Assert.AreEqual("app/nid/", url.GetPath());
        }

        [TestMethod]
        public void AddSegmentWithTrailingSlash()
        {
            var url = new UrlBuilder("http://www.shoutem.com/app");
            url.AddPathSegment("nid/");

            Assert.AreEqual("app/nid/", url.GetPath());
        }

        [TestMethod]
        public void RemoveSegmentWithTrailingSlash()
        {
            var url = new UrlBuilder("http://www.shoutem.com/app/nid/");
            url.RemovePathSegment("nid");

            Assert.AreEqual("app/", url.GetPath());
        }

        [TestMethod]
        public void SetPathToEmpty()
        {
            var url = new UrlBuilder("http://www.shoutem.com/nid/");
            url.SetPath(string.Empty);

            Assert.AreEqual("http://www.shoutem.com/", url.ToString());
        }
    }
}
