using System;
using Plugin.Connectivity.Abstractions;

namespace monkeyconf2019
{
    public delegate void OnConnectivityChangeHandler(object sender, ConnectivityChangedEventArgs e);
    public delegate void OnConnectivityTypeChangeHandler(object sender, ConnectivityTypeChangedEventArgs e);

    public interface IConnectivity
    {
        bool IsConnected { get; }
        bool IsConnectedToWifi { get; }
        event OnConnectivityChangeHandler OnConnectivityChanged;
        event OnConnectivityTypeChangeHandler OnConnectivityTypeChanged;
    }
}
