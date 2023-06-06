using AdBoardsMobileAndroid.Models;
using AdBoardsMobileAndroid.Models.db;
using AdBoards.ApiClient.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace AdBoardsMobileAndroid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdPage : ContentPage
    {
        bool isF;
        public AdPage()
        {
            InitializeComponent();
        }
        public AdPage(bool isFavorites)
        {
            InitializeComponent();

            isF = isFavorites;
            btnAddToFavorites.Text = "Удалить из избранного";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            lbName.Text = Context.AdNow.Name;
            lbDescription.Text = Context.AdNow.Description;
            lbCity.Text = Context.AdNow.City;
            lbPhone.Text = Context.AdNow.Person.Phone;
            lbPrice.Text = Context.AdNow.Price.ToString();
            lbSalesman.Text = Context.AdNow.Person.Name;
            imgAd.Source = Context.AdNow.PhotoName;
            imgPerson.Source = Context.AdNow.Person.PhotoName;


        }

        private async void BtnAddToFavorites_Clicked(object sender, EventArgs e)
        {
            if (isF)
            {
                var result = await Context.Api.DeleteFromFavorites(Context.AdNow.Id);
                if (!result)
                {
                    await DisplayAlert("Ошибка", "Что то пошло не так...", "ОК");
                    return;
                }

                await DisplayAlert("Успешно", "Объявление удалено из избранного", "ОК");
                btnAddToFavorites.Text = "Добавить в избранное";
                isF = false;
            }
            else
            {
                var result = await Context.Api.AddToFavorites(Context.AdNow.Id);

                if (!result)
                {
                    await DisplayAlert("Ошибка", "Что то пошло не так...", "ОК");
                    return;
                }
                await DisplayAlert("Успешно", "Объявление добавленно в избранное", "ОК");
                btnAddToFavorites.Text = "Удалить из избранного";
                isF = true;
            }
        }

        private async void BtnComplaint_Clicked(object sender, EventArgs e)
        {
            var result = await Context.Api.AddToComplaints(Context.AdNow.Id);


            if (!result)
            {
                await DisplayAlert("Успешно", "Вы уже пожаловались", "ОК");
                return;
            }

            await DisplayAlert("Ошибка", "Жалоба успешно отправленна", "ОК");
        }
    }
}