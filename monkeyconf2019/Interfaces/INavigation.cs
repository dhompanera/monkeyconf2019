using System;
using System.Threading.Tasks;

namespace monkeyconf2019
{
    public interface INavigation
    {
        Task SetMainPageAsync<TViewModel>(object[] args) where TViewModel : BaseViewModel;
        Task PushAsync<TViewModel>(object[] args) where TViewModel : BaseViewModel;
        Task PushAsync<TViewModel>(object[] args, string title) where TViewModel : BaseViewModel;
        Task PushModalAsync<TViewModel>(object[] args) where TViewModel : BaseViewModel;
        Task PopAsync();
        Task PopModalAsync();
        Task DisplayAlert(string title, string message, string cancel);
        Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
        Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons);
    }
}
