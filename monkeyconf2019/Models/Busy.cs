using System;
namespace monkeyconf2019
{
    public class Busy : IDisposable
    {
        readonly object _sync = new Object();
        readonly BaseViewModel _viewModel;

        public Busy(BaseViewModel viewModel)
        {
            _viewModel = viewModel;
            lock (_sync)
            {
                _viewModel.IsBusy = true;
            }
        }

        public void Dispose()
        {
            lock (_sync)
            {
                _viewModel.IsBusy = false;
            }
        }
    }
}
