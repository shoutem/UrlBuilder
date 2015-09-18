namespace UrlBuilderTests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Se.Url;

    [TestClass]
    public class QueryParamTest
    {
        [TestMethod]
        public void DefaultQueryParam()
        {
            var url = new UrlBuilder();

            Assert.AreEqual("http://localhost/", url.ToString());
        }

        [TestMethod]
        public void SetQueryParam()
        {
            var url = new UrlBuilder("http://www.shoutem.com/app");
            url.SetQueryParam("nid", 123);

            var param = url.GetQueryParam("nid");
            Assert.AreEqual("123", param.ToString());
        }

        [TestMethod]
        public void SetExistingQueryParam()
        {
            var url = new UrlBuilder("http://www.shoutem.com/app?nid=123");
            url.SetQueryParam("nid", 321);

            var param = url.GetQueryParam("nid");
            Assert.AreEqual("321", param.ToString());
        }

        [TestMethod]
        public void SetQueryParamToNull()
        {
            var url = new UrlBuilder("http://www.shoutem.com/app");
            url.SetQueryParam("nid", null);

            var param = url.GetQueryParam("nid");
            Assert.AreEqual(null, param);
        }

        [TestMethod]
        public void SetQueryParamToEmtpy()
        {
            var url = new UrlBuilder("http://www.shoutem.com/app");
            url.SetQueryParam("nid", string.Empty);

            var param = url.GetQueryParam("nid");
            Assert.AreEqual(string.Empty, param.ToString());
        }

        [TestMethod]
        public void AppendQueryParam()
        {
            var url = new UrlBuilder("http://www.shoutem.com/app");
            url.AppendQueryParam("nid", 123);

            var param = url.GetQueryParam("nid");
            Assert.AreEqual("123", param.ToString());
        }

        [TestMethod]
        public void AppendExistingQueryParam()
        {
            var url = new UrlBuilder("http://www.shoutem.com/app");
            url.SetQueryParam("role", "moderator");
            url.AppendQueryParam("role", "admin");

            var paramList = url.GetQueryParam("role") as IList<object>;
            var param1 = paramList[0];
            var param2 = paramList[1];

            Assert.AreEqual("moderator", param1.ToString());
            Assert.AreEqual("admin", param2.ToString());
        }

        [TestMethod]
        public void RemoveQueryParam()
        {
            var url = new UrlBuilder("http://www.shoutem.com/app?nid=123");
            url.RemoveQueryParam("nid");

            Assert.AreEqual(false, url.ContainsQueryParam("nid"));
        }

        [TestMethod]
        public void SetQueryParams()
        {
            var url = new UrlBuilder("http://www.shoutem.com/app");
            var dictParams = new Dictionary<string, object> {{"nid", 123}, {"role", "admin"}};
            url.SetQueryParams(dictParams);

            var param1 = url.GetQueryParam("nid");
            var param2 = url.GetQueryParam("role");

            Assert.AreEqual("123", param1.ToString());
            Assert.AreEqual("admin", param2.ToString());
        }

        [TestMethod]
        public void RemoveQueryParams()
        {
            var url = new UrlBuilder("http://www.shoutem.com/app?nid=123&role=admin");
            var listParams = new List<string> {"nid", "role"};
            url.RemoveQueryParams(listParams);

            Assert.AreEqual(false, url.ContainsQueryParam("nid"));
            Assert.AreEqual(false, url.ContainsQueryParam("role"));
        }

        [TestMethod]
        public void SetMultipleQueryParam()
        {
            var url = new UrlBuilder("http://www.shoutem.com/app?role=moderator&role=admin");

            var paramList = url.GetQueryParam("role") as IList<object>;
            var param1 = paramList[0];
            var param2 = paramList[1];

            Assert.AreEqual("moderator", param1.ToString());
            Assert.AreEqual("admin", param2.ToString());
        }

        [TestMethod]
        public void RemoveMultipleQueryParam()
        {
            var url = new UrlBuilder("http://www.shoutem.com/app?role=moderator&role=admin");
            url.RemoveQueryParam("role");

            Assert.AreEqual(false, url.ContainsQueryParam("role"));
        }

        [TestMethod]
        public void GetQueryParam()
        {
            var url = new UrlBuilder("http://www.shoutem.com/app?nid=123&role=admin");
            var param = url.GetQueryParam("role");

            Assert.AreEqual("admin", param.ToString());
        }

        [TestMethod]
        public void GetQueryParamNames()
        {
            var url = new UrlBuilder("http://www.shoutem.com/app");
            var dictParams = new Dictionary<string, object> { { "nid", 123 }, { "role", "admin" } };
            url.SetQueryParams(dictParams);

            var paramNames = url.GetParamNames();

            CollectionAssert.AreEqual(dictParams.Keys, paramNames);
        }

        [TestMethod]
        public void ContainsQueryParam()
        {
            var url = new UrlBuilder("http://www.shoutem.com/app?nid=123&role=admin");

            Assert.AreEqual(true, url.ContainsQueryParam("role"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ContainsQueryParamNull()
        {
            var url = new UrlBuilder("http://www.shoutem.com/app?nid=123&role=admin");

            Assert.AreEqual(false, url.ContainsQueryParam(null));
        }
    }
}
