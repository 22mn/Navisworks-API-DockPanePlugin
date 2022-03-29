using Autodesk.Navisworks.Api.Plugins;
using System.Windows.Forms.Integration;
using System.Windows.Forms;

namespace WPFDockablePane
{
    // assign name, devId 
    [Plugin("WPFDockablePane", "TwentyTwo", DisplayName ="TwentyTwo {DockablePane Sample}",
        ToolTip="A tutorial project by TwentyTwo.")]
    // set preferred size 
    [DockPanePlugin(350, 500, FixedSize =false)]

    public class MainClass : DockPanePlugin 
    {
        // Called to tell the plugin to create it's pane.
        // Should be overriden in conjunction with DestroyControlPane.
        public override Control CreateControlPane()
        {
            // WindowForm contl to host WPF elem
            ElementHost elemHost = new ElementHost
            {
                // assign wpfcontrol
                AutoSize = true,
                Child = new WPFControl()
            };
            // create control
            elemHost.CreateControl();
            
            return elemHost;
        }
        // Called to tell the plugin to destroy it's pane.
        // Should be overriden in conjunction with CreateControlPane.
        public override void DestroyControlPane(Control pane)
        {
            // releases all resources /dispose
            pane.Dispose();
        }
    }
}
