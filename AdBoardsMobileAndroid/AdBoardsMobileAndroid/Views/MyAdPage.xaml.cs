using AdBoards.ApiClient.Contracts.Requests;
using AdBoards.ApiClient.Contracts.Responses;
using AdBoards.ApiClient.Extensions;
using AdBoardsMobileAndroid.Models.db;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AdBoardsMobileAndroid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAdPage : ContentPage
    {
        AddAdModel ad = new();
        public MyAdPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            imgAd.Source = Context.AdNow.PhotoName;
            tbName.Text = Context.AdNow.Name;
            tbDescription.Text = Context.AdNow.Description;
            if (Context.AdNow.AdType.Id == 1)
                rbBuy.IsChecked = true;
            else
                rbSell.IsChecked = true;
            tbPrice.Text = Context.AdNow.Price.ToString();
            pickerCategory.SelectedIndex = (int)Context.AdNow.Category.Id - 1;
            tbCity.Text = Context.AdNow.City;
        }

        private async void BtnSaveChanges_Clicked(object sender, EventArgs e)
        {
            ad.Id = Context.AdNow.Id;
            ad.Name = tbName.Text;
            ad.Description = tbDescription.Text;
            ad.CategoryId = pickerCategory.SelectedIndex + 1;
            if (rbBuy.IsChecked == true)
                ad.AdTypeId = 1;
            else if (rbSell.IsChecked == true)
                ad.AdTypeId = 2;
            ad.Price = Convert.ToInt32(tbPrice.Text);
            ad.City = tbCity.Text;

            Ad a = await Context.Api.AdUpdate(ad);
            if (ad.Photo != null)
                a = await Context.Api.UpdateAdPhoto(ad);

            if (a == null)
            {
                await DisplayAlert("Ошибка", "Что то пошло не так!", "ОК");
                return;
            }

            Context.AdNow = a;
            await DisplayAlert("Успешно", "Вы успешно изменили объявление!", "OK");
        }

        private async void BtnDropAd_Clicked(object sender, EventArgs e)
        {
            var result = await Context.Api.DeleteAd(Context.AdNow.Id);

            if (result)
            {
                await Shell.Current.Navigation.PopAsync();
                return;
            }

            await DisplayAlert("Ошибка", "Что то пошло не так!", "ОК");
        }

        private async void BtnGetPhoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                imgAd.Source = ImageSource.FromFile(photo.FullPath);
                var stream = new FileStream(photo.FullPath, FileMode.Open);
                ad.Photo = new FormFile(stream, 0, stream.Length, "streamFile", Path.GetFileName(photo.FullPath));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }
    }
}