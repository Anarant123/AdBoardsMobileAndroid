using AdBoards.ApiClient.Contracts.Requests;
using AdBoards.ApiClient.Extensions;
using AdBoardsMobileAndroid.Models.db;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AdBoardsMobileAndroid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private async void BtnRegistration_Clicked(object sender, EventArgs e)
        {
            DateTime selectedDate = dpBirthday.Date;
            DateTime currentDate = DateTime.Now;
            DateTime minDate = currentDate.AddYears(-14);
            if (selectedDate > minDate)
            {
                await DisplayAlert("Ошибка", "Выберите дату, не позднее 14 лет назад.", "OK");
                return;
            }

            if (tbPassword1.Text != tbPassword2.Text)
            {
                await DisplayAlert("Ошибка", "Пароли должны совпадать!", "ОК");
                return;
            }

            if (tbLogin.Text == "" || tbPhone.Text == "" || tbEmail.Text == "" || tbPassword1.Text == "")
            {
                await DisplayAlert("Ошибка", "Заполните все поля!", "ОК");
                return;
            }

            PersonReg person = new()
            {
                Login = tbLogin.Text,
                Birthday = dpBirthday.Date,
                Phone = tbPhone.Text,
                Email = tbEmail.Text,
                Password = tbPassword1.Text,
                ConfirmPassword = tbPassword2.Text
            };

            var result = await Context.Api.Registr(person);
            

            if (result)
            {
                await Navigation.PopAsync();
                return;
            }
            await DisplayAlert("Ошибка", "Пользователь с данным логином или Email уже существует", "ОК");

        }
    }
}