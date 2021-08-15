using RestSharp;

namespace UnitTestProject1.HelperClasses
{
    public static class Util
    {
        public static RestClient GetClient() => new RestClient("http://019ee.mocklab.io/customers");
        public static void AddAuthorization(RestRequest request) => request.AddHeader("Authorization", "eW92YV90ZXN0OnlvdmFfcGFhc3N3b3JkCg==");
    }
}
