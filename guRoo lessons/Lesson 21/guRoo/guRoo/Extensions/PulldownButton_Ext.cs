// System
using System.Diagnostics;
// Autodesk
using Autodesk.Revit.UI;
// guRoo
using gRib = guRoo.Utilities.Ribbon_Utils;

// Associated with the extensions namespace
namespace guRoo.Extensions
{
    public static class PulldownButton_Ext
    {
        #region Button creation

        /// <summary>
        /// Attempts to create a PushButton on a PulldownButton.
        /// </summary>
        /// <param name="pulldownButton">The button to add the button to (extended).</param>
        /// <param name="buttonName">The name the user sees.</param>
        /// <param name="className">The full class name the button runs.</param>
        /// <param name="availability">The availability class name.</param>
        /// <returns>A PushButton.</returns>
        public static PushButton Ext_AddPushButton(this PulldownButton pulldownButton, string buttonName,
            string className, string availability = null)
        {
            // If no panel, return an error
            if (pulldownButton is null)
            {
                Debug.WriteLine($"ERROR: Could not add {buttonName} to pulldown.");
                return null;
            }

            // Create a data object
            var pushButtonData = gRib.NewPushButtonData(buttonName, className);

            if (pulldownButton.AddPushButton(pushButtonData) is PushButton pushButton)
            {
                // Set availability
                if (availability is not null)
                {
                    pushButton.AvailabilityClassName = availability;
                }

                // If the button was made, return it
                return pushButton;
            }
            else
            {
                // Report the error if it fails
                Debug.WriteLine($"ERROR: Could not add {buttonName} to pulldown.");
                return null;
            }
        }

        #endregion
    }
}