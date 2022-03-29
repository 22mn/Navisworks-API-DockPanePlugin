using System.Windows.Controls;
using System.Reflection;

namespace WPFDockablePane
{
    /// <summary>
    /// Interaction logic for WPFControl.xaml
    /// </summary>
    public partial class WPFControl : UserControl
    {
        public WPFControl()
        {
            // initialize
            InitializeComponent();

            // navigated event register 
            // to hide unwanted JS Script pop-ups 
            wb.Navigated += (a, b) => { HideScriptErrors(wb, true); };
        }
        
        // callback method
        public void HideScriptErrors(WebBrowser wb, bool hide)
        {
            // get webbrowser's a private field / _axIWebBrowser2
            // field search included instance & non-public members /private field
            FieldInfo fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", 
                BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;

            // get COM webbrowser obj
            var objComWebBrowser = fiComWebBrowser.GetValue(wb);
            if (objComWebBrowser == null)
            {
                // in case too early
                wb.Loaded += (o, s) => HideScriptErrors(wb, hide); 
                return;
            }
            // invokes the specified member, using the specified binding constraints 
            // and matching the specified argument list
            objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, 
                null, objComWebBrowser, new object[] { hide });
        }
    }
}
