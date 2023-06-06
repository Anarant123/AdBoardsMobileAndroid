using AdBoardsMobileAndroid.Models;
using AdBoardsMobileAndroid.Models.db;
using AdBoardsMobileAndroid.Models.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AdBoardsMobileAndroid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditingProfilePage : ContentPage
    {
        PersonDTO p = new PersonDTO();
        public EditingProfilePage()
        {
            InitializeComponent();

            //imgProfile.Source = Context.UserNow.Img;
            //tbName.Text = Context.UserNow.Name;
            //tbCity.Text = Context.UserNow.City;
            //tbEmail.Text = Context.UserNow.Email;
            //tbPhone.Text = Context.UserNow.Phone;
            //dpBirthday.Date = Convert.ToDateTime(Context.UserNow.Birthday);
        }

        async private void btnSaveChanges_Clicked(object sender, EventArgs e)
        {
            //p.Login = Context.UserNow.Login;
            //p.Name = tbName.Text;
            //p.Birthday = Convert.ToDateTime(dpBirthday.Date);
            //p.City = tbCity.Text;
            //p.Email = tbEmail.Text;
            //p.Phone = tbPhone.Text;

            //var httpClient = new HttpClient();
            //using StringContent jsonContent = new(JsonSerializer.Serialize(p), Encoding.UTF8, "application/json");
            //using HttpResponseMessage response = await httpClient.PutAsync($"http://{IPv4.ip}:5228/People/Update", jsonContent);
            //var jsonResponse = await response.Content.ReadAsStringAsync();

            //if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    Person p = JsonSerializer.Deserialize<Person>(jsonResponse)!;
            //    Context.UserNow = p;
            //    await DisplayAlert("Успешно", "Данные профиля изменились!", "OK");
            //    await Shell.Current.Navigation.PopAsync();
            //}
            //else
            //{
            //    await DisplayAlert("Ошибка", "Что то пошло не так", "OK");
            //}
        }

        private async void btnGetPhoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                imgProfile.Source = ImageSource.FromFile(photo.FullPath);
                p.Photo = File.ReadAllBytes(photo.FullPath);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }
    }
}