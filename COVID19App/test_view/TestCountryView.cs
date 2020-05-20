using core;

using view;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Windows.Forms;
using network;
using test_core;

namespace test_view
{
    [TestClass]
    public class TestCountryView
    {
        // This test is used to look at page with statistics at global level.
        [Ignore]
        [TestMethod]
        public void Test()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IDataProvider<CountryInfoEx> provider = new MockDataProviderEx();

            IReadOnlyList<CountryInfoEx> data = provider.GetCountryData();

            Form form = new Form();
            form.Width = 800;
            form.Height = 600;

            IView view = new CountryView(data);

            TabControl tabControl = new TabControl();
            tabControl.Controls.Add(view.GetPage());

            form.Controls.Add(tabControl);
            tabControl.Dock = DockStyle.Fill;

            Application.Run(form);
        }
    }
}
