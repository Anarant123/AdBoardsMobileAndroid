using AdBoardsMobileAndroid.Models;
using AdBoardsMobileAndroid.Models.db;
using AdBoardsMobileAndroid.Models.DTO;
using AdBoards.ApiClient.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AdBoards.ApiClient.Contracts.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

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
            ad.Name = tbName.Text;
            ad.City = tbCity.Text;
            ad.CategoryId = pickerCategory.SelectedIndex + 1;
            ad.Description = tbDescription.Text;
            ad.Price = Convert.ToInt32(tbPrice.Text);
            ad.AdTypeId = rbBuy.IsChecked ? 1 : 2;

            ad.Id = (await Context.Api.AddAd(ad)).Id;
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