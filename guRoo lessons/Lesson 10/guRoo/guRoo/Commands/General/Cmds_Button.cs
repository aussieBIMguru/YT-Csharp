﻿// Autodesk
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;

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

            // Testing if the command worked
            TaskDialog.Show("It's working!", doc.Title);

            // Final return
            return Result.Succeeded;
        }
    }
}