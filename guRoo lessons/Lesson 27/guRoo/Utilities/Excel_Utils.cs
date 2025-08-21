// System
using System.IO;
// ClosedXML
using ClosedXML.Excel;
// Autodesk
using Autodesk.Revit.UI;
// guRoo
using gFrm = guRoo.Forms;
using gFil = guRoo.Utilities.File_Utils;
using DocumentFormat.OpenXml.Spreadsheet;

// Belonging to the Utilities namespace
namespace guRoo.Utilities
{
    /// <summary>
    /// Functions for dealing with Excel files.
    /// </summary>
    public static class Excel_Utils
    {
        /// <summary>
        /// Attempt to get a workbook at a filepath.
        /// </summary>
        /// <param name="filePath">The filepath to access.</param>
        /// <returns>An XLWorkbook</returns>
        public static XLWorkbook GetWorkbook(string filePath)
        {
            // Try to read the workbook
            try
            {
                return new XLWorkbook(filePath);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Attempt to get a worksheet, with the option to default to the first one.
        /// </summary>
        /// <param name="workbook">The workbook to check.</param>
        /// <param name="worksheetName">The name of the worksheet to get.</param>
        /// <param name="getFirstIfNotFound">Get the first worksheet if we fail to find.</param>
        /// <returns>An IXLWorksheet</returns>
        public static IXLWorksheet GetWorkSheet(XLWorkbook workbook, string worksheetName = "Sheet1", bool getFirstIfNotFound = false)
        {
            // Null check
            if (workbook is null)
            {
                return null;
            }

            // Check each worksheet
            foreach (var worksheet in workbook.Worksheets)
            {
                if (worksheet.Name == worksheetName)
                {
                    return worksheet;
                }
            }

            // Get the first if we failed to find
            if (getFirstIfNotFound)
            {
                return workbook.Worksheets.First();
            }

            // Return null if we got no worksheet
            return null;
        }

        /// <summary>
        /// Verifies if an Excel file can be read.
        /// </summary>
        /// <param name="filePath">The filepath to check.</param>
        /// <param name="worksheetName">An optional worksheet to find.</param>
        /// <returns>A Result.</returns>
        public static Result VerifyExcelFile(string filePath, string worksheetName = null)
        {
            // Catch if file does not exist
            if (!File.Exists(filePath))
            {
                return gFrm.Custom.Cancelled("The file could not be found.");
            }

            // Catch if file cannot be read
            if (!gFil.IsFileReadable(filePath))
            {
                return gFrm.Custom.Cancelled("The file could not be read.");
            }

            // If we want to check for a worksheet by name...
            if (worksheetName is not null)
            {
                // Get the workbook
                var workbook = GetWorkbook(filePath);

                // If we did not found the worksheet, cancel
                if (GetWorkSheet(workbook, worksheetName, false) is null)
                {
                    return gFrm.Custom.Cancelled($"{worksheetName} was not found in the Excel file.");
                }
            }

            // Otherwise, we can proceed
            return Result.Succeeded;
        }

        /// <summary>
        /// Reads an Excel worksheet.
        /// </summary>
        /// <param name="worksheet">The worksheet to read.</param>
        /// <param name="readRows">Number of rows to read (all by default).</param>
        /// <param name="readColumns">Number of columns to read (all by default).</param>
        /// <returns></returns>
        public static List<List<string>> ReadWorksheet(IXLWorksheet worksheet, int readRows = 0, int readColumns = 0)
        {
            // Matrix to construct, null check
            var matrix = new List<List<string>>();
            if (worksheet is null) { return matrix; }

            // Get range used, check if we are trying to read too much
            var rangeUsed = worksheet.RangeUsed();
            if (rangeUsed.RowCount() <= readRows) { readRows = 0; }
            if (rangeUsed.ColumnCount() <= readColumns) { readColumns = 0; }

            // Numbers of rows we have read
            int rowsRead = 0;

            // For each row in the worksheet...
            foreach (var row in rangeUsed.RowsUsed())
            {
                // New row to build
                var dataList = new List<string>();

                // If we can still read rows...
                if (rowsRead < readRows || readRows == 0)
                {
                    // Number of columns we have read
                    int columnsRead = 0;

                    // For each cell in the row...
                    foreach (var cell in row.Cells())
                    {
                        // If we can stll read columns...
                        if (columnsRead < readColumns || readColumns == 0)
                        {
                            // Get its trimmed value as text
                            var value = cell.GetString().Trim();
                            dataList.Add(value);
                        }
                        columnsRead++;
                    }
                }

                // Add the row to the matrix
                matrix.Add(dataList);
                rowsRead++;
            }

            // Return the matrix
            return matrix;
        }
    }
}
