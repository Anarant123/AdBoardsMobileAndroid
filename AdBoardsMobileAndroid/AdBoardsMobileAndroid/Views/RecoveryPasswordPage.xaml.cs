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
    public partial class RecoveryPasswordPage : ContentPage
    {
        public RecoveryPasswordPage()
        {
            InitializeComponent();
        }

        private void btnRecoverPass_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnSend_Clicked(object sender, EventArgs e)
        {

        }
    }
}