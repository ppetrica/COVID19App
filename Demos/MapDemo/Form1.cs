using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Media;


namespace TestMap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            LiveCharts.WinForms.GeoMap geoMap1 = new LiveCharts.WinForms.GeoMap();

            geoMap1.LandClick += GeoMap1_LandClick;
            // 2. CrCOVID19Appeate a dictionary that we'll fill with Random Data in this example
            Random r = new Random();

            // 3. Note that we define the "key" and number pattern, where the key is the
            // ID of the element in the XML where you want to define the numeric value.
            Dictionary<string, double> values = new Dictionary<string, double>();

            // 4. Fill the specific keys of the countries with a random number
            values["MX"] = double.MinValue;
            values["CA"] = double.MaxValue;
            values["US"] = double.MaxValue / 2;
            values["IN"] = double.MaxValue / 4;
            values["CN"] = double.MaxValue / 8;
            values["JP"] = double.MinValue / 2;
            values["BR"] = double.MinValue / 4;
            values["DE"] = double.MinValue / 8;
            values["FR"] = 321;
            values["GB"] = 1234;
            values["RO"] = 1000;

            //geoMap1.LandStroke = Brushes.Red;
            //geoMap1.DefaultLandFill = Brushes.Black;
            // 5. Assign data and map file
            geoMap1.HeatMap = values;
            geoMap1.Source = @"C:\Users\ppetrica\Desktop\World.xml";


            GradientStopCollection collection = new GradientStopCollection();
            collection.Add(new GradientStop(Colors.Red, 0));
            collection.Add(new GradientStop(Colors.Yellow, 0.5));
            collection.Add(new GradientStop(Colors.Green, 1));
            geoMap1.GradientStopCollection = collection;

            // 6. Important, you can only add the control to the form after defining the source of the map,
            // otherwise it will throw a File Not Found exception
            this.Controls.Add(geoMap1);
            // 7. Set the style of the control to fill it's container, in this example will fill the entire form
            geoMap1.Dock = DockStyle.Fill;
        }

        private void GeoMap1_LandClick(object arg1, LiveCharts.Maps.MapData arg2)
        {
            // Display the ID of the clicked element in the map
            // e.g "FR", "DE"
            Console.WriteLine(arg2.Id);
        }
    }
}
