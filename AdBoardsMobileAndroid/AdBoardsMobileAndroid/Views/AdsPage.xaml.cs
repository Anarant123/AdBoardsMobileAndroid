using AdBoardsMobileAndroid.Models;
using AdBoardsMobileAndroid.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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

            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://{IPv4.ip}:5228/Ads/GetAds");
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

        private async void cvAds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.CurrentSelection!= null)
            {
                Context.AdNow = (Ad)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync(nameof(AdPage));
            }
        }
    }
}