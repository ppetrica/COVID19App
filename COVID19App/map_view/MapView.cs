using core;
using LiveCharts.Maps;
using LiveCharts.WinForms;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Media;

namespace view
{
    public class MapView
    {
        public MapView (IReadOnlyList<CountryInfoEx> info)
        {
            map.Source = MAP_FILE;

            GradientStopCollection collection = new GradientStopCollection();
            collection.Add(new GradientStop(Colors.Green, 0.0));
            collection.Add(new GradientStop(Colors.Yellow, 0.5));
            collection.Add(new GradientStop(Colors.Red, 1.0));

            map.GradientStopCollection = collection;

            map.Dock = DockStyle.Fill;

            CountryInfoEx mostSevere = Utils.MaxElement(info,
                (CountryInfoEx c1, CountryInfoEx c2) => c1.Confirmed > c2.Confirmed);

            double half = mostSevere.Confirmed / 2.0;

            Dictionary<string, double> scaledValues = new Dictionary<string, double>();
            foreach (CountryInfoEx country in info)
            {
                int confirmed = country.Confirmed;

                scaledValues[country.CountryCode] = (confirmed < half) ? (half - confirmed) / half * double.MinValue
                                                                       : (confirmed - half) / half * double.MaxValue;
            }

            map.HeatMap = scaledValues;

            map.LandClick += OnUserClick;
        }

        public void Subscribe(MapObserver observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(MapObserver observer)
        {
            observers.Remove(observer);
        }

        public Control Get()
        {
            return map;
        }

        private void OnUserClick(object obj, MapData data)
        {
            foreach (MapObserver observer in observers)
            {
                observer.OnClick(data.Id);
            }
        }

        private const string MAP_FILE = "World.xml";
        
        private List<MapObserver> observers = new List<MapObserver>();
        private GeoMap map = new GeoMap();
    }
}
