using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OnlyFiles.SizeConverter
{
    public class FileSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long size = (long)value;
            double kb = size / 1024.0;
            double mb = kb / 1024.0;
            double gb = mb / 1024.0;
            double tb = gb / 1024.0;

            if (tb >= 1)
            {
                return string.Format("{0:F2} TB", tb);
            }
            else if (gb >= 1)
            {
                return string.Format("{0:F2} GB", gb);
            }
            else if (mb >= 1)
            {
                return string.Format("{0:F2} MB", mb);
            }
            else if (kb >= 1)
            {
                return string.Format("{0:F2} KB", kb);
            }
            else
            {
                return string.Format("{0} BT", size);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
