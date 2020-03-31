using System.Collections.Generic;
using System.Windows.Forms;
using view;
using core;


namespace COVID19App
{
    public partial class MainForm : Form, MapObserver
    {
        public MainForm()
        {
            InitializeComponent();

            List<CountryInfoEx> mock = new List<CountryInfoEx>();

            List<DayInfo> list;
            
            list = new List<DayInfo>();
            list.Add(new DayInfo(new Date(1980, 10, 2), 0, 0, 0));
            mock.Add(new CountryInfoEx(new CountryInfo("", list), "MX"));

            list = new List<DayInfo>();
            list.Add(new DayInfo(new Date(1980, 11, 2), 1, 0, 0));
            mock.Add(new CountryInfoEx(new CountryInfo("", list), "CA"));

            //heatValues["CA"] = 1.0;
            //heatValues["US"] = 0.75;
            //heatValues["IN"] = 0.625;
            //heatValues["IN"] = 0.5;
            //heatValues["CN"] = 0.475;
            //heatValues["JP"] = 0.25;
            //heatValues["BR"] = 0.125;
            //heatValues["DE"] = 0.0;
            //heatValues["FR"] = 0.8;
            //heatValues["GB"] = 0.2;
            //heatValues["RO"] = 0.7;

            MapView view = new MapView(mock);

            view.Subscribe(this);

            Controls.Add(view.Get()); 
        }

        public void OnClick(string countryCode)
        {
            System.Console.WriteLine(countryCode);
        }
    }
}
