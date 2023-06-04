using AdBoardsMobileAndroid.Models;
using AdBoardsMobileAndroid.Models.db;
using AdBoardsMobileAndroid.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
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

        private async void btnRegistration_Clicked(object sender, EventArgs e)
        {
            DateTime selectedDate = dpBirthday.Date;
            DateTime currentDate = DateTime.Now;
            DateTime minDate = currentDate.AddYears(-14);
            if (selectedDate > minDate)
            {
                await DisplayAlert("Ошибка", "Выберите дату, не позднее 14 лет назад.", "OK");
                return;
            }

            PersonDTO person = new PersonDTO();

            if (tbPassword1.Text == tbPassword2.Text)
            {
                person.RightId = 1;
                person.Login = tbLogin.Text;
                person.Birthday = dpBirthday.Date;
                person.Phone = tbPhone.Text;
                person.Email = tbEmail.Text;
                person.Password = tbPassword1.Text;

                var httpClient = new HttpClient();
                using StringContent jsonContent = new(JsonSerializer.Serialize(person), Encoding.UTF8, "application/json");
                using HttpResponseMessage response = await httpClient.PostAsync($"http://{IPv4.ip}:5228/People/Registration", jsonContent);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                Person p = new Person();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Ошибка", "Пользователь с данным логином или Email уже существует", "ОК");
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Пароли должны совпадать!", "ОК");
            }
            await Navigation.PopAsync();
        }
    }
}