using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AdBoardsMobileAndroid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorizationPage : ContentPage
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        async private void btnSignIn_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new AppShell());
            //Navigation.RemovePage(this);

            Application.Current.MainPage = new AppShell();
        }

        async private void btnSignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }

        async private void btnRecoverPass_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecoveryPasswordPage());
        }
    }
}