// System
using System.Diagnostics;
// Autodesk
using Autodesk.Revit.UI;

// Associate to the utility namespace
namespace guRoo.Utilities
{
    // These utilites relate to ribbon creation/management
    public static class Ribbon_Utils
    {
        /// <summary>
        /// Attempts to add a tab to the application.
        /// </summary>
        /// <param name="uiCtlApp">The UIControlledApplication.</param>
        /// <param name="tabName">The name of the tab to create.</param>
        /// <returns>A Result.</returns>
        public static Result AddRibbonTab(UIControlledApplication uiCtlApp, string tabName)
        {
            try
            {
                // Try to add a tab
                uiCtlApp.CreateRibbonTab(tabName);
                return Result.Succeeded;
            }
            catch
            {
                // Report the error if it fails
                Debug.WriteLine($"ERROR: Could not create tab {tabName}");
                return Result.Failed;
            }
        }

        /// <summary>
        /// Attempts to create a RibbonPanel on a tab by name.
        /// </summary>
        /// <param name="uiCtlApp">The UIControlledApplication.</param>
        /// <param name="tabName">The tab name to add it to.</param>
        /// <param name="panelName">The name to give the panel.</param>
        /// <returns>A RibbonPanel.</returns>
        public static RibbonPanel AddRibbonPanelToTab(UIControlledApplication uiCtlApp, string tabName, string panelName)
        {
            try
            {
                // Try to add a ribbon panel
                uiCtlApp.CreateRibbonPanel(tabName, panelName);
            }
            catch
            {
                // Report the error if it fails
                Debug.WriteLine($"ERROR: Could not add {panelName} to {tabName}");
                return null;
            }

            // Try to get and return the ribbon panel
            return GetRibbonPanelByName(uiCtlApp, tabName, panelName);
        }

        /// <summary>
        /// Attempts to get a RibbonPanel on a tab by name.
        /// </summary>
        /// <param name="uiCtlApp">The UIControlledApplication</param>
        /// <param name="tabName">The tab name to search from.</param>
        /// <param name="panelName">The panel name to find.</param>
        /// <returns>A RibbonPanel.</returns>
        public static RibbonPanel GetRibbonPanelByName(UIControlledApplication uiCtlApp, string tabName, string panelName)
        {
            // Get all panels on the tab
            var panels = uiCtlApp.GetRibbonPanels(tabName);

            // Try to find the panel with the given name
            foreach ( var panel in panels)
            {
                if (panel.Name == panelName)
                {
                    return panel;
                }
            }

            // If not found, return null
            return null;
        }

        /// <summary>
        /// Attempts to create a PushButton on a RibbonPanel.
        /// </summary>
        /// <param name="panel">The RibbonPanel to add the button to.</param>
        /// <param name="buttonName">The name the user sees.</param>
        /// <param name="className">The full class name the button runs.</param>
        /// <param name="internalName">The behind the scenes name.</param>
        /// <param name="assemblyPath">The executing assembly location.</param>
        /// <returns>A PushButton.</returns>
        public static PushButton AddPushButtonToPanel(RibbonPanel panel,string buttonName, string className,
            string internalName, string assemblyPath)
        {
            // If no panel, return an error
            if (panel is null)
            {
                Debug.WriteLine($"ERROR: Could not create {buttonName}.");
                return null;
            }

            // Create a data object
            var pushButtonData = new PushButtonData(internalName, buttonName, assemblyPath, className);

            if (panel.AddItem(pushButtonData) is PushButton pushButton)
            {
                // If the button was made, return it
                pushButton.ToolTip = "Testing tooltip.";

                // We will come back to these later
                // pushButton.Image = null;
                // pushButton.LargeImage = null;

                return pushButton;
            }
            else
            {
                // Report the error if it fails
                Debug.WriteLine($"ERROR: Could not create {buttonName}.");
                return null;
            }
        }
    }
}
