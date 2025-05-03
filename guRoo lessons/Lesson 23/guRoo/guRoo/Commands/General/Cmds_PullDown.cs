// Autodesk
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;

// Associate with PullDown example commands
namespace guRoo.Cmds_PullDown
{
    /// <summary>
    /// Example command
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class Cmd_1Button : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Collect the document
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var doc = uiDoc.Document;

            // Testing if the command worked
            Autodesk.Revit.UI.TaskDialog.Show("Button 1 is working!", doc.Title);

            // Final return
            return Result.Succeeded;
        }
    }

    /// <summary>
    /// Example command
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class Cmd_2Button : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Collect the document
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var doc = uiDoc.Document;

            // Testing if the command worked
            Autodesk.Revit.UI.TaskDialog.Show("Button 2 is working!", doc.Title);

            // Final return
            return Result.Succeeded;
        }
    }

    /// <summary>
    /// Example command
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class Cmd_3Button : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Collect the document
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var doc = uiDoc.Document;

            // Testing if the command worked
            Autodesk.Revit.UI.TaskDialog.Show("Button 3 is working!", doc.Title);

            // Final return
            return Result.Succeeded;
        }
    }
}
