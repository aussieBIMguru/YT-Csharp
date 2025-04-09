// Autodesk
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
// guRoo
using guRoo.Extensions;
using gFrm = guRoo.Forms;

// Associate with pushbutton commands
namespace guRoo.Cmds_Button
{
    /// <summary>
    /// Example command
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class Cmd_Button: IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Collect the document
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var doc = uiDoc.Document;

            // Show a standard form
            gFrm.Custom.Message(message: "This is a message form.");

            // Ask the user a question
            var yesNoResult = gFrm.Custom.Message(message: "Cancel the task?", yesNo: true);

            // Catch if the user said no (cancel)
            if (yesNoResult.Cancelled)
            {
                return gFrm.Custom.Cancelled("\'No\' was chosen.");
            }

            // Show an error form
            gFrm.Custom.Error("An error did not occur, just showing this form.");

            // Finish with a completed form
            return gFrm.Custom.Completed("Script completed");
        }
    }
}