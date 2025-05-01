// Autodesk
using View = Autodesk.Revit.DB.View;
// guRoo
using gFrm = guRoo.Forms;

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

        /// <summary>
        /// Get all worksets in document.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="sorted"></param>
        /// <returns></returns>
        public static List<Workset> Ext_GetWorksets(this Document doc, bool sorted = true)
        {
            // Collect our worksets
            var worksets = new FilteredWorksetCollector(doc)
                .OfKind(WorksetKind.UserWorkset)
                .ToWorksets()
                .ToList();

            // Return elements, optional sorting
            if (sorted)
            {
                return worksets
                    .OrderBy(w => w.Name)
                    .ToList();
            }
            else
            {
                return worksets;
            }
        }

        #endregion

        #region Collector based forms

        /// <summary>
        /// Select a revision from the document.
        /// </summary>
        /// <param name="doc">A Revit document (extended).</param>
        /// <param name="title">The form title (optional).</param>
        /// <param name="message">The form message (optional).</param>
        /// <param name="sorted">Sort the Revisions by sequence.</param>
        /// <returns>A FormResult object.</returns>
        public static gFrm.FormResult<Revision> Ext_SelectRevision(this Document doc, string title = null,
            string message = null, bool sorted = true)
        {
            // Default values
            title ??= "Select a revision";
            message ??= "Select a revision from below:";

            // Get revisions
            var revisions = doc.Ext_GetRevisions(sorted);

            // Process into keys and values
            var keys = revisions
                .Select(r => r.Ext_ToRevisionKey())
                .ToList();

            // Return the form outcome
            return gFrm.Custom.SelectFromDropdown<Revision>(keys, revisions, title, message);
        }

        /// <summary>
        /// Select sheet(s) from the document.
        /// </summary>
        /// <param name="doc">A Revit document (extended).</param>
        /// <param name="title">The form title (optional).</param>
        /// <param name="multiSelect">Select one or more objects.</param>
        /// <param name="sorted">Sort the sheets by number.</param>
        /// <param name="includePlaceholders">Include placeholder sheets.</param>
        /// <returns>A FormResult object.</returns>
        public static gFrm.FormResult<ViewSheet> Ext_SelectSheets(this Document doc, string title = null,
            bool multiSelect = true, bool sorted = true, bool includePlaceholders = false)
        {
            // Default title
            title ??= multiSelect ? "Select sheet(s) from the list:" : "Select a sheet from the list";

            // Get sheets
            var sheets = doc.Ext_GetSheets(sorted, includePlaceholders);

            // Process into keys and values
            var keys = sheets
                .Select(s => s.Ext_ToSheetKey())
                .ToList();

            // Return the form outcome
            return gFrm.Custom.SelectFromList<ViewSheet>(keys, sheets, title, multiSelect);
        }

        /// <summary>
        /// Selecting workset(s) from a list.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="title"></param>
        /// <param name="multiSelect"></param>
        /// <param name="sorted"></param>
        /// <returns>A FormResult object.</returns>
        public static gFrm.FormResult<Workset> Ext_SelectWorksets(this Document doc, string title = null,
            bool multiSelect = true, bool sorted = true)
        {
            // Default title
            title ??= multiSelect ? "Select workset(s) from the list:" : "Select a workset from the list";

            // Get worksets
            var worksets = doc.Ext_GetWorksets(sorted);

            // Process into keys and values
            var keys = worksets
                .Select(w => w.Name)
                .ToList();

            // Return the form outcome
            return gFrm.Custom.SelectFromList<Workset>(keys, worksets, title, multiSelect);
        }

        #endregion
    }
}