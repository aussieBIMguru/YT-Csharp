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
    }
}