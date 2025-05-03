// Autodesk
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
// guRoo
using guRoo.Extensions;
using gFrm = guRoo.Forms;

namespace guRoo.Cmds_Select
{
    /// <summary>
    /// Example command
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class Cmd_GetTtbs : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Collect the document
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var doc = uiDoc.Document;

            // Get selected sheet Ids
            var selectedSheetIds = uiDoc.Ext_GetSelectedElements<ViewSheet>()
                .Select(s => s.Id)
                .ToList();

            // Get all title blocks that are on any of those sheets
            var titleBlocks = doc.Ext_ElementsOfCategory(BuiltInCategory.OST_TitleBlocks)
                .Where(ttb => selectedSheetIds.Contains(ttb.OwnerViewId))
                .ToList();

            // Catch if no title blocks
            if (titleBlocks.Count == 0)
            {
                return gFrm.Custom.Cancelled("No titleblocks were found.");
            }

            // Select the title blocks
            return uiDoc.Ext_SelectElements(titleBlocks);
        }
    }
}
