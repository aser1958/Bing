using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bing.Properties;
using Bing;
using Microsoft.Maps.MapControl.WPF.Core;
using Microsoft.Maps.MapControl.WPF;

namespace Bing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MapTileLayer tileLayer = new MapTileLayer();
        private double tileOpacity = 0.50;



        public MainWindow()
        {
            InitializeComponent();

            //mMap.Focus(); // Control with keyBoard
            //mMap.Mode = new AerialMode(true); // AerialMode with Lables
            //myMap.AnimationLevel = AnimationLevel.None;



            Location rambam = new Location(32.83314073512528, 34.985717330983555);
            Location shani = new Location(31.876640708011877, 34.81796609362201);


            Pushpin pushpin = new Pushpin();
            pushpin.Location = rambam;
            pushpin.ToolTip = "Sapir";


            MapLayer layerPush = new MapLayer();
            Image image = new Image();
            BitmapImage bit = new BitmapImage(new Uri(@"d:\Visual Studio\C#\Apps\Bing\Bing\Media\pushRed2.png"));
            image.Source = bit;
            image.Width = 50;
            image.Height = 50;
            PositionOrigin position = PositionOrigin.TopCenter;
            layerPush.AddChild(image,shani,position);
            myMap.Children.Add(layerPush);
            myMap.Children.Add(pushpin);

            myMap.SetView(rambam, 14);

            



        }

        private void But1_Click(object sender, RoutedEventArgs e)
        {
            Location rambam = new Location(32.83314073512528, 34.985717330983555);
            myMap.AnimationLevel = AnimationLevel.None;
            myMap.SetView(rambam, 14);
        }

        private void But2_Click(object sender, RoutedEventArgs e)
        {
            Location shani = new Location(31.876640708011877, 34.81796609362201);
            myMap.AnimationLevel = AnimationLevel.None;
            myMap.SetView(shani, 14);
        }

        private void But3_Click(object sender, RoutedEventArgs e)
        {
            Location shani2 = new Location(47.6424, -122.3219);

            myMap.SetView(shani2, 10);

            MapPolygon polygon = new MapPolygon();
            polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
            polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
            polygon.StrokeThickness = 5;
            polygon.Opacity = 0.25;
            polygon.Locations = new LocationCollection() {
        new Location(47.6424,-122.3219),
        new Location(47.8424,-122.1747),
        new Location(47.5814,-122.1747)};

            myMap.Children.Add(polygon);

        }

        private void But4_Click(object sender, RoutedEventArgs e)
        {
            Location rambam = new Location(32.83314073512528, 34.985717330983555);
            Location shani = new Location(31.876640708011877, 34.81796609362201);

            MapPolyline polygon = new MapPolyline();
            polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
            polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
            polygon.StrokeThickness = 5;
            polygon.Opacity = 1;
            polygon.Locations = new LocationCollection() {shani,rambam};

            myMap.Children.Add(polygon);
            myMap.ZoomLevel = 8;
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            MapLayer imageLayer = new MapLayer();

            Image image = new Image();


            BitmapImage bit = new BitmapImage(new Uri("pack://application:,,,/Bing;component/Resources/shani.JPG"));


            image.Source = bit;

            image.Width = 150;
            image.Height = 150;

            Location shani = new Location(31.876640708011877, 34.81796609362201);

            PositionOrigin positionOrigin = PositionOrigin.Center;

            imageLayer.AddChild(image, shani, positionOrigin);

            myMap.Children.Add(imageLayer);


        }

        private void AddVideo_Click(object sender, RoutedEventArgs e)
        {
            MapLayer layer = new MapLayer();
            MediaElement video = new MediaElement();

            

            video.Source = new Uri(@"d:\מצלמה\רחוב שני\20170416_163950.mp4");
            video.Width = 250;
            video.Height = 200;

            Location shani = new Location(31.876640708011877, 34.81796609362201);

            PositionOrigin positionOrigin = PositionOrigin.Center;

            layer.AddChild(video, shani, positionOrigin);

            myMap.Children.Add(layer);


        }

        private void MyMap_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            Point mousePosition = e.GetPosition(this);
            //Convert the mouse coordinates to a locatoin on the map
            Location pinLocation = myMap.ViewportPointToLocation(mousePosition);

            txtLat.Text= pinLocation.Latitude.ToString();
            txtLong.Text = pinLocation.Longitude.ToString();



        }

        private void AddingTile_Click(object sender, RoutedEventArgs e)
        {
            



        }
    }
}
