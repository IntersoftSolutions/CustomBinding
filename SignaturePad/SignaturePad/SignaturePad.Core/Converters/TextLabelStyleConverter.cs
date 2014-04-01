using System;
using System.Globalization;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Drawing;

namespace SignaturePad
{
    public class TextLabelStyleConverter : IValueConverter
    {
        #region IValueConverter implementation

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                bool isSold = (bool)value;

                if (isSold)
                {
                    StyleAttributes style = new StyleAttributes();
                    style.Strikethrough = true;
                    style.ForegroundColor = Colors.LightGray;
                    return style;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}