// Autodesk
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using guRoo.Extensions;

// Associate with pushbutton commands
namespace guRoo.Cmds_Button
{
    /// <summary>
    /// Example command
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class Cmd_Button: IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Collect the document
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var doc = uiDoc.Document;

            // Collect all walls
            var walls = new FilteredElementCollector(doc)
                .OfClass(typeof(Wall))
                .WhereElementIsNotElementType()
                .ToElements();

            // Show the message
            TaskDialog.Show(doc.Title, $"We have {walls.Count} walls in the model.");

            // Construct a filter
            var parameterId = new ElementId(BuiltInParameter.WALL_USER_HEIGHT_PARAM);
            var provider = new ParameterValueProvider(parameterId);
            var rule = new FilterNumericLess();
            var passesRule = new FilterDoubleRule(provider, rule, 12, 0.1);
            var paramFilter = new ElementParameterFilter(passesRule);

            // Collect all walls lower than 12 feet
            var wallsFiltered = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Walls)
                .WhereElementIsNotElementType()
                .WherePasses(paramFilter)
                .ToElements();

            // Show the message
            TaskDialog.Show(doc.Title, $"We have {wallsFiltered.Count} walls less than 12 ft in the model.");

            // Collect all sheets
            var sheets = doc.Ext_GetSheets();
            var revisions = doc.Ext_GetRevisions();

            // Show the message
            TaskDialog.Show(doc.Title, $"We have {sheets.Count} sheets in the model.");

            // Show the message
            TaskDialog.Show(doc.Title, $"We have {revisions.Count} revisions in the model.");

            // Final return
            return Result.Succeeded;
        }
    }
}