// Autodesk
using Autodesk.Revit.UI;
using System.Runtime.CompilerServices;

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
    }
}