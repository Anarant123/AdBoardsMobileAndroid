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

        async private void BtnSignIn_Clicked(object sender, EventArgs e)
        {
            var result = string.Empty;
            string ValidateFields()
            {
                if (string.IsNullOrWhiteSpace(tbLogin.Text))
                    result += "Введите логин!\n";

                if (string.IsNullOrWhiteSpace(tbPassword.Text))
                    result += "Введите пароль!\n";

                return result;
            }

            if (!string.IsNullOrEmpty(ValidateFields()))
            {
                await DisplayAlert("Ошибка", ValidateFields(), "ОК");
                return;
            }

            Context.UserNow = await Context.Api.Authorize(tbLogin.Text, tbPassword.Text);
            
            if (Context.UserNow == null)
            {
                await DisplayAlert("Ошибка", "Вы ввели неверные данные", "ОК");
                return;
            }

            Context.Api.Jwt = Context.UserNow.Token;
            Application.Current.MainPage = new AppShell();
        }

        async private void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }

        async private void BtnRecoverPass_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecoveryPasswordPage());
        }
    }
}