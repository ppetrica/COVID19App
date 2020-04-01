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
            _countries = info;

            _map.Source = _MapFile;
            _map.Dock = DockStyle.Fill;

            GradientStopCollection collection = new GradientStopCollection();
            collection.Add(new GradientStop(Colors.Green, 0.0));
            collection.Add(new GradientStop(Colors.Yellow, 0.5));
            collection.Add(new GradientStop(Colors.Red, 1.0));

            _map.GradientStopCollection = collection;

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

            _map.HeatMap = scaledValues;

            _map.LandClick += OnUserClick;
        }

        public void Subscribe(MapObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(MapObserver observer)
        {
            _observers.Remove(observer);
        }

        public Control GetControl()
        {
            return _map;
        }

        private void OnUserClick(object obj, MapData data)
        {
            CountryInfoEx? res = Utils.Find(_countries, (CountryInfoEx country) => country.CountryCode == data.Id);

            if (res.HasValue)
            {
                foreach (MapObserver observer in _observers)
                {
                    observer.OnClick(res.Value);
                }
            }
        }

        private const string _MapFile = "World.xml";
        private IReadOnlyList<CountryInfoEx> _countries;
        private List<MapObserver> _observers = new List<MapObserver>();
        private GeoMap _map = new GeoMap();
    }
}
