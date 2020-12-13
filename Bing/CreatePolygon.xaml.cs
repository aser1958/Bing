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
using System.Windows.Shapes;
using Microsoft.Maps.MapControl.WPF;


namespace Bing
{
    /// <summary>
    /// Interaction logic for CreatePolygon.xaml
    /// </summary>
    public partial class CreatePolygon : Window
    {
        // The user defined polygon to add to the map.
        MapPolygon newPolygon = null;
        // The map layer containing the polygon points defined by the user.
        MapLayer polygonPointLayer = new MapLayer();



        public CreatePolygon()
        {
            InitializeComponent();

            //Set focus to map
            MapWithPolygon.Focus();
            SetUpNewPolygon();
            // Adds location points to the polygon for every single mouse click
            MapWithPolygon.MouseDoubleClick += new MouseButtonEventHandler(
            MapWithPolygon_MouseDoubleClick);

            // Adds the layer that contains the polygon points
            NewPolygonLayer.Children.Add(polygonPointLayer);


        }

        private void SetUpNewPolygon()
        {
            newPolygon = new MapPolygon();
            // Defines the polygon fill details
            newPolygon.Locations = new LocationCollection();
            newPolygon.Fill = new SolidColorBrush(Colors.Blue);
            newPolygon.Stroke = new SolidColorBrush(Colors.Green);
            newPolygon.StrokeThickness = 3;
            newPolygon.Opacity = 0.8;
            //Set focus back to the map so that +/- work for zoom in/out
            MapWithPolygon.Focus();
        }


        private void MapWithPolygon_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            // Creates a location for a single polygon point and adds it to
            // the polygon's point location list.
            Point mousePosition = e.GetPosition(this);
            //Convert the mouse coordinates to a location on the map
            Location polygonPointLocation = MapWithPolygon.ViewportPointToLocation(
                mousePosition);
            newPolygon.Locations.Add(polygonPointLocation);

            // A visual representation of a polygon point.
            Rectangle r = new Rectangle();
            r.Fill = new SolidColorBrush(Colors.Red);
            r.Stroke = new SolidColorBrush(Colors.Yellow);
            r.StrokeThickness = 1;
            r.Width = 8;
            r.Height = 8;

            // Adds a small square where the user clicked, to mark the polygon point.
            polygonPointLayer.AddChild(r, polygonPointLocation);
            //Set focus back to the map so that +/- work for zoom in/out
            MapWithPolygon.Focus();

        }

        private void btnCreatePolygon_Click(object sender, RoutedEventArgs e)
        {
            //If there are two or more points, add the polygon layer to the map
            if (newPolygon.Locations.Count >= 2)
            {
                // Removes the polygon points layer.
                polygonPointLayer.Children.Clear();

                // Adds the filled polygon layer to the map.
                NewPolygonLayer.Children.Add(newPolygon);
                SetUpNewPolygon();
            }
        }

    }
}
