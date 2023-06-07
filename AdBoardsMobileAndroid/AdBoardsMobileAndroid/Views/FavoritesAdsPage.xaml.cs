using AdBoardsMobileAndroid.Models.db;
using AdBoardsMobileAndroid.Models;
using AdBoards.ApiClient.Extensions;
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
using AdBoards.ApiClient.Contracts.Responses;

namespace AdBoardsMobileAndroid.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FavoritesAdsPage : ContentPage
	{
		public FavoritesAdsPage ()
		{
			InitializeComponent ();
		}

        private async void CvAds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Context.AdNow = new Ad();
            Context.AdNow = (cvAds.SelectedItem as Ad);
            Context.AdNow.IsFavorite = true;
            await Navigation.PushAsync(new AdPage());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Context.AdList = new AdListViewModel
            {
                Ads = await Context.Api.GetFavoritesAds()
            };
            cvAds.ItemsSource = Context.AdList.Ads.ToList();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            filterContainer.IsVisible = !filterContainer.IsVisible;
        }

        private async void BtnUseFilter_Clicked(object sender, EventArgs e)
        {
            cvAds.ItemsSource = await Context.Api.UseFulter(3, tbPriceFrom.Text, tbPriceTo.Text, tbCity.Text, Convert.ToInt32(pickerCategory.SelectedIndex), (bool)rbBuy.IsChecked!, (bool)rbSell.IsChecked!);

            if ((cvAds.ItemsSource as List<Ad>).Count == 0)
                await DisplayAlert("Сообщение об ошибке", "С данными фильтрами ничего не найденно", "OK");
        }

        private void BtnDropFilter_Clicked(object sender, EventArgs e)
        {
            tbCity.Text = "";
            tbPriceFrom.Text = "";
            tbPriceTo.Text = "";
            pickerCategory.SelectedIndex = 0;
            OnAppearing();
        }
    }
}