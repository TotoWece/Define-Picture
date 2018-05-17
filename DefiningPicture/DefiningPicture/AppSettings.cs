using System;
using System.Collections.Generic;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace DefiningPicture
{
    public static class AppSettings
    {
        static ISettings Settings => CrossSettings.Current;

        public static string ComputerVisionRoot
        {
            get => Settings.GetValueOrDefault(nameof(ComputerVisionRoot), string.Empty);
            set => Settings.AddOrUpdateValue(nameof(ComputerVisionRoot), value);
        }

        public static string ComputerVisionKey
        {
            get => Settings.GetValueOrDefault(nameof(ComputerVisionKey), string.Empty);
            set => Settings.AddOrUpdateValue(nameof(ComputerVisionKey), value);
        }

        public static bool IsTakePhoto
        {
            get => Settings.GetValueOrDefault(nameof(IsTakePhoto), true);
            set => Settings.AddOrUpdateValue(nameof(IsTakePhoto), value);
        }
    }
}
