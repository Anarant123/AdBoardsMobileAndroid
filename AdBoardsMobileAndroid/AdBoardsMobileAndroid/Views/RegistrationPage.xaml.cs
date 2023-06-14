using AdBoards.ApiClient.Contracts.Requests;
using AdBoards.ApiClient.Extensions;
using AdBoardsMobileAndroid.Models.db;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
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

            string ValidateFields()
            {
                string result = string.Empty;
                if (string.IsNullOrWhiteSpace(tbLogin.Text))
                    result += "Введите логин.\n";

                if (string.IsNullOrWhiteSpace(tbEmail.Text) || !IsValidEmail(tbEmail.Text))
                    result += "Введите корректный email.\n";

                if (string.IsNullOrWhiteSpace(tbPhone.Text) || !IsValidPhone(tbPhone.Text))
                    result += "Введите корректный номер телефона.\n";

                if (string.IsNullOrWhiteSpace(tbPassword1.Text) || string.IsNullOrWhiteSpace(tbPassword2.Text) || tbPassword1.Text.Length < 8)
                    result += "Введите пароль в оба поля.";
                else if (tbPassword1.Text != tbPassword2.Text)
                    result += "Пароли не совпадают.\n";

                return result;
            }

            bool IsValidEmail(string email)
            {
                string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
                Match match = Regex.Match(email, pattern);
                return match.Success;
            }

            bool IsValidPhone(string phone)
            {
                string pattern = @"^(\+)[1-9][0-9\-().]{9,15}$";
                Match match = Regex.Match(phone, pattern);
                return match.Success;
            }

            if (!string.IsNullOrEmpty(ValidateFields()))
            {
                await DisplayAlert("Ошибка", ValidateFields(), "ОК");
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

            var result = (await Context.Api.Registr(person)).ToList();


            if (result.Count == 0)
            {
                await Navigation.PopAsync();
                return;
            }

            var error = string.Join(Environment.NewLine, result.Select(x => x.Message));

            await DisplayAlert("Ошибка", error, "ОК");

        }
    }
}