// System
using System.Diagnostics;
// Autodesk
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using guRoo.Extensions;

// guRoo
using gFrm = guRoo.Forms;
using gXcl = guRoo.Utilities.Excel_Utils;

// Testing commands
namespace guRoo.Cmds_Testing
{
    /// <summary>
    /// Example command
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class Cmd_Testing : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Get the document
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var doc = uiDoc.Document;

            // Return success
            return Result.Succeeded;
        }
    }
}
