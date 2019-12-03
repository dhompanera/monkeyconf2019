using System;
namespace monkeyconf2019
{
    public class MockServices : IData
    {
        public MockServices() : this(new DependencyServiceWrapper())
        {
        }

        public MockServices(IDependencyService dependencyService)
        {
            _dependencyService = dependencyService;
        }

        #region Properties

        readonly IDependencyService _dependencyService;

        #endregion
    }
}
