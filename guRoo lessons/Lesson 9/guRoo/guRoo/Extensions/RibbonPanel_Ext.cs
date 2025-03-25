// System
using System.Diagnostics;
// Autodesk
using Autodesk.Revit.UI;
// guRoo
using gRib = guRoo.Utilities.Ribbon_Utils;

// Associated with the extensions namespace
namespace guRoo.Extensions
{
    public static class RibbonPanel_Ext
    {
        #region PushButtons

        /// <summary>
        /// Attempts to create a PushButton on a RibbonPanel.
        /// </summary>
        /// <param name="panel">The RibbonPanel to add the button to (extended(.</param>
        /// <param name="buttonName">The name the user sees.</param>
        /// <param name="className">The full class name the button runs.</param>
        /// <returns>A PushButton.</returns>
        public static PushButton Ext_AddPushButton(this RibbonPanel panel, string buttonName, string className)
        {
            // If no panel, return an error
            if (panel is null)
            {
                Debug.WriteLine($"ERROR: Could not create {buttonName}.");
                return null;
            }

            // Get our base name
            var baseName = gRib.CommandToBaseName(className);

            // Create a data object
            var pushButtonData = new PushButtonData(baseName, buttonName, Globals.AssemblyPath, className);

            if (panel.AddItem(pushButtonData) is PushButton pushButton)
            {
                // If the button was made, return it
                pushButton.ToolTip = gRib.LookupTooltip(baseName);

                // We will come back to these later
                pushButton.Image = gRib.GetIcon(baseName, resolution: 16);
                pushButton.LargeImage = gRib.GetIcon(baseName, resolution: 32);

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
    }
}