using System;

namespace monkeyconf2019
{
    public interface IData
    {

    }

    public class ConnectivityException : Exception
    {
        public ConnectivityException()
        {
        }

        public ConnectivityException(string message) : base(message)
        {
        }
    }
}
