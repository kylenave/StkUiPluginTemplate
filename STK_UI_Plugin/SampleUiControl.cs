using AGI.Ui.Plugins;
using AGI.STKObjects;
using System;
using System.Windows.Forms;
using stdole;

namespace STK_Sample_UI_Plugin
{
    public partial class SampleUiControl : UserControl, IAgUiPluginEmbeddedControl
    {
        private IAgUiPluginEmbeddedControlSite _pEmbeddedControlSite;
        private MySampleUiPlugin _uiPlugin;
        private AgStkObjectRoot _root;
        public SampleUiControl()
        {
            InitializeComponent();
        }

        public void SetSite(IAgUiPluginEmbeddedControlSite site)
        {
            _pEmbeddedControlSite = site;
            _uiPlugin = _pEmbeddedControlSite?.Plugin as MySampleUiPlugin;
            _root = _uiPlugin?.STKRoot;
        }

        public void OnClosing()
        {
            throw new NotImplementedException();
        }

        public void OnSaveModified()
        {
            throw new NotImplementedException();
        }

        public IPictureDisp GetIcon()
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_root.CurrentScenario == null)
            {
                MessageBox.Show("I know that no scenario is open.");
            }
            else
            {
                MessageBox.Show("I know your scenario's name is " + _root.CurrentScenario.InstanceName);
            }
        }
    }
}
