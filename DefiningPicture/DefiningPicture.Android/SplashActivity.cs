using System;
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace DefiningPicture.Droid
{
    [Activity(Label = "Define Me", 
        Icon = "@drawable/cameraIcon",      
        Theme = "@style/SplashTheme", 
        MainLauncher = true,
        NoHistory = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, 
        ScreenOrientation = ScreenOrientation.Portrait)]

    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
        }
    }
}