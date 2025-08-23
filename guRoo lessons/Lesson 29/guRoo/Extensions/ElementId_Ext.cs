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

        /// <summary>
        /// Returns if an ElementId is null or invalid.
        /// </summary>
        /// <param name="elementId">The ElementId (extended).</param>
        /// <returns>A boolean.</returns>
        public static bool Ext_IsNullOrInvalid(this ElementId elementId)
        {
            return elementId is null || elementId == ElementId.InvalidElementId;
        }

        /// <summary>
        /// Returns if an ElementId is not null or invalid.
        /// </summary>
        /// <param name="elementId">The ElementId (extended).</param>
        /// <returns>A boolean.</returns>
        public static bool Ext_IsValid(this ElementId elementId)
        {
            return !elementId.Ext_IsNullOrInvalid();
        }

        /// <summary>
        /// Returns the integer value of an ElementId.
        /// </summary>
        /// <param name="elementId">The ElementId (extended)</param>
        /// <returns>An integer.</returns>
        public static int Ext_IntegerValue(this ElementId elementId)
        {
            if (elementId.Ext_IsNullOrInvalid()) { return -1; }

            #if REVIT2021 || REVIT2022 || REVIT2023
            return elementId.IntegerValue;
            #else
            return (int)elementId.Value;
            #endif
        }
    }
}
