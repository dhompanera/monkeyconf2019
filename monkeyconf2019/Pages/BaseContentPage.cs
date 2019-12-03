using System;
using Xamarin.Forms;

namespace monkeyconf2019
{
    public class BaseContentPage<T> : ContentPage where T : BaseViewModel, new()
    {
        protected T _viewModel;

        public T ViewModel
        {
            get
            {
                if (_viewModel == null)
                {
                    _viewModel = new T();
                    _viewModel.Page = this;
                }

                return _viewModel;
            }
        }

        double width;
        double height;

        ~BaseContentPage()
        {
            _viewModel = null;
        }

        public BaseContentPage()
        {
            BindingContext = ViewModel;
        }

        #region Functions

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
        }

        #endregion
    }
}
