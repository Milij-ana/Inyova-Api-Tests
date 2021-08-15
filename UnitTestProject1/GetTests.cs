using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using UnitTestProject1.HelperClasses;

namespace UnitTestProject1
{
    [TestClass]
    public class GetTests
    {
        [TestMethod]
        public void TestIfGetIsAvailable()
        {
            var client = Util.GetClient();
            var request = new RestRequest(Method.GET);
            Util.AddAuthorization(request);

            var response = client.Execute(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK, "Get method on path /customers isn't available.");
        }

        [TestMethod]
        public void TestIfAuthorizationWorks()
        {
            var client = Util.GetClient();
            var request = new RestRequest(Method.GET);

            var response = client.Execute(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Unauthorized, "Authorization isn't working properly.");
        }

        [TestMethod]
        public void TestContent()
        {
            var client = Util.GetClient();
            var request = new RestRequest(Method.GET);
            Util.AddAuthorization(request);

            var response = client.Execute(request);
            Assert.IsTrue(!string.IsNullOrEmpty(response.Content), "Content is empty!");
            Assert.IsTrue(response.ContentType.Contains("application/json"), "Content isn't in expected format!");
            try
            {
                var content = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(response.Content);
                Assert.IsTrue(content.Count() > 0, "Content is empty!");
            }
            catch (Exception)
            {
                Assert.Fail("Content is not of expected type");
            }
        }

        [TestMethod]
        [DataRow("Inyova")]
        [DataRow("Foo")]
        public void TestMandatoryValues(string name)
        {
            var client = Util.GetClient();
            var request = new RestRequest(Method.GET);
            Util.AddAuthorization(request);

            var response = client.Execute(request);

            var content = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(response.Content);
            var isPresent = false;

            foreach (var person in content)
            {
                if (person.first_name == name)
                {
                    isPresent = true;
                }
            }

            Assert.IsTrue(isPresent, "Mandatory data isn't within content, expected value:" + name);
        }

      

    }
}
