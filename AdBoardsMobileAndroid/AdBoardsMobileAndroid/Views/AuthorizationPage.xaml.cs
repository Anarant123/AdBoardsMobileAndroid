using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AdBoardsMobileAndroid.Models.db;
using AdBoardsMobileAndroid.Models;
using Xamarin.Essentials;
using AdBoards.ApiClient.Extensions;

namespace AdBoardsMobileAndroid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorizationPage : ContentPage
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            string login = Preferences.Get("UserLogin", "null");
            string password = Preferences.Get("UserPassword", "null");
            if (login != "null")
            {
                Context.UserNow = await Context.Api.Authorize(login, password);
                Context.Api.Jwt = Context.UserNow.Token;
            }
        }

        async private void btnSignIn_Clicked(object sender, EventArgs e)
        {
            Context.UserNow = await Context.Api.Authorize(tbLogin.Text, tbPassword.Text);
            Context.Api.Jwt = Context.UserNow.Token;
            
            if (Context.UserNow == null)
            {
                await DisplayAlert("Ошибка", "Вы ввели неверные данные", "ОК");
                return;
            }

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