using System;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System.Linq;

namespace monkeyconf2019
{
    public class ConnectivityServices : IConnectivity
    {
        public ConnectivityServices()
        {
            CrossConnectivity.Current.ConnectivityChanged += ConnectivityChanged;
            CrossConnectivity.Current.ConnectivityTypeChanged += ConnectivityTypeChanged;
        }

        public bool IsConnected
        {
            get
            {
                return CrossConnectivity.Current.IsConnected;
            }
        }

        public bool IsConnectedToWifi
        {
            get
            {
                if (CrossConnectivity.Current.ConnectionTypes.Contains(ConnectionType.WiFi))
                {
                    return true;
                }
                return false;
            }
        }

        public event OnConnectivityChangeHandler OnConnectivityChanged;

        public void ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (OnConnectivityChanged != null)
            {
                OnConnectivityChanged(sender, e);
            }
        }

        public event OnConnectivityTypeChangeHandler OnConnectivityTypeChanged;

        public void ConnectivityTypeChanged(object sender, ConnectivityTypeChangedEventArgs e)
        {
            if (OnConnectivityTypeChanged != null)
            {
                OnConnectivityTypeChanged(sender, e);
            }
        }
    }
}
