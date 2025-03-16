// Autodesk
using Autodesk.Revit.UI;

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
            
            // Code logic here

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