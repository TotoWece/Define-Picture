using System;
using Xamarin.Forms;
using Plugin.Permissions.Abstractions;

namespace DefiningPicture
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            GetPermission();
			MainPage = new NavigationPage (new DefineView());
		}

        public async void GetPermission()
        {
            if (!await PermissionHelper.Has(Permission.Camera) || !await PermissionHelper.Has(Permission.Storage))
            {
                await PermissionHelper.Request(Permission.Camera, Permission.Storage);
            }
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
