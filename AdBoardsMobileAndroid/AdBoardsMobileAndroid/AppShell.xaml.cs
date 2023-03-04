using AdBoardsMobileAndroid.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AdBoardsMobileAndroid
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(EditingProfilePage),typeof(EditingProfilePage));
            Routing.RegisterRoute(nameof(AdPage),typeof(AdPage));
            Routing.RegisterRoute(nameof(MyAdsPage), typeof(MyAdsPage));
        }

    }
}
