using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
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
        Android.Views.View view;

        public ProfilePage(Context context)
            :base(context)
        {
            activity = context as Activity;
            view = activity.LayoutInflater.Inflate(Resource.Layout.ProfileLayout, this, false);    
        }
    }
}