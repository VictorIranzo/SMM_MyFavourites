using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common.Apis;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AndroidAuthorization
{
    public class AccountManager
    {
        private static GoogleSignInAccount googleSignInAccount;
        private static GoogleApiClient mGoogleApiClient;

        private AccountManager() {}

        // TODO: Revisar si estos métodos deberían dejar de ser estáticos para implementar correctamente el patrón Singleton.
        // Si son de instancia, pueden acceder igualmente al atributo estático.

        public static string GetAccountMail()
        {
            throw new NotImplementedException();
        }

        public static string GetAccountName()
        {
            return googleSignInAccount?.DisplayName;
        }

        public static string GetImageURL()
        {
            return googleSignInAccount?.PhotoUrl?.ToString();
        }

        public static void LogOut()
        {
            Auth.GoogleSignInApi.SignOut(mGoogleApiClient).SetResultCallback(new SignOutResultCallback());
        }

        public static void SetAccount(GoogleSignInAccount newCount)
        {
            googleSignInAccount = newCount;
        }

        public static void SetGoogleApi(GoogleApiClient newApi)
        {
            mGoogleApiClient = newApi;
        }
    }
}