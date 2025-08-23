// Autodesk
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using guRoo.Extensions;

// guRoo
using gFrm = guRoo.Forms;
using gXcl = guRoo.Utilities.Excel_Utils;

// Testing commands
namespace guRoo.Cmds_Import
{
    /// <summary>
    /// Example command
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class Cmd_CreateSheets : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Get the document
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var doc = uiDoc.Document;

            // Select a file path
            var formResult = gFrm.Custom.SelectFilePaths(filter: gFrm.Custom.FILTER_EXCEL, multiSelect: false);
            if (formResult.Cancelled) { return Result.Cancelled; }
            var filePath = formResult.Object;

            // Is the file accessible
            if (gXcl.VerifyExcelFile(filePath) != Result.Succeeded)
            {
                return Result.Cancelled;
            }

            // Read our excel data
            var workbook = gXcl.GetWorkbook(filePath);
            var worksheet = gXcl.GetWorkSheet(workbook, getFirstIfNotFound: true);
            var matrix = gXcl.ReadWorksheet(worksheet);

            // Verify the file is suitable
            if (matrix.Count < 2 || matrix[0][0] != "Number" || matrix[0][1] != "Name")
            {
                return gFrm.Custom.Cancelled("Excel file structure is not valid.");
            }

            // Remove header
            matrix.RemoveAt(0);

            // Select a title block type
            var formResultTtb = doc.Ext_SelectTitleBlockTypes(multiSelect: false);
            if (formResultTtb.Cancelled) { return Result.Cancelled; }
            var ttbTypeId = formResultTtb.Object.Id;

            // Get the sheets in the document
            var sheets = doc.Ext_GetSheets(includePlaceholders: true, sorted: false);
            var sheetDictionary = new Dictionary<string, ViewSheet>();
            foreach (var sheet in sheets)
            {
                sheetDictionary[sheet.SheetNumber.ToLower()] = sheet;
            }

            // Properties related to task
            int progressTotal = matrix.Count;
            int progressStep = gFrm.Utilities.ProgressDelay(progressTotal);
            int sheetsCreated = 0;
            int sheetsUpdated = 0;
            int sheetsSkipped = 0;

            // Using a progress bar...
            using (var pb = new gFrm.ProgressBar(taskName: "Creating/updating sheet(s)", pbTotal: progressTotal))
            {
                // Using a transaction...
                using (var t = new Transaction(doc, "guRoo: Import Sheets"))
                {
                    // Start the transaction
                    t.Start();

                    // For each row...
                    foreach (var row in matrix)
                    {
                        // Cancel command if progress bar cancelled
                        if (pb.CancelCheck(t))
                        {
                            return Result.Cancelled;
                        }

                        // Name and number
                        var sheetNumber = row[0];
                        var sheetName = row[1];

                        // Does the sheet exist
                        if (sheetDictionary.TryGetValue(sheetNumber.ToLower(), out var exSheet))
                        {
                            if (exSheet.Ext_IsEditable(doc) && exSheet.Name != sheetName)
                            {
                                exSheet.Name = sheetName;
                                sheetsUpdated++;
                            }
                            else
                            {
                                sheetsSkipped++;
                            }
                        }
                        // If not, create it
                        else
                        {
                            var newSheet = ViewSheet.Create(doc, ttbTypeId);
                            newSheet.Name = sheetName;
                            newSheet.SheetNumber = sheetNumber;
                            sheetsCreated++;
                        }

                        // Increment progress
                        Thread.Sleep(progressStep);
                        pb.Increment();
                    }

                    // Commit the transaction
                    pb.Commit(t);
                }
            }

            // Form to tell the user what happened
            return gFrm.Custom.Completed("The task has finished.\n\n" +
                $"{sheetsCreated} sheets created.\n" +
                $"{sheetsUpdated} sheets updated.\n" +
                $"{sheetsSkipped} sheets skipped.\n\n" +
                $"Skipped sheets were either not editable, or their name was the same as in Excel.");
        }
    }
}