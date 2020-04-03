using System.Windows.Forms;
using System.Collections.Generic;
using view;
using core;


namespace COVID19App
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            _mainTabControl = (TabControl)Controls.Find("mainTabControl", false)[0];

            List<CountryInfoEx> mock = new List<CountryInfoEx>();

            List<DayInfo> list;

            list = new List<DayInfo>();
            list.Add(new DayInfo(new Date(1980, 10, 2), 0, 0, 0));
            list.Add(new DayInfo(new Date(1980, 10, 3), 5, 1, 0));
            list.Add(new DayInfo(new Date(1980, 10, 4), 25, 3, 1));
            mock.Add(new CountryInfoEx(new CountryInfo("", list), "MX"));

            list = new List<DayInfo>();
            list.Add(new DayInfo(new Date(1981, 11, 14), 1, 0, 1));
            list.Add(new DayInfo(new Date(1981, 11, 15), 7, 2, 1));
            list.Add(new DayInfo(new Date(1981, 11, 18), 18, 4, 0));
            mock.Add(new CountryInfoEx(new CountryInfo("", list), "CA"));

            list = new List<DayInfo>();
            list.Add(new DayInfo(new Date(1981, 11, 14), 0, 0, 0));
            list.Add(new DayInfo(new Date(1981, 11, 15), 1, 0, 0));
            list.Add(new DayInfo(new Date(1981, 11, 18), 2, 0, 1));
            mock.Add(new CountryInfoEx(new CountryInfo("", list), "RU"));

            list = new List<DayInfo>();
            list.Add(new DayInfo(new Date(1981, 11, 14), 0, 0, 0));
            list.Add(new DayInfo(new Date(1981, 11, 15), 20, 1, 2));
            list.Add(new DayInfo(new Date(1981, 11, 30), 80, 10, 5));
            mock.Add(new CountryInfoEx(new CountryInfo("", list), "IT"));

            IView view = new MapView(mock);

            TabPage page = new TabPage("World Map");
            page.Controls.Add(view.GetControl());

            _mainTabControl.TabPages.Add(page);

            page = new TabPage("World Stats");
            _mainTabControl.TabPages.Add(page);
        }


        private TabControl _mainTabControl;
    }
}
