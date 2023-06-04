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

namespace AdBoardsMobileAndroid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorizationPage : ContentPage
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            string login = Preferences.Get("UserLogin", "null");
            string password = Preferences.Get("UserPassword", "null");
            if (login != "null")
                Authorization(login, password);
        }

        async private void btnSignIn_Clicked(object sender, EventArgs e)
        {

            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"http://{IPv4.ip}:5228/People/Authorization?login={tbLogin.Text}&password={tbPassword.Text}");
            var response = await httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Person user = new Person();
                user = JsonSerializer.Deserialize<Person>(responseContent)!;
                Preferences.Set("UserLogin", user.Login);
                Preferences.Set("UserPassword", user.Password);

                Context.UserNow = user;

                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("Ошибка", "Вы ввели неверные данные", "ОК");
            }
        }

        async private void btnSignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }

        async private void btnRecoverPass_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecoveryPasswordPage());
        }

        private async void Authorization(string login, string password)
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"http://{IPv4.ip}:5228/People/Authorization?login={login}&password={password}");
            var response = await httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Person user = new Person();
                user = JsonSerializer.Deserialize<Person>(responseContent)!;

                Context.UserNow = user;

                Application.Current.MainPage = new AppShell();
            }
        }
    }
}