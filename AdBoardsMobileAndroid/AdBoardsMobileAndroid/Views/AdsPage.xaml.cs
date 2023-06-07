﻿using AdBoardsMobileAndroid.Models;
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
                Context.AdNow = await Context.Api.GetAd(Context.AdNow.Id);
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
            cvAds.ItemsSource = await Context.Api.UseFulter(1, tbPriceFrom.Text, tbPriceTo.Text, tbCity.Text, Convert.ToInt32(pickerCategory.SelectedIndex), (bool)rbBuy.IsChecked!, (bool)rbSell.IsChecked!);

            if ((cvAds.ItemsSource as List<Ad>).Count == 0)
                await DisplayAlert("Сообщение об ошибке", "С данными фильтрами ничего не найденно", "OK");
        }
    }
}