// Autodesk
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
// guRoo
using gRib = guRoo.Utilities.Ribbon_Utils;

// This application belongs to the root namespace
namespace guRoo
{
    // Implementing the interface for applications
    public class Application: IExternalApplication
    {
        #region Properties

        // Make a private uiCtlApp
        private static UIControlledApplication _uiCtlApp;

        #endregion

        #region On startup method

        // This will run on startup
        public Result OnStartup(UIControlledApplication uiCtlApp)
        {
            #region Globals registration

            // Store _uiCtlApp, register on idling
            _uiCtlApp = uiCtlApp;

            try
            {
                _uiCtlApp.Idling += RegisterUiApp;
            }
            catch
            {
                Globals.UiApp = null;
                Globals.UsernameRevit = null;
            }
            
            // Registering globals
            Globals.RegisterProperties(uiCtlApp);

            #endregion

            #region Ribbon setup

            // Add ribbon tab
            gRib.AddRibbonTab(uiCtlApp, Globals.AddinName);

            // Create panel
            var panelGeneral = gRib.AddRibbonPanelToTab(uiCtlApp, Globals.AddinName, "General");

            // Add button to panel
            var buttonTest = gRib.AddPushButtonToPanel(panelGeneral, "Testing",
                "guRoo.Cmds_General.Cmd_Test","_testing", Globals.AssemblyPath);

            #endregion

            // Final return
            return Result.Succeeded;
        }

        #endregion

        #region On shutdown method

        // This will run on shutdown
        public Result OnShutdown(UIControlledApplication uiCtlApp)
        {
            // Cleanup code logic
            
            // Final return
            return Result.Succeeded;
        }

        #endregion

        #region Use idling to register UiApp

        /// <summary>
        /// Registers the UiApp and Revit username globally.
        /// </summary>
        /// <param name="sender">Sender of the Idling event.</param>
        /// <param name="e">Idling event arguments.</param>
        private static void RegisterUiApp(object sender, IdlingEventArgs e)
        {
            _uiCtlApp.Idling -= RegisterUiApp;

            if (sender is UIApplication uiApp)
            {
                Globals.UiApp = uiApp;
                Globals.UsernameRevit = uiApp.Application.Username;
            }
        }

        #endregion
    }
}