// Associated with the extensions namespace
namespace guRoo.Extensions
{
    public static class Document_Ext
    {
        #region Collector constructors

        /// <summary>
        /// Construct a new FilteredElementCollector.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns>A FilteredElementCollector.</returns>
        public static FilteredElementCollector Ext_Collector(this Document doc)
        {
            return new FilteredElementCollector(doc);
        }

        /// <summary>
        /// Construct a new FilteredElementCollector with a given view.
        /// </summary>
        /// <param name="doc">The document (extended).</param>
        /// <param name="view">The view to collect elements from.</param>
        /// <returns>A FilteredElementCollector.</returns>
        public static FilteredElementCollector Ext_Collector(this Document doc, View view)
        {
            if (view is null) { new FilteredElementCollector(doc); }
            return new FilteredElementCollector(doc, view.Id);
        }

        #endregion

        #region Specific collectors

        /// <summary>
        /// Returns all sheets in the document.
        /// </summary>
        /// <param name="doc">The document (extended)</param>
        /// <param name="sorted">Sort the sheets by number.</param>
        /// <param name="includePlaceholders">Include placeholder sheets.</param>
        /// <returns>A list of sheets.</returns>
        public static List<ViewSheet> Ext_GetSheets(this Document doc, bool sorted = true, bool includePlaceholders = false)
        {
            // Collect our sheets
            var sheets = doc.Ext_Collector()
                .OfClass(typeof(ViewSheet))
                .Cast<ViewSheet>()
                .ToList();

            // Filter out placeholders if desired
            if (!includePlaceholders)
            {
                sheets = sheets
                    .Where(s => !s.IsPlaceholder)
                    .ToList();
            }

            // Return elements, optional sorting
            if (sorted)
            {
                return sheets
                    .OrderBy(s => s.SheetNumber)
                    .ToList();
            }
            else
            {
                return sheets;
            }
        }

        /// <summary>
        /// Returns all revisions in the document.
        /// </summary>
        /// <param name="doc">The document (extended)</param>
        /// <param name="sorted">Sort the revisions by sequence.</param>
        /// <returns>A list of Revisions.</returns>
        public static List<Revision> Ext_GetRevisions(this Document doc, bool sorted = true)
        {
            // Collect our revisions
            var revisions = doc.Ext_Collector()
                .OfClass(typeof(Revision))
                .Cast<Revision>()
                .ToList();

            // Return elements, optional sorting
            if (sorted)
            {
                return revisions
                    .OrderBy(r => r.SequenceNumber)
                    .ToList();
            }
            else
            {
                return revisions;
            }
        }

        #endregion
    }
}