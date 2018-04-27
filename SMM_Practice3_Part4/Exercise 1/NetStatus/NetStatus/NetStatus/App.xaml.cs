using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace NetStatus
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            if (CrossConnectivity.Current.IsConnected)
            {
                MainPage = new NetworkViewPage();
            }
            else
            {
                MainPage = new NoNetworkPage();
            }
        }

		protected override void OnStart ()
		{
            // Handle when your app starts
            base.OnStart();

            CrossConnectivity.Current.ConnectivityChanged += HandleConnectivityChanged;
		}

        private void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            Type currentPage = this.MainPage.GetType();

            if (e.IsConnected && currentPage != typeof(NetworkViewPage))
            {
                this.MainPage = new NetworkViewPage();
            }

            if (!e.IsConnected && currentPage != typeof(NoNetworkPage)) {
                this.MainPage = new NoNetworkPage();
            }
        }

        protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
