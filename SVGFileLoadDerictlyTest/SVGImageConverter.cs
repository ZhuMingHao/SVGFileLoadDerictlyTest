using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace SVGFileLoadDerictlyTest
{
    public class SVGImageConverter : IValueConverter
    {
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var svg = new SvgImageSource();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = client.GetAsync(new Uri(value as string)).Result;
                    if (response != null && response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var stream = response.Content.ReadAsStreamAsync().Result)
                        {
                            using (var memStream = new MemoryStream())
                            {
                                stream.CopyToAsync(memStream);
                                memStream.Position = 0;
                                svg.SetSourceAsync(memStream.AsRandomAccessStream());

                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return svg;
        }
    }
}
