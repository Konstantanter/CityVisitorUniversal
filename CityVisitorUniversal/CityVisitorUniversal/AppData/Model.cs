using CityVisitorUniversal.AppData;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace CityVisitorUniversal
{
    public class Image
    {
        public string FileName { get; set; }
        public ImageSource Source => ImageSource.FromFile(FileName);
    }
    public class BackgroundToTextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color backgroundColor = (Color)value;

            if (backgroundColor == Color.Green) return Color.White;

            else return Color.Black;
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class VisitedToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string VisitedPercent)
            {
                if (VisitedPercent.Equals("100 %"))
                    return Color.Green;
            }

            return Color.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class StateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is State state)
            {
                switch (state)
                {
                    case State.Visited:
                        return Color.Green;
                    case State.VisitedTransit:
                        return Color.Yellow;
                    case State.NotVisited:
                        return Color.White;
                    default: return Color.White;
                }
            }

            return Color.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
