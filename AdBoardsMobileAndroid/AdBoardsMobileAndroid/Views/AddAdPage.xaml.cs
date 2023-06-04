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
	public partial class AddAdPage : ContentPage
	{
        AdDTO ad = new AdDTO();
		public AddAdPage ()
		{
			InitializeComponent ();
            
			pickerCategory.SelectedIndex = 0;

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

        async private void btnCreateAd_Clicked(object sender, EventArgs e)
        {
            ad.Name = tbName.Text;
            ad.City = tbCity.Text;
            ad.Date = DateTime.Now;
            ad.CotegorysId = pickerCategory.SelectedIndex + 1;
            ad.Description = tbDescription.Text;
            ad.Price = Convert.ToInt32(tbPrice.Text);
            if (rbBuy.IsChecked == true)
                ad.TypeOfAdId = 1;
            else
                ad.TypeOfAdId = 2;
            ad.PersonId = Context.UserNow.Id;

            var httpClient = new HttpClient();
            using StringContent jsonContent = new(JsonSerializer.Serialize(ad), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await httpClient.PostAsync($"http://{IPv4.ip}:5228/Ads/Addition", jsonContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Ad a = JsonSerializer.Deserialize<Ad>(jsonResponse)!;
                Context.AdNow = a;

                await DisplayAlert("Успешно", "Вы добавили объявление", "ОК");
                await Shell.Current.GoToAsync(nameof(MyAdPage));
            }
            else
            {
                await DisplayAlert("Ошибка", "Что то пошло не так! \nОбъявление добавить не удалось...", "ОК");
            }
        }
    }
}