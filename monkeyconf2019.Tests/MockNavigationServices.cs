using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace monkeyconf2019.Tests
{
    public class MockNavigationServices : INavigation
    {
        static MockNavigationServices _instance;
        public static MockNavigationServices Instance
        {
            get
            {
                return _instance ?? (_instance = new MockNavigationServices());
            }
            set
            {
                _instance = value;
            }
        }

        public MockNavigationServices()
        {
            NavigationStack = new Stack<string>();
        }

        public Stack<string> NavigationStack { get; private set; }
        public string ModalPage { get; private set; }
        public string PopupPage { get; private set; }
        public string AlertMessage { get; private set; }

        public object[] Args { get; private set; }

        public async Task DisplayAlert(string title, string message, string cancel)
        {
            AlertMessage = message;
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            AlertMessage = message;
            return true;
        }

        public async Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            return buttons[0];
        }

        public async Task PopAsync()
        {
            if (NavigationStack.Count > 0)
            {
                NavigationStack.Pop();
            }
        }

        public async Task PopModalAsync()
        {
            ModalPage = string.Empty;
        }

        public async Task PushAsync<TViewModel>(object[] args) where TViewModel : BaseViewModel
        {
            NavigationStack.Push(GetPageNameFromViewModel(typeof(TViewModel)));
        }

        public async Task PushAsync<TViewModel>(object[] args, string title) where TViewModel : BaseViewModel
        {
            NavigationStack.Push(GetPageNameFromViewModel(typeof(TViewModel)));
        }

        public async Task PushModalAsync<TViewModel>(object[] args) where TViewModel : BaseViewModel
        {
            ModalPage = GetPageNameFromViewModel(typeof(TViewModel));
        }

        public async Task SetMainPageAsync<TViewModel>(object[] args) where TViewModel : BaseViewModel
        {
            NavigationStack.Clear();
            NavigationStack.Push(GetPageNameFromViewModel(typeof(TViewModel)));
        }

        #region Private

        private string GetPageNameFromViewModel(Type viewModelType)
        {
            var pageName = viewModelType.FullName.Replace("ViewModel", "Page");
            return pageName;
        }

        #endregion
    }
}
