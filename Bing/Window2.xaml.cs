
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

using Microsoft.Maps.MapControl.WPF.Core;
using Microsoft.Maps.MapControl.WPF;
using BingMapsRESTToolkit.Extensions;
using BingMapsRESTToolkit;

namespace Bing
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        string keyMy = "TEXPwe2x8DSzE9NqNfuA~uaoP2VHHxR9xfkreZG7PQQ~Ahj3wia2jCMUhWg9oifBgAdx_UhYFZ1W15tI9OfqBJ3HbCO7CoKxtgydA_IxSKex";

        public Window2()
        {
            InitializeComponent();

            Coordinate reh = new Coordinate(31.876640708011877, 34.81796609362201);

            SimpleWaypoint rev=new SimpleWaypoint(32.83314073512528, 34.985717330983555);
            SimpleWaypoint rev2 = new SimpleWaypoint(31.876640708011877, 34.81796609362201);

            List<SimpleWaypoint> aa = new List<SimpleWaypoint>();
            aa.Add(rev);

            List<SimpleWaypoint> bb = new List<SimpleWaypoint>();
            bb.Add(rev2);


            var req = new DistanceMatrix()
            {
                Origins = aa,
                Destinations = bb,

            };

            Mat(req);

            var routeRequest = new RouteRequest()
            {
                Waypoints = new List<SimpleWaypoint>(){

                   new SimpleWaypoint("Seattle, WA"),
                   new SimpleWaypoint("Bellevue, WA"),
                   new SimpleWaypoint("Redmond, WA"),
                   new SimpleWaypoint("Kirkland, WA")
                 

                   //  new SimpleWaypoint("רחובות"),
                   //new SimpleWaypoint("אשדוד"),
                   //new SimpleWaypoint("תל אביב"),
                   //new SimpleWaypoint("רמת גן")

                },

                WaypointOptimization = TspOptimizationType.TravelTime,
                RouteOptions = new RouteOptions()
                {
                    TravelMode = TravelModeType.Driving
                },
                BingMapsKey = keyMy
            };




            Rut(routeRequest);




            //Create a request.
            var request = new GeocodeRequest()
            {
                Query = "רחובות שני 21",
                //Query = "New York, NY",


                IncludeIso2 = true,
                IncludeNeighborhood = true,
                MaxResults = 25,
                BingMapsKey = "TEXPwe2x8DSzE9NqNfuA~uaoP2VHHxR9xfkreZG7PQQ~Ahj3wia2jCMUhWg9oifBgAdx_UhYFZ1W15tI9OfqBJ3HbCO7CoKxtgydA_IxSKex"
            };



            aser(request);

        }

        private void Mat(DistanceMatrix matrix)
        {

            var res = matrix.GetTimes(0,1);
        }

        private async void Rut(RouteRequest routeRequest)
        {

            var response = await routeRequest.Execute();




            BoundingBox rect = new BoundingBox(new double[] { response.ResourceSets[0].Resources[0].BoundingBox[0], response.ResourceSets[0].Resources[0].BoundingBox[1], response.ResourceSets[0].Resources[0].BoundingBox[2], response.ResourceSets[0].Resources[0].BoundingBox[3] });

            Microsoft.Maps.MapControl.WPF.Location shani = new Microsoft.Maps.MapControl.WPF.Location(31.876640708011877, 34.81796609362201);


            mMap.SetView(shani, 14);

        }


        private async void aser(GeocodeRequest request)
        {
            //Process the request by using the ServiceManager.
            var response = await ServiceManager.GetResponseAsync(request);



            if (response != null &&
                response.ResourceSets != null &&
                response.ResourceSets.Length > 0 &&
                response.ResourceSets[0].Resources != null &&
                response.ResourceSets[0].Resources.Length > 0)
            {
                var result = response.ResourceSets[0].Resources[0] as BingMapsRESTToolkit.Location;

                Microsoft.Maps.MapControl.WPF.Location location = new Microsoft.Maps.MapControl.WPF.Location();

                location.Longitude = result.Point.Coordinates[1];
                location.Latitude = result.Point.Coordinates[0];

                Pushpin pushpin = new Pushpin();
                pushpin.Location = location;

                mMap.Children.Add(pushpin);

                ////mMap.SetView(location, 14);
            }
        }
    }
}
