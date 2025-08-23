// Autodesk
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
// guRoo
using guRoo.Extensions;
using gFrm = guRoo.Forms;
using gFil = guRoo.Utilities.File_Utils;

// Testing commands
namespace guRoo.Cmds_Export
{
    /// <summary>
    /// Example command
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class Cmd_SheetsPdf : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Collect the document
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var doc = uiDoc.Document;

            // Select sheets to export
            var formResults = doc.Ext_SelectSheets(title: "Select sheets to export");
            if (formResults.Cancelled) { return Result.Cancelled; }
            var sheets = formResults.Objects;

            // Select a directory to export to
            var directoryResult = gFrm.Custom.SelectFolder(title: "Select folder to export to");
            if (directoryResult.Cancelled) { return Result.Cancelled; }
            var directoryPath = directoryResult.Object;

            // Default pdf export option
            var options = gFil.DefaultPdfExportOptions();

            // Properties related to task
            int progressTotal = sheets.Count;
            int progressStep = gFrm.Utilities.ProgressDelay(progressTotal);
            int sheetsExported = 0;

            // Using a progress bar...
            using (var pb = new gFrm.ProgressBar(taskName: "Exporting sheets", pbTotal: progressTotal))
            {
                // Using a transaction...
                using (var t = new Transaction(doc, "guRoo: Export sheets to PDF"))
                {
                    // Start the transaction
                    t.Start();

                    // For each sheet...
                    foreach (var sheet in sheets)
                    {
                        // Cancel command if progress bar cancelled
                        if (pb.CancelCheck(t))
                        {
                            if (sheetsExported > 0)
                            {
                                gFil.OpenDirectory(directoryPath);
                            }
                            return Result.Cancelled;
                        }

                        // Export the sheet to Pdf
                        var fileName = sheet.Ext_ToExportKey(doc);
                        sheet.Ext_ExportToPdf(fileName, directoryPath, doc, options);

                        // Increment progress
                        sheetsExported++;
                        Thread.Sleep(progressStep);
                        pb.Increment();
                    }

                    // Commit the transaction
                    pb.Commit(t);
                }
            }

            // Return success
            return gFil.OpenDirectory(directoryPath);
        }
    }

    /// <summary>
    /// Example command
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class Cmd_SheetsDwg : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Collect the document
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var doc = uiDoc.Document;

            // Return success
            return Result.Succeeded;
        }
    }
}