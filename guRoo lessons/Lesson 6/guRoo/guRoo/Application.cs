// System
using System.Reflection;
// Autodesk
using Autodesk.Revit.UI;
// guRoo
using gRib = guRoo.Utilities.Ribbon_Utils;

// This application belongs to the root namespace
namespace guRoo
{
    // Implementing the interface for applications
    public class Application: IExternalApplication
    {
        // This will run on startup
        public Result OnStartup(UIControlledApplication uiCtlApp)
        {
            // Collect the controlled application
            var ctlApp = uiCtlApp.ControlledApplication;
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyPath = assembly.Location;

            // Variables
            string tabName = "guRoo";

            // Add ribbon tab
            gRib.AddRibbonTab(uiCtlApp, tabName);

            // Create panel
            var panelGeneral = gRib.AddRibbonPanelToTab(uiCtlApp, tabName, "General");

            // Add button to panel
            var buttonTest = gRib.AddPushButtonToPanel(panelGeneral, "Testing",
                "guRoo.Cmds_General.Cmd_Test","_testing", assemblyPath);

            // Final return
            return Result.Succeeded;
        }

        // This will run on shutdown
        public Result OnShutdown(UIControlledApplication uiCtlApp)
        {
            // Cleanup code logic
            
            // Final return
            return Result.Succeeded;
        }
    }
}