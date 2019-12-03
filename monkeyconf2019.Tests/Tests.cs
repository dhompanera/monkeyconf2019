using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace monkeyconf2019.Tests
{
    [TestFixture()]
    public class Tests
    {
        protected DependencyServiceStub DependencyService { get; set; }
        protected Mock<IConnectivity> ConnMock { get; set; }
        protected Mock<HttpMessageHandler> HttpMock { get; set; }

        [SetUp]
        public void Setup()
        {
            MockForms.Init();

            DependencyService = new DependencyServiceStub();
            ConnMock = new Mock<IConnectivity>();
            HttpMock = new Mock<HttpMessageHandler>();

            DependencyService.Register<IConnectivity>(ConnMock.Object);
            DependencyService.Register<INavigation>(MockNavigationServices.Instance);
            DependencyService.Register<HttpMessageHandler>(HttpMock.Object);
        }

        protected void SetupHttpMockSend(HttpMethod method, Uri uri, System.Net.HttpStatusCode code, StringContent content)
        {
            HttpMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(msg => msg.Method == method && msg.RequestUri == uri),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = code,
                    Content = content
                });
        }
    }
}