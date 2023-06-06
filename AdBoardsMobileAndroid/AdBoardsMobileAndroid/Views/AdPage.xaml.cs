using AdBoardsMobileAndroid.Models;
using AdBoardsMobileAndroid.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace AdBoardsMobileAndroid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdPage : ContentPage
    {
        bool isF = false;
        public AdPage()
        {
            InitializeComponent();
        }
        public AdPage(bool isFavorites)
        {
            InitializeComponent();

            isF = isFavorites;
            btnAddToFavorites.Text = "Удалить из избранного";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            lbName.Text = Context.AdNow.Name;
            lbDescription.Text = Context.AdNow.Description;
            lbCity.Text = Context.AdNow.City;
            lbPhone.Text = Context.AdNow.Person.Phone;
            lbPrice.Text = Context.AdNow.Price.ToString();
            lbSalesman.Text = Context.AdNow.Person.Name;
            imgAd.Source = Context.AdNow.Img;
            imgPerson.Source = Context.AdNow.Person.Img;


        }

        private async void btnAddToFavorites_Clicked(object sender, EventArgs e)
        {
            //if (isF)
            //{
            //    var httpClient = new HttpClient();
            //    using HttpResponseMessage response = await httpClient.DeleteAsync($"http://{IPv4.ip}:5228/Favorites/Delete?AdId={Context.AdNow.Id}&PersonId={Context.UserNow.Id}");
            //    var jsonResponse = await response.Content.ReadAsStringAsync();
            //    if (!Context.AdList.Ads.Any())
            //        Context.AdList.Ads = null;

            //    await Shell.Current.Navigation.PopAsync();
            //}
            //else
            //{
            //    var httpClient = new HttpClient();
            //    var request = new HttpRequestMessage(HttpMethod.Post, $"http://{IPv4.ip}:5228/Favorites/Addition?AdId={Context.AdNow.Id}&PersonId={Context.UserNow.Id}");
            //    var response = await httpClient.SendAsync(request);

            //    if (response.IsSuccessStatusCode)
            //        await DisplayAlert("Успешно", "Объявление добавленно в избранное", "ОК");
            //    else
            //        await DisplayAlert("Ошибка", "Объявление уже в избранном", "ОК");
            //}
        }

        private async void btnComplaint_Clicked(object sender, EventArgs e)
        {
            //var httpClient = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Post, $"http://{IPv4.ip}:5228/Complaint/Addition?AdId={Context.AdNow.Id}&PersonId={Context.UserNow.Id}");
            //var response = await httpClient.SendAsync(request);
            //var responseContent = await response.Content.ReadAsStringAsync();


            //if (response.IsSuccessStatusCode)
            //    await DisplayAlert("Успешно", "Жалоба отправленна", "ОК");
            //else
            //    await DisplayAlert("Ошибка", "Вы уже пожаловались", "ОК");
        }
    }
}