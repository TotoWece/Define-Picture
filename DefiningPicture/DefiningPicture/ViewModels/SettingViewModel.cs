using System;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using Xamarin.Forms;

namespace DefiningPicture
{
    public class SettingViewModel : INotifyPropertyChanged
    {
        public SettingViewModel()
        { }

        public event PropertyChangedEventHandler PropertyChanged;

        private Command _applyCommand;
        public Command ApplyCommand
        {
            get
            {
                return _applyCommand ??
                    (_applyCommand = new Command(async () => await ExecuteApplyCommandAsync()));
            }
        }

        private string _visionRoot = AppSettings.ComputerVisionRoot;
        public string VisionRoot
        {
            get { return _visionRoot; }
            set
            {
                _visionRoot = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this,
                        new PropertyChangedEventArgs("VisionRoot"));
                }
            }
        }

        private string _visionKey = AppSettings.ComputerVisionKey;
        public string VisionKey
        {
            get { return _visionKey; }
            set
            {
                _visionKey = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this,
                        new PropertyChangedEventArgs("VisionKey"));
                }
            }
        }

        async Task ExecuteApplyCommandAsync()
        {
            AppSettings.ComputerVisionRoot = VisionRoot;
            AppSettings.ComputerVisionKey = VisionKey;
            await App.Current.MainPage.Navigation.PopToRootAsync();
        }
    }
}
