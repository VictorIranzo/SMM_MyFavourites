using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Auth.Api.SignIn;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AndroidAuthorization
{
    public class AccountManager
    {
        private static AccountManager instance;
        private GoogleSignInAccount googleSignInAccount;

        public static AccountManager GetAccountManager()
        {
            if (instance == null) instance = new AccountManager();

            return instance;
        }

        private AccountManager() { }

        public string GetAccountMail()
        {
            throw new NotImplementedException();
        }

        public string GetAccountName()
        {
            throw new NotImplementedException();
        }

        public string GetImageURL()
        {
            throw new NotImplementedException();
        }

        public void LogOut()
        {
            Process.KillProcess(Process.MyPid());
        }

        public void SetAccount(GoogleSignInAccount googleSignInAccount)
        {
            this.googleSignInAccount = googleSignInAccount;
        }
    }
}