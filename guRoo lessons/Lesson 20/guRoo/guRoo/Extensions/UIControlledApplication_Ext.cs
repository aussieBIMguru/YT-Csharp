// System
using System.Diagnostics;
// guRoo
using Autodesk.Revit.UI;

// Associate with the extensions namespace
namespace guRoo.Extensions
{
    public static class UIControlledApplication_Ext
    {
        #region RibbonTabs

        /// <summary>
        /// Attempts to add a tab to the application.
        /// </summary>
        /// <param name="uiCtlApp">The UIControlledApplication (extended).</param>
        /// <param name="tabName">The name of the tab to create.</param>
        /// <returns>A Result.</returns>
        public static Result Ext_AddRibbonTab(this UIControlledApplication uiCtlApp, string tabName)
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
        /// <param name="uiCtlApp">The UIControlledApplication (extended).</param>
        /// <param name="tabName">The tab name to add it to.</param>
        /// <param name="panelName">The name to give the panel.</param>
        /// <returns>A RibbonPanel.</returns>
        public static RibbonPanel Ext_AddRibbonPanel(this UIControlledApplication uiCtlApp, string tabName, string panelName)
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
            return uiCtlApp.Ext_GetRibbonPanel(tabName, panelName);
        }

        #endregion

        #region RibbonPanels

        /// <summary>
        /// Attempts to get a RibbonPanel on a tab by name.
        /// </summary>
        /// <param name="uiCtlApp">The UIControlledApplication (extended).</param>
        /// <param name="tabName">The tab name to search from.</param>
        /// <param name="panelName">The panel name to find.</param>
        /// <returns>A RibbonPanel.</returns>
        public static RibbonPanel Ext_GetRibbonPanel(this UIControlledApplication uiCtlApp, string tabName, string panelName)
        {
            // Get all panels on the tab
            var panels = uiCtlApp.GetRibbonPanels(tabName);

            // Try to find the panel with the given name
            foreach (var panel in panels)
            {
                if (panel.Name == panelName)
                {
                    return panel;
                }
            }

            // If not found, return null
            return null;
        }

        #endregion
    }
}