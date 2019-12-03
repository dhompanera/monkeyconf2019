using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace monkeyconf2019
{
    public class RestServices : IData
    {
        public RestServices() : this(new DependencyServiceWrapper())
        {

        }

        public RestServices(IDependencyService service)
        {
            dependencyService = service;
        }

        #region Properties

        readonly IDependencyService dependencyService;

        HttpClient client;

        #endregion

        #region Helpers

        public async Task<string> RunRequest(Func<Task<HttpResponseMessage>> execute)
        {
            try
            {
                SetConnection();

                var response = await execute();

                if (response == null || !response.IsSuccessStatusCode) return string.Empty;

                var content = await response.Content.ReadAsStringAsync();

                return content;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetConnection()
        {
            if (client == null)
            {
                var messageHandler = dependencyService.Get<HttpMessageHandler>();
                client = messageHandler == null ? new HttpClient() : new HttpClient(messageHandler);
                client.MaxResponseContentBufferSize = 256000;
            }
        }

        #endregion
    }
}
