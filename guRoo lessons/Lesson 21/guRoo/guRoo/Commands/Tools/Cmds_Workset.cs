// Autodesk
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
// guRoo
using guRoo.Extensions;
using gFrm = guRoo.Forms;

namespace guRoo.Cmds_Workset
{
    /// <summary>
    /// Example command
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class Cmd_Create : IExternalCommand
    {
        private static readonly List<string> WorksetNames = new List<string>()
        {
            "AR Interior",
            "AR Facade",
            "AR Structure",
            "AR Fitout",
            "AR Site",
            "Z_Link DWG XXX",
            "Z_Link RVT XXX"
        };

        private static readonly string GridsWorksetName = "AR Levels and Grids";
        
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Collect the document
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var doc = uiDoc.Document;

            // Get worksets and names
            var worksets = doc.Ext_GetWorksets();
            var worksetNames = worksets.Select(w => w.Name).ToList();

            // Get workset names that are not present
            var unusedNames = WorksetNames.Where(n => !worksetNames.Contains(n)).ToList();

            // Cancel if no missing worksets
            if (unusedNames.Count == 0)
            {
                return gFrm.Custom.Cancelled("All standard worksets are available.");
            }

            // Choose the missing names from a list
            var formResult = gFrm.Custom.SelectFromList(keys: unusedNames,
                values: unusedNames.Cast<object>().ToList(),
                title: "Select worksets to add to document");
            if (formResult.Cancelled) { return Result.Cancelled; }
            var chosenWorksetNames = formResult.Objects.Cast<string>().ToList();

            // Offer to rename grids workset if present
            if (worksetNames.Contains("Shared Levels and Grids"))
            {
                // Ask for renaming
                var confirmRenameResult = gFrm.Custom.Message(title: "Rename workset",
                    message: "The default grids and levels workset has been found\n\n" +
                    "Would you like to rename it?",
                    yesNo: true);

                // Check if we said yes
                if (confirmRenameResult.Affirmative)
                {
                    // Get the workset
                    int ind = worksetNames.IndexOf("Shared Levels and Grids");
                    var gridsWorkset = worksets[ind];

                    // If we can edit it
                    if (gridsWorkset.IsEditable)
                    {
                        // Rename it
                        using (var t = new Transaction(doc, "guRoo: Rename grids workset"))
                        {
                            t.Start();

                            WorksetTable.RenameWorkset(doc, gridsWorkset.Id, GridsWorksetName);

                            t.Commit();
                        }
                    }
                    // Notify if we can't
                    else
                    {
                        gFrm.Custom.Error("The workset was not editable.\n\n" +
                            "Remaining worksets will now be created anyway.");
                    }
                }
            }

            // Get progress bar properties
            int progressTotal = chosenWorksetNames.Count;
            int progressStep = gFrm.Utilities.ProgressDelay(progressTotal);

            // Using a progres bar...
            using (var pb = new gFrm.ProgressBar(taskName: "Creating worksets", pbTotal: progressTotal))
            {
                // Using a transaction...
                using (var t = new Transaction(doc, "guRoo: Create worksets"))
                {
                    t.Start();

                    // Add the worksets
                    foreach (var name in chosenWorksetNames)
                    {
                        // Check for cancellation
                        if (pb.CancelCheck(t))
                        {
                            return Result.Cancelled;
                        }

                        // Create the workset
                        Workset.Create(doc, name);

                        // Increment progress
                        Thread.Sleep(progressStep);
                        pb.Increment();
                    }

                    pb.Commit(t);
                }
            }

            // Final message to user
            return gFrm.Custom.Completed($"{progressTotal} workset(s) created");
        }
    }
}