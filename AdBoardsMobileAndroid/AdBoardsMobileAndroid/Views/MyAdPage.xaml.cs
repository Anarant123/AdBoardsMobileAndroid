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
    public partial class MyAdPage : ContentPage
    {
        AdDTO ad = new AdDTO();
        public MyAdPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            imgAd.Source = Context.AdNow.Img;
            tbName.Text = Context.AdNow.Name;
            tbDescription.Text = Context.AdNow.Description;
            if (Context.AdNow.TypeOfAdId == 1)
                rbBuy.IsChecked = true;
            else
                rbSell.IsChecked = true;
            tbPrice.Text = Context.AdNow.Price.ToString();
            pickerCategory.SelectedIndex = (int)Context.AdNow.CotegorysId - 1;
            tbCity.Text = Context.AdNow.City;
        }

        private async void btnSaveChanges_Clicked(object sender, EventArgs e)
        {
            //ad.Id = Context.AdNow.Id;
            //ad.Name = tbName.Text;
            //ad.Description = tbDescription.Text;
            //ad.CotegorysId = pickerCategory.SelectedIndex + 1;
            //if (rbBuy.IsChecked == true)
            //    ad.TypeOfAdId = 1;
            //else if (rbSell.IsChecked == true)
            //    ad.TypeOfAdId = 2;
            //ad.Price = Convert.ToInt32(tbPrice.Text);
            //ad.City = tbCity.Text;
            //if (ad.Photo == null)
            //    ad.Photo = Context.AdNow.Photo;

            //var httpClient = new HttpClient();
            //using StringContent jsonContent = new(JsonSerializer.Serialize(ad), Encoding.UTF8, "application/json");
            //using HttpResponseMessage response = await httpClient.PutAsync($"http://{IPv4.ip}:5228/Ads/Update", jsonContent);
            //var jsonResponse = await response.Content.ReadAsStringAsync();

            //if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    Ad a = JsonSerializer.Deserialize<Ad>(jsonResponse)!;
            //    a.Person = Context.UserNow;
            //    Context.AdNow = a;

            //    await DisplayAlert("Успешно", "Изменения установленны", "ОК");
            //}
            //else
            //{
            //    await DisplayAlert("Ошибка", "Что то пошло не так!", "ОК");
            //}
        }

        private async void btnDropAd_Clicked(object sender, EventArgs e)
        {
            //var httpClient = new HttpClient();
            //using HttpResponseMessage responseD = await httpClient.DeleteAsync($"http://localhost:5228/Ads/Delete?id={Context.AdNow.Id}");
            //using HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:5228/Ads/GetMyAds?id={Context.UserNow.Id}");
            //var responseContent = await response.Content.ReadAsStringAsync();

            //if (response.IsSuccessStatusCode)
            //{
            //    Context.AdList = new AdListViewModel();

            //    Context.AdList.Ads = JsonSerializer.Deserialize<List<Ad>>(responseContent);

            //    await Shell.Current.Navigation.PopAsync();
            //}
            //else
            //{
            //    await DisplayAlert("Ошибка", "Что то пошло не так!", "ОК");
            //}
        }

        private async void btnGetPhoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                imgAd.Source = ImageSource.FromFile(photo.FullPath);
                ad.Photo = File.ReadAllBytes(photo.FullPath);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }
    }
}