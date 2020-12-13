using Microsoft.Maps.MapControl.WPF;
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

namespace Bing
{
    /// <summary>
    /// Interaction logic for AddTile.xaml
    /// </summary>
    public partial class AddTile : Window
    {
        MapTileLayer tileLayer;
        private double tileOpacity = 0.4;



        public AddTile()
        {
            InitializeComponent();
        }

        private void AddTileOverlay()
        {

            // Create a new map layer to add the tile overlay to.
            tileLayer = new MapTileLayer();

            // The source of the overlay.
            TileSource tileSource = new TileSource();
            tileSource.UriFormat = "{UriScheme}://ecn.t0.tiles.virtualearth.net/tiles/r{quadkey}.jpeg?g=129&mkt=en-us&shading=hill&stl=H";

            // Add the tile overlay to the map layer
            tileLayer.TileSource = tileSource;

            // Add the map layer to the map
            if (!MapTileOverlay.Children.Contains(tileLayer))
            {
                MapTileOverlay.Children.Add(tileLayer);
            }
            tileLayer.Opacity = tileOpacity;
        }


        private void btnAddTileLayer_Click(object sender, RoutedEventArgs e)
        {
            // Add the tile overlay on the map, if it doesn't already exist.
            if (tileLayer != null)
            {
                if (!MapTileOverlay.Children.Contains(tileLayer))
                {
                    MapTileOverlay.Children.Add(tileLayer);
                }
            }
            else
            {
                AddTileOverlay();
            }

        }

        private void btnRemoveTileLayer_Click(object sender, RoutedEventArgs e)
        {
            // Removes the tile overlay if it has been added to the map.
            if (MapTileOverlay.Children.Contains(tileLayer))
            {
                MapTileOverlay.Children.Remove(tileLayer);
            }

        }
    }
}
