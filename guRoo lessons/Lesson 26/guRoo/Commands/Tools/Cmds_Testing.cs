// System
using System.Diagnostics;
// Autodesk
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
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
            // Excel verification routine
            var filePath = "C:\\Users\\Gavin\\Desktop\\My Excel file.xlsx";

            // Routine to get excel data
            var workbook = gXcl.GetWorkbook(filePath);
            var worksheet = gXcl.GetWorkSheet(workbook, getFirstIfNotFound: true);
            var matrix = gXcl.ReadWorksheet(worksheet);

            // Reading and showing the matrix
            foreach (var row in matrix)
            {
                var readRow = string.Join("\t", row);
                Debug.WriteLine(readRow);
            }

            // Return success
            return Result.Succeeded;
        }
    }
}
