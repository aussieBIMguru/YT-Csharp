// Autodesk
using Autodesk.Revit.UI;
// guRoo
using gFil = guRoo.Utilities.File_Utils;

// Associated with the extensions namespace
namespace guRoo.Extensions
{
    public static class ViewSheet_Ext
    {
        /// <summary>
        /// Constructs a name key based on a Sheet.
        /// </summary>
        /// <param name="sheet">A Revit Sheet (extended).</param>
        /// <param name="includeId">Append the ElementId to the end.</param>
        /// <returns>A string.</returns>
        public static string Ext_ToSheetKey(this ViewSheet sheet, bool includeId = false)
        {
            if (sheet == null) { return "Invalid sheet"; }

            if (includeId)
            {
                return $"{sheet.SheetNumber}: {sheet.Name} [{sheet.Id.ToString()}]";
            }
            else
            {
                return $"{sheet.SheetNumber}: {sheet.Name}";
            }
        }

        /// <summary>
        /// Adds a revision to a sheet.
        /// </summary>
        /// <param name="sheet">The sheet (extended).</param>
        /// <param name="revision">The revision to add.</param>
        /// <returns>A Result.</returns>
        public static Result Ext_AddRevision(this ViewSheet sheet, Revision revision)
        {
            var revisionIds = sheet.GetAdditionalRevisionIds();

            if (revisionIds.Contains(revision.Id))
            {
                return Result.Failed;
            }
            else
            {
                revisionIds.Add(revision.Id);
                sheet.SetAdditionalRevisionIds(revisionIds);
                return Result.Succeeded;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="fileName"></param>
        /// <param name="directoryPath"></param>
        /// <param name="doc"></param>
        /// <param name="options"></param>
        /// <param name="hideCropBoundaries"></param>
        /// <returns></returns>
        public static Result Ext_ExportToPdf(this ViewSheet sheet, string fileName, string directoryPath,
            Document doc = null, PDFExportOptions options = null, bool hideCropBoundaries = true)
        {
            if (sheet is null || fileName is null || directoryPath is null) { return Result.Failed; }

            doc ??= sheet.Document;
            options ??= gFil.DefaultPdfExportOptions(hideCropBoundaries);

            options.FileName = fileName;
            var sheetIds = new List<ElementId>() { sheet.Id };

            try
            {
                doc.Export(directoryPath, sheetIds, options);
                return Result.Succeeded;
            }
            catch
            {
                return Result.Failed;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="doc"></param>
        /// <param name="valueIfInvalid"></param>
        /// <returns></returns>
        public static string Ext_ToExportKey(this ViewSheet sheet, Document doc = null, string valueIfInvalid = "-")
        {
            doc ??= sheet.Document;

            var projectNumber = doc.ProjectInformation.Number;
            var sheetRevision = sheet.Ext_GetStringParameterValue("Current Revision",
                emptyIsInvalid: true, valueIfInvalid: valueIfInvalid);

            return $"{projectNumber}-{sheet.SheetNumber} [{sheetRevision}] - {sheet.Name}";
        }
    }
}