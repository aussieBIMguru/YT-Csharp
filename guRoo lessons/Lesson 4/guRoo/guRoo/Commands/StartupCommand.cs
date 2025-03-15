using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Nice3point.Revit.Toolkit.External;

// Example of referencing a namespace with an alias
using gFrm = guRoo.Forms;

namespace guRoo.Commands
{
    /// <summary>
    ///     External command entry point invoked from the Revit interface
    /// </summary>
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class StartupCommand : ExternalCommand
    {
        public override void Execute()
        {
            // Examples of constructor overloading
            var formResult1 = new gFrm.FormResult();
            var formResult2 = new gFrm.FormResult(true);

            // Examples of working with form
            formResult1.ExampleCancelled = true;
            formResult2.SetToInvalid();
            
            TaskDialog.Show(Document.Title, "Hot reload!");
        }
    }
}