// guRoo
using guRoo.Extensions;
using gFrm = guRoo.Forms;

// Associate with the utilities namespace
namespace guRoo.Utilities
{
    public static class Workshare_Utils
    {
        /// <summary>
        /// Processes elements based on their editability.
        /// </summary>
        /// <param name="elements">Elements to process.</param>
        /// <param name="doc">The related document.</param>
        /// <returns>A WorksharingResult.</returns>
        public static WorksharingResult ProcessElements(List<Element> elements, Document doc = null)
        {
            // Create a worksharing result, and element lists
            var worksharingResult = new WorksharingResult() { Cancelled = false };
            var editable = new List<Element>();
            var notEditable = new List<Element>();

            // Add elements to the lists
            foreach (var element in elements)
            {
                if (element.Ext_IsEditable(doc))
                {
                    editable.Add(element);
                }
                else
                {
                    notEditable.Add(element);
                }
            }

            // Assign lists to result
            worksharingResult.EditableElements = editable;
            worksharingResult.NoneditableElements = notEditable;

            // Catch no editable elements
            if (editable.Count == 0)
            {
                if (elements.Count > 0)
                {
                    gFrm.Custom.Cancelled("No required elements are editable.\n\n" +
                        "The task can not proceed, and has been cancelled.");
                }
                worksharingResult.Cancelled = true;
            }
            // Catch not all elements are editable
            else if (notEditable.Count > 0)
            {
                var formResult = gFrm.Custom.Message(title: "Choose how to proceed",
                    message: "Not all elements are editable, would you like to proceed with those that are?",
                    yesNo: true);

                if (!formResult.Affirmative)
                {
                    worksharingResult.Cancelled = true;
                }
            }

            // Return worksharing result
            return worksharingResult;
        }
    }

    /// <summary>
    /// Stores worksharing processing results.
    /// </summary>
    public class WorksharingResult
    {
        public List<Element> EditableElements { get; set; }
        public List<Element> NoneditableElements { get; set; }
        public bool Cancelled { get; set; }
    }
}
