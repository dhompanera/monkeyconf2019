using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace monkeyconf2019
{
    public class BaseViewModel : BaseNotify
    {
        public BaseViewModel() : this(new DependencyServiceWrapper())
        {
            Page = null;
        }

        public BaseViewModel(IDependencyService dependencyService)
        {
            DependencyService = dependencyService;
            ConnectivityServices = DependencyService.Get<IConnectivity>();
            NavigationService = DependencyService.Get<INavigation>();

            ConnectivityServices.OnConnectivityChanged += async (sender, e) => {
                NotifyPropertyChanged("Online");
                NotifyPropertyChanged("Offline");
                if (Online)
                {
                    try
                    {
                        // Do something when connection recovered
                    }
                    catch { /* fail silently */ }
                }
                else
                {
                    CancelTasks();
                }
            };
        }

        #region Properties

        internal IDependencyService DependencyService;
        internal IConnectivity ConnectivityServices;
        internal INavigation NavigationService;

        public ContentPage Page
        {
            get;
            set;
        }

        public bool Online
        {
            get
            {
                return ConnectivityServices.IsConnected;
            }
        }

        public bool Offline
        {
            get
            {
                return !Online;
            }
        }

        private bool _isBusy = false;

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("IsNotBusy");
            }
        }

        public bool IsNotBusy
        {
            get { return !IsBusy; }
        }

        private bool _landscape;
        public bool Landscape
        {
            get { return _landscape; }

            set
            {
                _landscape = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("Portrait");
            }
        }

        public bool Portrait
        {
            get { return !Landscape; }
        }

        #endregion

        #region Helpers

        CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public CancellationToken CancellationToken
        {
            get
            {
                return _cancellationTokenSource.Token;
            }
        }

        public virtual void CancelTasks()
        {
            if (!_cancellationTokenSource.IsCancellationRequested && CancellationToken.CanBeCanceled)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();
            }
        }

        public async Task RunSafe(Func<Task> execute, bool notifyOnError = false)
        {
            Exception exception = null;

            try
            {
                if (!ConnectivityServices.IsConnected)
                {
                    throw new ConnectivityException();
                }

                if (!CancellationToken.IsCancellationRequested)
                {
                    await execute();
                }
            }
            catch (TaskCanceledException)
            {
                Debug.WriteLine("Task Cancelled");
            }
            catch (ConnectivityException ex)
            {
                //MessagingCenter.Send<BaseViewModel>(this, Constants.NO_CONNECTION_ERROR);
                exception = ex;
                if (notifyOnError)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Debug.WriteLine(@"Exception handled {0}", ex.Message);
            }
            finally
            {
                if (exception != null)
                {
                    // Do something
                }
            }
        }

        #endregion

        #region Functions

        #endregion

        #region Commands

        #endregion

    }
}
