using System;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace monkeyconf2019
{
    public class NavigationServices : INavigation
    {
        public async Task SetMainPageAsync<TViewModel>(object[] args) where TViewModel : BaseViewModel
        {
            Page page = CreatePage(typeof(TViewModel), args);

            Application.Current.MainPage = new NavigationPage(page);
        }

        public async Task PushAsync<TViewModel>(object[] args) where TViewModel : BaseViewModel
        {
            await PushAsync<TViewModel>(args, string.Empty);
        }

        public async Task PushAsync<TViewModel>(object[] args, string title) where TViewModel : BaseViewModel
        {
            Page page = CreatePage(typeof(TViewModel), args);
            if (!string.IsNullOrEmpty(title))
            {
                page.Title = title;
            }

            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public async Task PushModalAsync<TViewModel>(object[] args) where TViewModel : BaseViewModel
        {
            Page page = CreatePage(typeof(TViewModel), args);

            await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        public async Task PopAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task PopModalAsync()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        public async Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            return await Application.Current.MainPage.DisplayActionSheet(title, cancel, destruction, buttons);
        }

        #region Private

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var pageName = viewModelType.FullName.Replace("ViewModel", "Page");
            var viewModelAssemblyName = viewModelType.Assembly.FullName;
            var pageAssemblyName = string.Format(
                        CultureInfo.InvariantCulture, "{0}, {1}", pageName, viewModelAssemblyName);
            var pageType = Type.GetType(pageAssemblyName);
            return pageType;
        }

        private Page CreatePage(Type viewModelType, object[] args)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType, args) as Page;
            return page;
        }

        #endregion
    }
}
