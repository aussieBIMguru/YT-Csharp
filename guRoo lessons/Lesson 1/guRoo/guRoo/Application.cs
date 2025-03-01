using guRoo.Commands;
using Nice3point.Revit.Toolkit.External;

namespace guRoo
{
    /// <summary>
    ///     Application entry point
    /// </summary>
    [UsedImplicitly]
    public class Application : ExternalApplication
    {
        public override void OnStartup()
        {
            CreateRibbon();
        }

        private void CreateRibbon()
        {
            var panel = Application.CreatePanel("Commands", "guRoo");

            panel.AddPushButton<StartupCommand>("Execute")
                .SetImage("/guRoo;component/Resources/Icons/RibbonIcon16.png")
                .SetLargeImage("/guRoo;component/Resources/Icons/RibbonIcon32.png");
        }
    }
}