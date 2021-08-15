using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Net;
using System.Text.Json;
using UnitTestProject1.HelperClasses;

namespace UnitTestProject1
{
    [TestClass]
    public class PostTests
    {
        [TestMethod]
        public void TestIfPostIsAvailable()
        {
            var client = Util.GetClient();
            var request = new RestRequest(Method.POST);
            Util.AddAuthorization(request);

            var response = client.Execute(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created, "Post method on path /customers isn't available.");
        }

        [TestMethod]
        public void TestIfAuthorizationWorks()
        {
            var client = Util.GetClient();
            var request = new RestRequest(Method.POST);

            var response = client.Execute(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Unauthorized, "Authorization isn't working properly.");
        }

        [TestMethod]
        public void TestContent()
        {
            var client = Util.GetClient();
            var request = new RestRequest(Method.POST);
            Util.AddAuthorization(request);
            var body = GetBodyObject();
            request.AddJsonBody(body);

            var response = client.Execute(request);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created, "Post isn't working properly!");
            try
            {
                var content = JsonSerializer.Deserialize<Person>(response.Content);
            }
            catch (Exception) {
                Assert.Fail("Content isn't of expected type");
            }

        }

        private Person GetBodyObject()
        {
            var client = Util.GetClient();
            var request = new RestRequest(Method.POST);
            Util.AddAuthorization(request);
            var response = client.Execute(request);

            return JsonSerializer.Deserialize<Person>(response.Content);
        }


    }
}
