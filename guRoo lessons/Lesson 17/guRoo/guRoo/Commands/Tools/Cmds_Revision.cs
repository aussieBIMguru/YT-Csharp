using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var selectedRevision = revisionResult.Object as Revision;

            // Get sheets
            var sheetResult = doc.Ext_SelectSheets();
            if (sheetResult.Cancelled) { return Result.Cancelled; }
            var selectedSheets = sheetResult.Objects.Cast<ViewSheet>().ToList();

            // Editable check
            if (doc.IsWorkshared)
            {
                var worksharingResult = gWsh.ProcessElements(selectedSheets.Cast<Element>().ToList(), doc);
                if (worksharingResult.Cancelled) { return Result.Cancelled; }
                selectedSheets = worksharingResult.EditableElements.Cast<ViewSheet>().ToList();
            }

            // Track revisions added
            int sheetsRevved = 0;

            // Edit the sheets
            using (var t = new Transaction(doc, "guRoo: BulkRev"))
            {
                t.Start();

                // For each sheet...
                foreach (var sheet in selectedSheets)
                {
                    // Add revision
                    var addResult = sheet.Ext_AddRevision(selectedRevision);
                    if (addResult == Result.Succeeded)
                    {
                        sheetsRevved++;
                    }
                }

                t.Commit();
            }

            // Return the outcome
            return gFrm.Custom.Completed($"{sheetsRevved}/{selectedSheets.Count} sheets revised.\n\n" +
                $"If sheets were skipped, they already had that revision applied.");
        }
    }
}
