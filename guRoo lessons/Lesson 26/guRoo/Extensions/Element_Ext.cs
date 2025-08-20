// Associate with the extensions namespace
namespace guRoo.Extensions
{
    public static class Element_Ext
    {
        /// <summary>
        /// Checks if an element is editable.
        /// </summary>
        /// <param name="element">The element (extended).</param>
        /// <param name="doc">The related document.</param>
        /// <returns>A Boolean.</returns>
        public static bool Ext_IsEditable(this Element element, Document doc = null)
        {
            // Null check
            if (element is null) { return true; }
            
            // Assign document if not provided
            doc ??= element.Document;

            // If document not workshared, element is editable
            if (!doc.IsWorkshared) { return true; }

            // Get worksharing status'
            var checkoutStatus = WorksharingUtils.GetCheckoutStatus(doc, element.Id);
            var updateStatus = WorksharingUtils.GetModelUpdatesStatus(doc, element.Id);

            // Check owned by us, someone else, or current
            if (checkoutStatus == CheckoutStatus.OwnedByOtherUser) { return false; }
            else if (checkoutStatus == CheckoutStatus.OwnedByCurrentUser) { return true; }
            else { return updateStatus == ModelUpdatesStatus.CurrentWithCentral; }
        }
    }
}
