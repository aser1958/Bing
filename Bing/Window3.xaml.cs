
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
using Bing.Properties;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Core;
using BingMapsRESTToolkit.Extensions;
using BingMapsRESTToolkit;


namespace Bing
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        string myKey = "TEXPwe2x8DSzE9NqNfuA~uaoP2VHHxR9xfkreZG7PQQ~Ahj3wia2jCMUhWg9oifBgAdx_UhYFZ1W15tI9OfqBJ3HbCO7CoKxtgydA_IxSKex";

        public Window3()
        {
            InitializeComponent();


            var routeRequest = new RouteRequest()
            {
                RouteOptions = new RouteOptions()
                {
                    RouteAttributes = new List<RouteAttributeType>()
        {
            RouteAttributeType.RoutePath
        }
                },
                Waypoints = new List<SimpleWaypoint>()
    {
        new SimpleWaypoint(){
            Address = "רחובות שני 21"
        },
        new SimpleWaypoint(){
            Address = "אשדוד"
        }
    },
                BingMapsKey = myKey
            };



            Sale(routeRequest);
        }


        private async void Sale(RouteRequest routeRequest)
        {

            var response = await routeRequest.Execute();

            if (response != null &&
    response.ResourceSets != null &&
    response.ResourceSets.Length > 0 &&
    response.ResourceSets[0].Resources != null &&
    response.ResourceSets[0].Resources.Length > 0)
            {

                var route = response.ResourceSets[0].Resources[0] as Route;
                var coords = route.RoutePath.Line.Coordinates; //This is 2D array of lat/long values.
                var locs = new LocationCollection();

                for (int i = 0; i < coords.Length; i++)
                {
                    locs.Add(new Microsoft.Maps.MapControl.WPF.Location(coords[i][0], coords[i][1]));
                }

                var routeLine = new MapPolyline()
                {
                    Locations = locs,
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue),
                    StrokeThickness = 5
                };

                mMap.Children.Add(routeLine);

            }
        }
    }
}
