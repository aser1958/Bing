﻿using System;
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
using BingMapsRESTToolkit;


namespace Bing
{
    /// <summary>
    /// Interaction logic for GetRout.xaml
    /// </summary>
    public partial class GetRout : Window
    {
        string myKey = "TEXPwe2x8DSzE9NqNfuA~uaoP2VHHxR9xfkreZG7PQQ~Ahj3wia2jCMUhWg9oifBgAdx_UhYFZ1W15tI9OfqBJ3HbCO7CoKxtgydA_IxSKex";

        public GetRout()
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
            Address = "שומרה"
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

                if (mMap.Children.Count > 0)
                {
                    mMap.Children.RemoveAt(0);
                }  
               
                
                mMap.Children.Add(routeLine);

                Microsoft.Maps.MapControl.WPF.Location start = new Microsoft.Maps.MapControl.WPF.Location(locs[0].Latitude,locs[0].Longitude);

                mMap.SetView(start, 12);
                mMap.Center = start;

                Route aa = response.ResourceSets[0].Resources[0] as Route;
                txtKm.Text = aa.TravelDistance.ToString();

                txtTime.Text=App.SecondToHMS(aa.TravelDuration);
            }
        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
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
            Address = txtMoza.Text
        },
        new SimpleWaypoint(){
            Address =txtYaad.Text
        }
    },
                BingMapsKey = myKey
            };



            Sale(routeRequest);
        }
    }
}