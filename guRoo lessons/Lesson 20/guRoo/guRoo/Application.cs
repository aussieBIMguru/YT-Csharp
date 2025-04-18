// Autodesk
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
// guRoo
using guRoo.Extensions;
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
            Globals.RegisterTooltips($"{Globals.AddinName}.Resources.Files.Tooltips");

            #endregion

            #region Ribbon setup examples

            // Add ribbon tab
            uiCtlApp.Ext_AddRibbonTab(Globals.AddinName);

            // Create panel
            var panelGeneral = uiCtlApp.Ext_AddRibbonPanel(Globals.AddinName, "General");

            // Add button to panel
            var buttonTest = panelGeneral.Ext_AddPushButton("Button", "guRoo.Cmds_Button.Cmd_Button");

            // Add pulldownbutton to panel
            var pulldownTest = panelGeneral.Ext_AddPulldownButton("PullDown", "guRoo.Cmds_PullDown");

            // Add buttons to pulldown
            pulldownTest.Ext_AddPushButton("Button 1", "guRoo.Cmds_PullDown.Cmd_1Button");
            pulldownTest.Ext_AddPushButton("Button 2", "guRoo.Cmds_PullDown.Cmd_2Button");
            pulldownTest.Ext_AddPushButton("Button 3", "guRoo.Cmds_PullDown.Cmd_3Button");

            // Create data objects for the stack
            var stack1Data = gRib.NewPulldownButtonData("Stack 1", "guRoo.Cmds_Stack1");
            var stack2Data = gRib.NewPulldownButtonData("Stack 2", "guRoo.Cmds_Stack2");
            var stack3Data = gRib.NewPulldownButtonData("Stack 3", "guRoo.Cmds_Stack3");

            // Create the stack
            var stack = panelGeneral.AddStackedItems(stack1Data, stack2Data, stack3Data);
            var pulldownStack1 = stack[0] as PulldownButton;
            var pulldownStack2 = stack[1] as PulldownButton;
            var pulldownStack3 = stack[2] as PulldownButton;

            // Add buttons to stacked pulldowns
            pulldownStack1.Ext_AddPushButton("Button", "guRoo.Cmds_Stack1.Cmd_Button");
            pulldownStack2.Ext_AddPushButton("Button", "guRoo.Cmds_Stack2.Cmd_Button");
            pulldownStack3.Ext_AddPushButton("Button", "guRoo.Cmds_Stack3.Cmd_Button");

            #endregion

            #region Command examples

            // Create tools panel
            var panelTools = uiCtlApp.Ext_AddRibbonPanel(Globals.AddinName, "Tools");

            // Add revision pulldown
            var pulldownRevision = panelTools.Ext_AddPulldownButton("Revision", "guRoo.Cmds_Revision");

            // Add pushbuttons to revision
            pulldownRevision.Ext_AddPushButton("BulkRev", "guRoo.Cmds_Revision.Cmd_BulkRev");

            // Add worksets pulldown
            var pulldownWorkset = panelTools.Ext_AddPulldownButton("Workset", "guRoo.Cmds_Workset");

            // Add pushbuttons to workset
            pulldownWorkset.Ext_AddPushButton("Create workset(s)", "guRoo.Cmds_Workset.Cmd_Create");

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