//using Moq;
//using System.Net.Http;
//using System.Collections.Generic;
//using Newtonsoft.Json;

//namespace OnlineDictionary.Tests.Helper
//{
//    internal static class MockHttpMessageHandler<T>
//    {
//        internal static Mock<HttpMessageHandler> SetupBasicGetResourceList(List<T> expectedResponse)
//        {
//            var mockResponse = new Mock<HttpMessageHandler>(System.Net.HttpStatusCode.OK)
//            {
//                Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
//            };

//            mockResponse.Content.Headers.ContentType = 
//        }
//    }
//}
