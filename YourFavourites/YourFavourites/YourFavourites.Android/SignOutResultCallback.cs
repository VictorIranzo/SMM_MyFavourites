using Android.Gms.Common.Apis;
using Java.Lang;

namespace YourFavourites.Droid
{
	public class SignOutResultCallback : Object, IResultCallback
	{
		public MainActivity Activity { get; set; }

		public void OnResult(Object result)
		{
			Activity.UpdateUI(false);
		}
	}
}
