// System
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;
// Autodesk
using Autodesk.Revit.UI;

// Associate to the utility namespace
namespace guRoo.Utilities
{
    // These utilites relate to ribbon creation/management
    public static class Ribbon_Utils
    {
        #region Tabs

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

        #endregion

        #region RibbonPanels

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

        #endregion

        #region PushButtons

        /// <summary>
        /// Attempts to create a PushButton on a RibbonPanel.
        /// </summary>
        /// <param name="panel">The RibbonPanel to add the button to.</param>
        /// <param name="buttonName">The name the user sees.</param>
        /// <param name="className">The full class name the button runs.</param>
        /// <param name="internalName">The behind the scenes name.</param>
        /// <param name="assemblyPath">The executing assembly location.</param>
        /// <returns>A PushButton.</returns>
        public static PushButton AddPushButtonToPanel(RibbonPanel panel,string buttonName, string className)
        {
            // If no panel, return an error
            if (panel is null)
            {
                Debug.WriteLine($"ERROR: Could not create {buttonName}.");
                return null;
            }

            // Get our base name
            var baseName = CommandToBaseName(className);

            // Create a data object
            var pushButtonData = new PushButtonData(baseName, buttonName, Globals.AssemblyPath, className);

            if (panel.AddItem(pushButtonData) is PushButton pushButton)
            {
                // If the button was made, return it
                pushButton.ToolTip = LookupTooltip(baseName);

                // We will come back to these later
                pushButton.Image = GetIcon(baseName, resolution: 16);
                pushButton.LargeImage = GetIcon(baseName, resolution: 32);

                return pushButton;
            }
            else
            {
                // Report the error if it fails
                Debug.WriteLine($"ERROR: Could not create {buttonName}.");
                return null;
            }
        }

        #endregion

        #region Resource management

        /// <summary>
        /// Converts a commnand name to the base name standard.
        /// </summary>
        /// <param name="commandName">The command class name.</param>
        /// <returns>A string (the base name).</returns>
        public static string CommandToBaseName(string commandName)
        {
            return commandName.Replace("guRoo.Cmds_", "").Replace(".Cmd", "");
        }

        /// <summary>
        /// Looks up a tooltip by key from the Global tooltips.
        /// </summary>
        /// <param name="key">The key of the tooltip.</param>
        /// <param name="failValue">Default value to return.</param>
        /// <returns></returns>
        public static string LookupTooltip(string key, string failValue = null)
        {
            // Null coalesce the tooltip
            failValue ??= "No tooltip was found.";

            // Try to find the tooltip value
            if (Globals.Tooltips.TryGetValue(key, out string value))
            {
                return value;
            }

            // Return the default value if it failed
            return failValue;
        }

        /// <summary>
        /// Return the icon resource as an ImageSource.
        /// </summary>
        /// <param name="baseName">The base name of the icon resource.</param>
        /// <param name="resolution">The icon resolution to find.</param>
        /// <returns></returns>
        public static ImageSource GetIcon(string baseName, int resolution = 32)
        {
            // Get the path to the resource
            var resourcePath = $"{Globals.AddinName}.Resources.Icons{resolution}.{baseName}{resolution}.png";

            // Stream the resource
            using (var stream = Globals.Assembly.GetManifestResourceStream(resourcePath))
            {
                // Return null if no stream found
                if (stream is null) { return null; }

                // Convert the resource to a bitmap
                var decoder = new PngBitmapDecoder(
                    stream, 
                    BitmapCreateOptions.PreservePixelFormat,
                    BitmapCacheOption.Default);

                // Return the first frame if available
                if (decoder.Frames.Count > 0)
                {
                    return decoder.Frames.First();
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion
    }
}