using AdBoardsMobileAndroid.Models.db;
using AdBoardsMobileAndroid.Models;
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

namespace AdBoardsMobileAndroid.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FavoritesAdsPage : ContentPage
	{
		public FavoritesAdsPage ()
		{
			InitializeComponent ();
		}

        private async void cvAds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (e.CurrentSelection != null)
            {
                Context.AdNow = (Ad)e.CurrentSelection.FirstOrDefault();
                await Navigation.PushAsync(new AdPage(true));
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://{IPv4.ip}:5228/Ads/GetFavoritesAds?id={Context.UserNow.Id}");
            var response = await httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Context.AdList = new AdListViewModel();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Используйте это, если нужно преобразование в camelCase
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                Context.AdList.Ads = JsonSerializer.Deserialize<List<Ad>>(responseContent, options);

                cvAds.ItemsSource = Context.AdList.Ads.ToList();
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            filterContainer.IsVisible = !filterContainer.IsVisible;
        }

        private async void btnUseFilter_Clicked(object sender, EventArgs e)
        {
            bool result;
            string responseContent;

            var httpClient = new HttpClient();
            using HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:5228/Ads/GetFavoritesAds?id={Context.UserNow.Id}");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            responseContent = await response.Content.ReadAsStringAsync();
            result = response.IsSuccessStatusCode;

            if (result)
            {
                Context.AdList = new AdListViewModel();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                Context.AdList.Ads = JsonSerializer.Deserialize<List<Ad>>(responseContent, options);

                if (!string.IsNullOrEmpty(tbPriceFrom.Text))
                    Context.AdList.Ads = Context.AdList.Ads.Where(x => x.Price >= Convert.ToInt32(tbPriceFrom.Text)).ToList();
                if (!string.IsNullOrEmpty(tbPriceTo.Text))
                    Context.AdList.Ads = Context.AdList.Ads.Where(x => x.Price <= Convert.ToInt32(tbPriceTo.Text)).ToList();
                if (!string.IsNullOrEmpty(tbCity.Text))
                    Context.AdList.Ads = Context.AdList.Ads.Where(x => x.City == tbCity.Text).ToList();
                if (pickerCategory.SelectedIndex != 0)
                    Context.AdList.Ads = Context.AdList.Ads.Where(x => x.CotegorysId == pickerCategory.SelectedIndex).ToList();
                if (Convert.ToBoolean(rbBuy.IsChecked))
                    Context.AdList.Ads = Context.AdList.Ads.Where(x => x.TypeOfAdId == 1).ToList();
                else if (Convert.ToBoolean(rbSell.IsChecked))
                    Context.AdList.Ads = Context.AdList.Ads.Where(x => x.TypeOfAdId == 2).ToList();

                cvAds.ItemsSource = Context.AdList.Ads;
            }
            else
            {
                await DisplayAlert("Ошибка", "С данными фильтрами ничего не найдено", "ОК");
            }
        }

        private void btnDropFilter_Clicked(object sender, EventArgs e)
        {
            tbCity.Text = "";
            tbPriceFrom.Text = "";
            tbPriceTo.Text = "";
            pickerCategory.SelectedIndex = 0;
            OnAppearing();
        }
    }
}