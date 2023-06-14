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
            int price;
            string ValidateFields()
            {
                var result = string.Empty;
                if (!int.TryParse(tbPrice.Text, out price) || price < 0)
                    result += "Некорректная цена\n";

                if (string.IsNullOrWhiteSpace(tbName.Text))
                    result += "Название объявления является обязательным полем.\n";

                if (string.IsNullOrWhiteSpace(tbCity.Text))
                    result += "Город является обязательным полем.\n";

                return result;
            }

            if (!string.IsNullOrEmpty(ValidateFields()))
            {
                await DisplayAlert("Ошибка", ValidateFields(), "OK");
                return;
            }

            ad.Id = Context.AdNow.Id;
            ad.Name = tbName.Text;
            ad.Description = tbDescription.Text;
            ad.CategoryId = pickerCategory.SelectedIndex + 1;
            if (rbBuy.IsChecked == true)
                ad.AdTypeId = 1;
            else if (rbSell.IsChecked == true)
                ad.AdTypeId = 2;
            ad.Price = price;
            ad.City = tbCity.Text;

            Context.AdNow = await Context.Api.AdUpdate(ad);
            ad.Id = Context.AdNow.Id;
            if (ad.Photo != null)
                Context.AdNow = await Context.Api.UpdateAdPhoto(ad);

            if (Context.AdNow == null)
            {
                await DisplayAlert("Ошибка", "Что то пошло не так!", "ОК");
                return;
            }

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