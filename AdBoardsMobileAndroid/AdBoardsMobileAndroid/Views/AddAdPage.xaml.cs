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
	public partial class AddAdPage : ContentPage
	{
		public AddAdPage ()
		{
			InitializeComponent ();
            
			pickerCategory.SelectedIndex = 0;

        }

        private void btnGetPhoto_Clicked(object sender, EventArgs e)
        {

        }

        async private void btnCreateAd_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}