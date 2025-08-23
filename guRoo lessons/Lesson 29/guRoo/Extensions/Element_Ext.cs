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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="parameterName"></param>
        /// <param name="valueIfInvalid"></param>
        /// <param name="emptyIsInvalid"></param>
        /// <returns></returns>
        public static string Ext_GetStringParameterValue(this Element element, string parameterName,
            string valueIfInvalid = "", bool emptyIsInvalid = false)
        {
            if (element is null || parameterName == null) { return valueIfInvalid; }

            var parameter = element.LookupParameter(parameterName);
            if (parameter is null) { return valueIfInvalid; }

            if (parameter.StorageType == StorageType.String)
            {
                var value = parameter.AsString();
                
                if (value == string.Empty && emptyIsInvalid)
                {
                    return valueIfInvalid;
                }
                else
                {
                    return value;
                }
            }

            return valueIfInvalid;
        }
    }
}
