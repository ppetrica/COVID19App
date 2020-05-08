using System.Windows.Forms;
using System.Collections.Generic;
using view;


namespace COVID19App
{
    public partial class MainForm : Form
    {
        public MainForm(List<IView> views)
        {
            InitializeComponent();

            _mainTabControl = (TabControl)Controls.Find("mainTabControl", false)[0];
            _mainTabControl.Dock = DockStyle.Fill;

            foreach (IView view in views)
            {
                _mainTabControl.TabPages.Add(view.GetPage());
            }
        }

        private readonly TabControl _mainTabControl;
    }
}
