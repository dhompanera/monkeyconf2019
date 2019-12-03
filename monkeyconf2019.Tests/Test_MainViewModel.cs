using System;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace monkeyconf2019.Tests
{
    [TestFixture]
    public class Test_MainViewModel : Tests
    {
        MainViewModel mainViewModel;

        [SetUp]
        public async Task Initialize()
        {
            mainViewModel = new MainViewModel(DependencyService);
        }

        [Test()]
        public async Task Test_Success_Case()
        {
            ConnMock.Setup(m => m.IsConnected).Returns(true);

            SetupHttpMockSend(
                HttpMethod.Get,
                new Uri(string.Format("http://monkeyconf.com/api/v1/endpoint")),
                System.Net.HttpStatusCode.OK,
                new StringContent(""));

            Assert.Pass();
        }
    }
}
