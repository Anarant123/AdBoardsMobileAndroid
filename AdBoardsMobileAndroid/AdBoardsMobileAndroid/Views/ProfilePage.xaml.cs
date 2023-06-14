using AdBoards.ApiClient.Extensions;
using AdBoardsMobileAndroid.Models.db;
using System;
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
            var person = Context.UserNow.Person;

            imgProfile.Source = person.PhotoName;
            lblName.Text = person.Name;
            lblCity.Text = person.City;
            lblEmail.Text = person.Email;
            lblPhone.Text = person.Phone;
            lblPhone.Text = person.Birthday.ToString().Substring(0, 10);
        }

        async private void TbiEditProfile_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(EditingProfilePage));
        }

        private void BtnExit_Clicked(object sender, EventArgs e)
        {
            Context.UserNow = null;
            Preferences.Clear();

            Application.Current.MainPage = new NavigationPage(new AuthorizationPage());
        }
    }
}