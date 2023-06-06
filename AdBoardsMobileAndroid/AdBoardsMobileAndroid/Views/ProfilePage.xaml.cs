using AdBoardsMobileAndroid.Models.db;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AdBoardsMobileAndroid.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage ()
        {
            InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //imgProfile.Source = Context.UserNow.Img;

            //lblName.Text = Context.UserNow.Name;
            //lblCity.Text = Context.UserNow.City;
            //lblEmail.Text = Context.UserNow.Email;
            //lblPhone.Text = Context.UserNow.Phone;
        }

        async private void tbiEditProfile_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(EditingProfilePage));
        }

        private void btnExit_Clicked(object sender, EventArgs e)
        {
            Context.UserNow = null;
            Preferences.Clear();

            Application.Current.MainPage = new NavigationPage(new AuthorizationPage());
        }
    }
}