using System;
using System.Collections.Generic;

namespace monkeyconf2019.Tests
{
    public class DependencyServiceStub : IDependencyService
    {
        readonly Dictionary<Type, object> _registeredServices = new Dictionary<Type, object>();

        public void Register<T>(object impl)
        {
            _registeredServices[typeof(T)] = impl;
        }

        public T Get<T>() where T : class
        {
            return (T)_registeredServices[typeof(T)];
        }
    }
}
