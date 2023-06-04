using AdBoardsMobileAndroid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AdBoardsMobileAndroid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecoveryPasswordPage : ContentPage
    {
        public RecoveryPasswordPage()
        {
            InitializeComponent();
        }

        private async void btnSend_Clicked(object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"http://{IPv4.ip}:5228/People/RecoveryPassword?Login={tbEmail.Text}");
            var response = await httpClient.SendAsync(request);

            await Navigation.PopAsync();
        }
    }
}