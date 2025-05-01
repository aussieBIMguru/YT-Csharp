// Autodesk
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
// guRoo
using guRoo.Extensions;
using gFrm = guRoo.Forms;
using gWsh = guRoo.Utilities.Workshare_Utils;

namespace guRoo.Cmds_Revision
{
    /// <summary>
    /// Example command
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class Cmd_BulkRev : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Collect the document
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var doc = uiDoc.Document;

            // Get a revision
            var revisionResult = doc.Ext_SelectRevision();
            if (revisionResult.Cancelled) { return Result.Cancelled; }
            var selectedRevision = revisionResult.Object;

            // Get sheets
            var sheetResult = doc.Ext_SelectSheets();
            if (sheetResult.Cancelled) { return Result.Cancelled; }
            var selectedSheets = sheetResult.Objects;

            // Editable check
            if (doc.IsWorkshared)
            {
                var worksharingResult = gWsh.ProcessElements(selectedSheets);
                if (worksharingResult.Cancelled) { return Result.Cancelled; }
                selectedSheets = worksharingResult.EditableElements;
            }

            // Properties related to task
            int progressTotal = selectedSheets.Count;
            int progressStep = gFrm.Utilities.ProgressDelay(progressTotal);
            int sheetsRevved = 0;

            // Using a progress bar...
            using (var pb = new gFrm.ProgressBar(taskName: "Adding revision to sheet(s)", pbTotal: progressTotal))
            {
                // Using a transaction...
                using (var t = new Transaction(doc, "guRoo: BulkRev"))
                {
                    // Start the transaction
                    t.Start();

                    // For each sheet...
                    foreach (var sheet in selectedSheets)
                    {
                        // Cancel command if progress bar cancelled
                        if (pb.CancelCheck(t))
                        {
                            return Result.Cancelled;
                        }
                        
                        // Add revision
                        var addResult = sheet.Ext_AddRevision(selectedRevision);
                        if (addResult == Result.Succeeded)
                        {
                            sheetsRevved++;
                        }

                        // Increment progress
                        Thread.Sleep(progressStep);
                        pb.Increment();
                    }

                    // Commit the transaction
                    pb.Commit(t);
                }
            }

            // Return the outcome
            return gFrm.Custom.Completed($"{sheetsRevved}/{selectedSheets.Count} sheets revised.\n\n" +
                $"If sheets were skipped, they already had that revision applied.");
        }
    }
}
