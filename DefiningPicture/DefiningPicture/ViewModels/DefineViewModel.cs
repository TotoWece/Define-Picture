using System;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using Xamarin.Forms;

using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;

namespace DefiningPicture
{
    public class DefineViewModel : INotifyPropertyChanged
    {
        public DefineViewModel()
        { }

        public event PropertyChangedEventHandler PropertyChanged;

        private Command _takePictureCommand;
        public Command TakePictureCommand
        {
            get
            {
                return _takePictureCommand ??
                    (_takePictureCommand = new Command(async () => await ExecuteTakePictureCommandAsync()));
            }
        }

        private Command _settingCommand;
        public Command SettingCommand
        {
            get
            {
                return _settingCommand ??
                    (_settingCommand = new Command(async () => await ExecuteSettingCommandAsync()));
            }
        }

        private string _description = "Take a Photo or Pick a Picture.";
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this,
                        new PropertyChangedEventArgs("Description"));
                }
            }
        }

        private bool _isTakePicture = AppSettings.IsTakePhoto;
        public bool IsTakePicture
        {
            get { return _isTakePicture; }
            set
            {
                _isTakePicture = value;
                AppSettings.IsTakePhoto = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this,
                        new PropertyChangedEventArgs("IsTakePicture"));
                }
            }
        }

        private bool _isNotBusy = true;
        public bool IsNotBusy
        {
            get { return _isNotBusy; }
            set
            {
                _isNotBusy = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this,
                        new PropertyChangedEventArgs("IsNotBusy"));
                }
            }
        }

        private string _definedImage;
        public string DefinedImage
        {
            get { return _definedImage; }
            set
            {
                _definedImage = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this,
                        new PropertyChangedEventArgs("DefinedImage"));
                }
            }
        }

        private bool _imageVisible = false;
        public bool ImageVisible
        {
            get { return _imageVisible; }
            set
            {
                _imageVisible = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this,
                        new PropertyChangedEventArgs("ImageVisible"));
                }
            }
        }

        async Task ExecuteTakePictureCommandAsync()
        {
            try
            {
                if (!await PermissionHelper.Has(Permission.Camera) || !await PermissionHelper.Has(Permission.Storage))
                {
                    await PermissionHelper.Request(Permission.Camera, Permission.Storage);
                    Description = "Access To Camera & Storage Denied.";
                    return;
                }

                if (AppSettings.ComputerVisionRoot == string.Empty || AppSettings.ComputerVisionKey == string.Empty)
                {
                    await PermissionHelper.Request(Permission.Camera, Permission.Storage);
                    Description = "Root and Key are Empty.";
                    return;
                }

                IsNotBusy = false;

                // Take a picture.
                await CrossMedia.Current.Initialize();
                MediaFile photo;

                if (IsTakePicture)
                {
                    if (CrossMedia.Current.IsCameraAvailable)
                    {
                        photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                        {
                            Directory = "DefinePicture",
                            Name = "pic.jpg"
                        });
                    }
                    else
                    {
                        IsTakePicture = false;
                        await Application.Current.MainPage.DisplayAlert("Alert", "No Camera Available On Your Device.", "OK");
                        photo = await CrossMedia.Current.PickPhotoAsync();
                    }
                }
                else
                {
                    photo = await CrossMedia.Current.PickPhotoAsync();
                }

                DefinedImage = photo.Path;
                ImageVisible = true;

                // Let Computer Vision help on what you're looking at
                Description = "Defining Picture...";

                var client = new VisionServiceClient(AppSettings.ComputerVisionKey, AppSettings.ComputerVisionRoot);
                AnalysisResult result = await client.DescribeAsync(photo.GetStream());

                // Parse the result
                var caption = StringExtension.ToTitleCase(result.Description.Captions[0].Text);
                var confidence = Math.Floor(result.Description.Captions[0].Confidence * 100);

                Description = $"Define Picture Result : \n{caption}. \nPercentage : {confidence}%";
                IsNotBusy = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR: {ex.Message}");
                Description = "Defining Picture Failed.";
                IsNotBusy = true;
            }
        }

        private static void DeletePhoto(MediaFile file)
        {
            var path = file?.Path;

            if (!string.IsNullOrEmpty(path) && File.Exists(path))
                File.Delete(file?.Path);
        }

        async Task ExecuteSettingCommandAsync()
        {
            await App.Current.MainPage.Navigation.PushAsync(new SettingView());
        }
    }
}
