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
        /// <typeparam name="T">The type of objects.</typeparam>
        /// <param name="objects">Elements to process.</param>
        /// <returns>A WorksharingResult.</returns>
        public static WorksharingResult<T> ProcessElements<T>(List<T> objects)
        {
            // Create a worksharing result, and element lists
            var worksharingResult = new WorksharingResult<T>() { Cancelled = false };
            var editable = new List<T>();
            var notEditable = new List<T>();

            // Add elements to the lists
            foreach (var obj in objects)
            {
                if (obj is Element element)
                {
                    if (!element.Ext_IsEditable(element.Document))
                    {
                        notEditable.Add(obj);
                        continue;
                    }
                }

                editable.Add(obj);
            }

            // Assign lists to result
            worksharingResult.EditableElements = editable;
            worksharingResult.NoneditableElements = notEditable;

            // Catch no editable elements
            if (editable.Count == 0)
            {
                if (objects.Count > 0)
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

        /// <summary>
        /// Stores worksharing processing results.
        /// </summary>
        /// <typeparam name="T">The type of objects.</typeparam>
        public class WorksharingResult<T>
        {
            public List<T> EditableElements { get; set; }
            public List<T> NoneditableElements { get; set; }
            public bool Cancelled { get; set; }
        }
    }
}
