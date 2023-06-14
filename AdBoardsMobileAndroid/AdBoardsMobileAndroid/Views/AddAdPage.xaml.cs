using AdBoards.ApiClient.Contracts.Requests;
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
	public partial class AddAdPage : ContentPage
	{
        readonly AddAdModel ad = new();
		public AddAdPage ()
		{
			InitializeComponent ();
            
			pickerCategory.SelectedIndex = 0;

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

        async private void BtnCreateAd_Clicked(object sender, EventArgs e)
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

            ad.Name = tbName.Text;
            ad.City = tbCity.Text;
            ad.CategoryId = pickerCategory.SelectedIndex + 1;
            ad.Description = tbDescription.Text;
            ad.Price = price;
            ad.AdTypeId = rbBuy.IsChecked ? 1 : 2;

            Context.AdNow = await Context.Api.AddAd(ad);
            ad.Id = Context.AdNow.Id;
            if (ad.Photo != null)
                Context.AdNow = await Context.Api.UpdateAdPhoto(ad);

            if (Context.AdNow == null)
            {
                await DisplayAlert("Ошибка", "Что то пошло не так! \nОбъявление добавить не удалось...", "ОК");
                return;
            }
            await DisplayAlert("Успешно", "Вы добавили объявление", "ОК");
            await Shell.Current.GoToAsync(nameof(MyAdPage));
        }
    }
}