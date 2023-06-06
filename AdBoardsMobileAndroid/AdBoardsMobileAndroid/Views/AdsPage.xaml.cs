using AdBoardsMobileAndroid.Models;
using AdBoardsMobileAndroid.Models.db;
using AdBoards.ApiClient.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using AdBoards.ApiClient;
using AdBoards.ApiClient.Contracts.Responses;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AdBoardsMobileAndroid.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdsPage : ContentPage
	{
		public AdsPage ()
		{
			InitializeComponent ();
            
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Context.AdList = new AdListViewModel
            {
                Ads = await Context.Api.GetAds()
            };
            cvAds.ItemsSource = Context.AdList.Ads.ToList();
        }

        private async void CvAds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.CurrentSelection!= null)
            {
                Context.AdNow = (Ad)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync(nameof(AdPage));
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            filterContainer.IsVisible = !filterContainer.IsVisible;
        }

        private void BtnDropFilter_Clicked(object sender, EventArgs e)
        {
            tbCity.Text = "";
            tbPriceFrom.Text = "";
            tbPriceTo.Text = "";
            pickerCategory.SelectedIndex = 0;
            OnAppearing();
        }

        private async void BtnUseFilter_Clicked(object sender, EventArgs e)
        {
            bool result;
            string responseContent;

            var httpClient = new HttpClient();
            using HttpResponseMessage response = await httpClient.GetAsync($"http://{IPv4.ip}:5228/Ads/GetAds");
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
                    Context.AdList.Ads = Context.AdList.Ads.Where(x => x.Category.Id == pickerCategory.SelectedIndex).ToList();
                if (Convert.ToBoolean(rbBuy.IsChecked))
                    Context.AdList.Ads = Context.AdList.Ads.Where(x => x.AdType.Id == 1).ToList();
                else if (Convert.ToBoolean(rbSell.IsChecked))
                    Context.AdList.Ads = Context.AdList.Ads.Where(x => x.AdType.Id == 2).ToList();

                cvAds.ItemsSource = Context.AdList.Ads;
            }
            else
            {
                await DisplayAlert("Ошибка", "С данными фильтрами ничего не найдено", "ОК");
            }
        }
    }
}