using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AdBoardsMobileAndroid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditingProfilePage : ContentPage
    {
        public EditingProfilePage()
        {
            InitializeComponent();
        }

        async private void btnSaveChanges_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private void btnGetPhoto_Clicked(object sender, EventArgs e)
        {

        }
    }
}