// Autodesk
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
// guRoo
using gFrm = guRoo.Forms;

// Associated with the extensions namespace
namespace guRoo.Extensions
{
    public static class UIDocument_Ext
    {
        /// <summary>
        /// Gets currently selected elements.
        /// </summary>
        /// <param name="uiDoc">The active UIDocument (extended).</param>
        /// <returns>A list of elements.</returns>
        public static List<Element> Ext_GetSelectedElements(this UIDocument uiDoc)
        {
            if (uiDoc is null) { return new List<Element>(); }

            var doc = uiDoc.Document;

            return uiDoc.Selection.GetElementIds()
                .Select(id => doc.GetElement(id))
                .Where(e => e is not null)
                .ToList();
        }

        /// <summary>
        /// Gets currently selected elements of a given type.
        /// </summary>
        /// <typeparam name="T">The type of elements to get.</typeparam>
        /// <param name="uiDoc">The active UIDocument (extended).</param>
        /// <returns>A list of elements.</returns>
        public static List<T> Ext_GetSelectedElements<T>(this UIDocument uiDoc)
        {
            if (uiDoc is null) { return new List<T>(); }

            return uiDoc.Ext_GetSelectedElements()
                .OfType<T>()
                .Cast<T>()
                .ToList();
        }

        /// <summary>
        /// Set the current selection to given ElementIds.
        /// </summary>
        /// <param name="uiDoc">The UIDocument (extended).</param>
        /// <param name="elementIds">The element Ids to select.</param>
        /// <returns>A Result.</returns>
        public static Result Ext_SelectElementIds(this UIDocument uiDoc, List<ElementId> elementIds)
        {
            if (uiDoc is null) { return Result.Failed; }

            elementIds = elementIds
                .Where(id => id.Ext_IsValid())
                .Distinct()
                .ToList();

            uiDoc.Selection.SetElementIds(elementIds);
            return Result.Succeeded;
        }

        /// <summary>
        /// Set the current selection to given Elements.
        /// </summary>
        /// <param name="uiDoc">The UIDocument (extended).</param>
        /// <param name="elements">The elements to select.</param>
        /// <returns>A Result.</returns>
        public static Result Ext_SelectElements(this UIDocument uiDoc, List<Element> elements)
        {
            var elementIds = elements
                .Select(e => e.Id)
                .ToList();

            return uiDoc.Ext_SelectElementIds(elementIds);
        }

        /// <summary>
        /// Set the current selection to given ElementId.
        /// </summary>
        /// <param name="uiDoc">The UIDocument (extended).</param>
        /// <param name="elementId">The ElementId to select.</param>
        /// <returns>A Result.</returns>
        public static Result Ext_SelectElementId(this UIDocument uiDoc, ElementId elementId)
        {
            if (uiDoc is null || elementId.Ext_IsNullOrInvalid()) { return Result.Failed; }

            return uiDoc.Ext_SelectElementIds(new List<ElementId>() { elementId });
        }

        /// <summary>
        /// Set the current selection to given element.
        /// </summary>
        /// <param name="uiDoc">The UIDocument (extended).</param>
        /// <param name="element">The element to select.</param>
        /// <returns>A Result.</returns>
        public static Result Ext_SelectElement(this UIDocument uiDoc, Element element)
        {
            if (uiDoc is null || element is null) { return Result.Failed; }

            return uiDoc.Ext_SelectElements(new List<Element>() { element } );
        }

        /// <summary>
        /// Run a selection using an ISelectionFilter.
        /// </summary>
        /// <param name="uiDoc">The active UIDocument (extended).</param>
        /// <param name="selectionFilter">The ISelectionFilter to apply.</param>
        /// <param name="prompt">Selection prompt in sub-ribbon.</param>
        /// <param name="showMessage">Show error message if encountered.</param>
        /// <returns>A list of Elements.</returns>
        public static List<Element> Ext_SelectWithFilter(this UIDocument uiDoc, ISelectionFilter selectionFilter,
            string prompt = null, bool showMessage = true)
        {
            prompt ??= "Select element(s)";
            
            try
            {
                return uiDoc.Selection.PickObjects(ObjectType.Element, selectionFilter, prompt)
                    .Select(r => uiDoc.Document.GetElement(r))
                    .Where(e => e is not null)
                    .ToList();
            }
            catch
            {
                if (showMessage)
                {
                    gFrm.Custom.Cancelled("Could not select elements.");
                }
                return new List<Element>();
            }
        }
    }
}
