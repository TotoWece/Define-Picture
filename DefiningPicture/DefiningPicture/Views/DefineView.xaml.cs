using System;
using Xamarin.Forms;

namespace DefiningPicture
{
	public partial class DefineView : ContentPage
	{
		public DefineView ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new DefineViewModel();
        }
	}
}