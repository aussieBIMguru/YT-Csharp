// Associated with the extensions namespace
namespace guRoo.Extensions
{
    public static class FamilySymbol_Ext
    {
        /// <summary>
        /// Presents a name key for a family type.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="includeId"></param>
        /// <returns></returns>
        public static string Ext_ToFamilySymbolKey(this FamilySymbol symbol, bool includeId = false)
        {
            if (symbol == null) { return "Invalid family type"; }

            var symbolKey = $"{symbol.Family.FamilyCategory.Name}: " +
                $"{symbol.Family.Name} - {symbol.Name}";

            if (includeId)
            {
                return $"{symbolKey} [{symbol.Id.ToString()}]";
            }
            else
            {
                return symbolKey;
            }
        }
    }
}
