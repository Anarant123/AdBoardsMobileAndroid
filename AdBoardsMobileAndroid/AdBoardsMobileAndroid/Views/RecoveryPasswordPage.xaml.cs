using AdBoards.ApiClient.Extensions;
using AdBoardsMobileAndroid.Models.db;
using System;
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

        private async void BtnSend_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbLogin.Text))
            {
                await DisplayAlert("Ошибка", "Введите логин.", "OK");
                return;
            }

            await Context.Api.Recover(tbLogin.Text);

            await Navigation.PopAsync();
        }
    }
}