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
        #region Button creation

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

            // Create a data object
            var pushButtonData = gRib.NewPushButtonData(buttonName, className);

            if (panel.AddItem(pushButtonData) is PushButton pushButton)
            {
                // If the button was made, return it
                return pushButton;
            }
            else
            {
                // Report the error if it fails
                Debug.WriteLine($"ERROR: Could not create {buttonName}.");
                return null;
            }
        }

        /// <summary>
        /// Attempts to create a PulldownButton on a RibbonPanel.
        /// </summary>
        /// <param name="panel">The RibbonPanel to add the button to (extended).</param>
        /// <param name="buttonName">The name the user sees.</param>
        /// <param name="className">The full class name the button runs.</param>
        /// <returns>A PulldownButton.</returns>
        public static PulldownButton Ext_AddPulldownButton(this RibbonPanel panel, string buttonName, string className)
        {
            // If no panel, return an error
            if (panel is null)
            {
                Debug.WriteLine($"ERROR: Could not create {buttonName}.");
                return null;
            }

            // Create a data object
            var pulldownButtonData = gRib.NewPulldownButtonData(buttonName, className);

            if (panel.AddItem(pulldownButtonData) is PulldownButton pulldownButton)
            {
                // If the button was made, return it
                return pulldownButton;
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