﻿using AdBoardsMobileAndroid.Models;
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
using AdBoards.ApiClient.Contracts.Responses;
using Microsoft.AspNetCore.Http.Internal;

namespace AdBoardsMobileAndroid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditingProfilePage : ContentPage
    {
        EditPersonModel p = new();
        public EditingProfilePage()
        {
            InitializeComponent();

            var person = Context.UserNow.Person;

            imgProfile.Source = person.PhotoName;
            tbName.Text = person.Name;
            tbCity.Text = person.City;
            tbEmail.Text = person.Email;
            tbPhone.Text = person.Phone;
            dpBirthday.Date = Convert.ToDateTime(person.Birthday);
        }

        async private void BtnSaveChanges_Clicked(object sender, EventArgs e)
        {
            p.Name = tbName.Text;
            p.Birthday = Convert.ToDateTime(dpBirthday.Date);
            p.City = tbCity.Text;
            p.Email = tbEmail.Text;
            p.Phone = tbPhone.Text;

            await Context.Api.PersonUpdate(p);
            var person = await Context.Api.UpdatePersonPhoto(p);

            if (person == null) 
            {
                await DisplayAlert("Ошибка", "Что-то пошло не так", "OK");
                return;
            }

            Context.UserNow.Person = person;
            await DisplayAlert("Успех", "Вы успешно изменили данные профиля", "OK");
        }

        private async void BtnGetPhoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                imgProfile.Source = ImageSource.FromFile(photo.FullPath);
                var stream = new FileStream(photo.FullPath, FileMode.Open);
                p.Photo = new FormFile(stream, 0, stream.Length, "streamFile", Path.GetFileName(photo.FullPath));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }
    }
}