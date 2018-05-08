using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace YourFavourites
{
    public interface BackablePage
    {
        MainPage MainPage { get; set; }
    }
}
