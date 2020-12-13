using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Microsoft.Maps.MapControl.WPF;
using BingMapsRESTToolkit;

namespace Bing
{
    /// <summary>
    /// Interaction logic for GetRoutFromGpx.xaml
    /// </summary>
    public partial class GetRoutFromGpx : Window
    {
        DataSet ds = new DataSet();
        DataTable myTbl = new DataTable();


        public GetRoutFromGpx()
        {
            InitializeComponent();
        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            string myFile = @"c:\ProgramData\Adobe\ARM\Reader_15.008.20082\Visual Studio\C#\Bing\Gpx\1_בדצמ׳_2020_22_02_01.gpx";


            ds.ReadXml(myFile);

            myTbl = ds.Tables["trkpt"];

            var locs = new LocationCollection();

            for (int i = 0; i < myTbl.Rows.Count; i=i+10)
            {
                locs.Add(new Microsoft.Maps.MapControl.WPF.Location(Convert.ToDouble(myTbl.Rows[i]["lat"]), Convert.ToDouble(myTbl.Rows[i]["lon"])));
            }

            var routeLine = new MapPolyline()
            {
                Locations = locs,
                Stroke = new SolidColorBrush(Colors.Blue),
                StrokeThickness = 5
            };

            mMap.Children.Add(routeLine);

            Microsoft.Maps.MapControl.WPF.Location start = new Microsoft.Maps.MapControl.WPF.Location(locs[locs.Count - 1].Latitude, locs[locs.Count - 1].Longitude);
            Microsoft.Maps.MapControl.WPF.Location end = new Microsoft.Maps.MapControl.WPF.Location(locs[0].Latitude, locs[0].Longitude);

            Pushpin pushpinStart = new Pushpin();
            pushpinStart.Location = start;

            Pushpin pushpinEnd = new Pushpin();
            pushpinEnd.Location = end;

            //mMap.Children.Add(pushpinStart);
            mMap.Children.Add(pushpinEnd);

            Image image = new Image();

            BitmapImage bitmap= new BitmapImage(new Uri("pack://application:,,,/Bing;component/Resources/pushRed2.png"));

            image.Source = bitmap;
            image.Width = 50;
            image.Height = 50;

            MapLayer layer = new MapLayer();

            PositionOrigin position = PositionOrigin.BottomCenter;

            layer.AddChild(image,start,position);

            mMap.Children.Add(layer);

            mMap.SetView(start, 10);
            mMap.Center = start;
        }
    }
}
