// Associated with the extensions namespace
namespace guRoo.Extensions
{
    public static class Revision_Ext
    {
        /// <summary>
        /// Constructs a name key based on a Revision.
        /// </summary>
        /// <param name="revision">A Revit Revision (extended).</param>
        /// <param name="includeId">Append the ElementId to the end.</param>
        /// <returns>A string.</returns>
        public static string Ext_ToRevisionKey(this Revision revision, bool includeId = false)
        {
            if (revision == null) { return "Invalid revision"; }

            if (includeId)
            {
                return $"{revision.SequenceNumber}: {revision.RevisionDate} - {revision.Description} [{revision.Id.ToString()}]";
            }
            else
            {
                return $"{revision.SequenceNumber}: {revision.RevisionDate} - {revision.Description}";
            }
        }
    }
}
