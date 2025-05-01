// Associate with the extensions namespace
namespace guRoo.Extensions
{
    public static class ElementId_Ext
    {
        /// <summary>
        /// Get an element from an ElementId.
        /// </summary>
        /// <typeparam name="T">The type to return the element as.</typeparam>
        /// <param name="elementId">The ElementId (extended).</param>
        /// <param name="doc">The document the Element is from.</param>
        /// <returns>T.</returns>
        public static T Ext_GetElement<T>(this ElementId elementId, Document doc)
        {
            // Null check
            if (elementId is null || doc is null) { return default(T); }
            
            // Get the element
            var element = doc.GetElement(elementId);

            // Return the element if possible as T
            if (element is T typedElement)
            {
                return typedElement;
            }
            else
            {
                return default(T);
            }
        }
    }
}
