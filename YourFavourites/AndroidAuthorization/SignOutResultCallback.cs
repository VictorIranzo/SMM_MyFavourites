using Android.Gms.Common.Apis;
using Java.Lang;

namespace AndroidAuthorization
{
	public class SignOutResultCallback : Object, IResultCallback
	{
        public void OnResult(Object result)
		{
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}
