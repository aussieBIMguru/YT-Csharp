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

            // Select a revision
            var revisionResult = doc.Ext_SelectRevision();
            if (revisionResult.Cancelled) { return Result.Cancelled; }
            var selectedRevision = revisionResult.Object as Revision;

            // Test the outcome
            gFrm.Custom.Message(message: selectedRevision.Ext_ToRevisionKey());
            return Result.Succeeded;
        }
    }
}