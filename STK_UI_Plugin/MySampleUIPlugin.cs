using AGI.Ui.Application;
using AGI.Ui.Core;
using AGI.Ui.Plugins;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AGI.STKObjects;

namespace STK_Sample_UI_Plugin
{

    [Guid("964E2657-1AB7-439B-A346-38EF1E4CECB2")]
    [ProgId("MyCompany.MySampleUiPlugin")]
    [ClassInterface(ClassInterfaceType.None)]
    public class MySampleUiPlugin : IAgUiPlugin, IAgUiPluginCommandTarget
    {
        private IAgUiPluginSite _pluginSite;
        private AgStkObjectRoot _root;

        public AgStkObjectRoot STKRoot => _root;

        public void OnStartup(IAgUiPluginSite pluginSite)
        {
            _pluginSite = pluginSite;
            _root = _pluginSite.Application.Personality2 as AgStkObjectRoot;
        }

        public void OnShutdown()
        {
            _pluginSite = null;
        }

        public void OnDisplayConfigurationPage(IAgUiPluginConfigurationPageBuilder configPageBuilder)
        {
            throw new NotImplementedException();
        }

        public void OnDisplayContextMenu(IAgUiPluginMenuBuilder menuBuilder)
        {
            menuBuilder.AddMenuItem(
                "MyCompany.MySampleUiPlugin.MySecondCommand", 
                "Example Menu Item 1", 
                " Display a message box.", 
                null);
        }

        public void OnInitializeToolbar(IAgUiPluginToolbarBuilder toolbarBuilder)
        {
            toolbarBuilder.AddButton("MyCompany.MySampleUiPlugin.MyFirstCommand", 
                "Example Button 1", 
                "Example UI Plugin Toolbar Button", 
                AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, 
                null);
        }

        public AgEUiPluginCommandState QueryState(string commandName)
        {
            if (string.Compare(commandName, "MyCompany.MySampleUiPlugin.MyFirstCommand", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            }
            else if (string.Compare(commandName, "MyCompany.MySampleUiPlugin.MySecondCommand", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            }
            return AgEUiPluginCommandState.eUiPluginCommandStateNone;
        }

        public void Exec(string commandName, IAgProgressTrackCancel trackCancel, IAgUiPluginCommandParameters parameters)
        {
            if (string.Compare(commandName, "MyCompany.MySampleUiPlugin.MyFirstCommand", StringComparison.OrdinalIgnoreCase) == 0)
            {
                OpenUserInterface();
            }
            else if (string.Compare(commandName, "MyCompany.MySampleUiPlugin.MySecondCommand", StringComparison.OrdinalIgnoreCase) == 0)
            {
                OpenUserInterface();
            }
        }

        public void OpenUserInterface()
        {
            IAgUiPluginWindowSite windows = _pluginSite as IAgUiPluginWindowSite;

            if (windows == null)
            {
                MessageBox.Show("Host application is unable to open windows.");
            }
            else
            {
                IAgUiPluginWindowCreateParameters winParams = windows.CreateParameters();
                winParams.AllowMultiple = false;
                winParams.AssemblyPath = this.GetType().Assembly.Location;
                winParams.UserControlFullName = typeof(SampleUiControl).FullName;
                winParams.Caption = "My First User Interface";
                winParams.DockStyle = AgEDockStyle.eDockStyleDockedBottom;
                winParams.Height = 200;
                object obj = windows.CreateNetToolWindowParam(this, winParams);
            }
        }
    }
}
