using OnlyFiles.Class;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace OnlyFiles.SizeConverter
{
    public class FileIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is FileItem item)
            {
                string imagePaths = item.IsFolder ? "Images/file_icon.png" : "Images/file_icon.png";

                return new BitmapImage(new Uri(imagePaths, UriKind.Relative));
            }
            else if (value is DirectoryItem item1)
            {
                string imagePath;
                if (item1.IsRoot)
                {
                    imagePath = item1.IsFolder ? "Images/d.png" : "Images/d.png";

                    item1.IsRoot = false;
                    return new BitmapImage(new Uri(imagePath, UriKind.Relative));
                }
                imagePath = item1.IsFolder ? "Images/folder_icon.png" : "Images/folder_icon.png";

                return new BitmapImage(new Uri(imagePath, UriKind.Relative));
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
