using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(YourFavourites.ProfilePage), typeof(YourFavourites.Droid.ProfilePage))]

namespace YourFavourites.Droid
{
    public class ProfilePage : PageRenderer
    {
        Activity activity;
        global::Android.Views.View view;


        public ProfilePage(Context context)
            :base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            try
            {
                SetupUserInterface();
                SetupEventHandlers();
                AddView(view);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"ERROR: ", ex.Message);
            }
        }

        void SetupUserInterface()
        {
            activity = this.Context as Activity;
            view = activity.LayoutInflater.Inflate(Resource.Layout.ProfileLayout, this, true);
            View
        }

        void SetupEventHandlers()
        {
            //takePhotoButton = view.FindViewById<global::Android.Widget.Button>(Resource.Id.takePhotoButton);
            //takePhotoButton.Click += TakePhotoButtonTapped;

            //switchCameraButton = view.FindViewById<global::Android.Widget.Button>(Resource.Id.switchCameraButton);
            //switchCameraButton.Click += SwitchCameraButtonTapped;

            //toggleFlashButton = view.FindViewById<global::Android.Widget.Button>(Resource.Id.toggleFlashButton);
            //toggleFlashButton.Click += ToggleFlashButtonTapped;
        }
    }
}