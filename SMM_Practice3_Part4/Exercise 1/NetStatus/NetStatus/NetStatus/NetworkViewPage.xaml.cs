using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NetStatus
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NetworkViewPage : ContentPage
	{
		public NetworkViewPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetConnectionType();

            CrossConnectivity.Current.ConnectivityChanged += HandleConnectivityChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CrossConnectivity.Current.ConnectivityChanged -= HandleConnectivityChanged;
        }

        private void GetConnectionType()
        {
            ConnectionDetails.Text = CrossConnectivity.Current.ConnectionTypes.First().ToString();
        }

        private void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            GetConnectionType();    
        }
    }
}