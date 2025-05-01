// Autodesk
using Autodesk.Revit.UI;

// Associate with the root namespace
namespace guRoo
{
    public static class Availability
    {
        // The class name we can use to avoid writing it many times
        private static string PATH_AVAILABILITY = "guRoo.Availability";

        /// <summary>
        /// Shortcuts to availability class names.
        /// </summary>
        public static class AvailabilityNames
        {
            public static string Disabled { get { return $"{PATH_AVAILABILITY}+Disabled"; } }
            public static string ZeroDoc { get { return $"{PATH_AVAILABILITY}+ZeroDoc"; } }
            public static string Project { get { return $"{PATH_AVAILABILITY}+Project"; } }
            public static string Family { get { return $"{PATH_AVAILABILITY}+Family"; } }
            public static string Workshared { get { return $"{PATH_AVAILABILITY}+Workshared"; } }
            public static string Selection { get { return $"{PATH_AVAILABILITY}+Selection"; } }
        }
        
        // Availability - Disabled
        private class Disabled : IExternalCommandAvailability
        {
            public bool IsCommandAvailable(UIApplication uiApp, CategorySet selectedCategories)
            {
                return false;
            }
        }

        // Availability - Zero document
        private class ZeroDoc : IExternalCommandAvailability
        {
            public bool IsCommandAvailable(UIApplication uiApp, CategorySet selectedCategories)
            {
                return true;
            }
        }

        // Availability - Is the document a project
        private class Project : IExternalCommandAvailability
        {
            public bool IsCommandAvailable(UIApplication uiApp, CategorySet selectedCategories)
            {
                if (uiApp.ActiveUIDocument is UIDocument uiDoc)
                {
                    return !uiDoc.Document.IsFamilyDocument;
                }
                else
                {
                    return false;
                }
            }
        }

        // Availability - Is the document a family
        private class Family : IExternalCommandAvailability
        {
            public bool IsCommandAvailable(UIApplication uiApp, CategorySet selectedCategories)
            {
                if (uiApp.ActiveUIDocument is UIDocument uiDoc)
                {
                    return uiDoc.Document.IsFamilyDocument;
                }
                else
                {
                    return false;
                }
            }
        }

        // Availability - Is the document workshared
        private class Workshared : IExternalCommandAvailability
        {
            public bool IsCommandAvailable(UIApplication uiApp, CategorySet selectedCategories)
            {
                if (uiApp.ActiveUIDocument is UIDocument uiDoc)
                {
                    return uiDoc.Document.IsWorkshared;
                }
                else
                {
                    return false;
                }
            }
        }

        // Availability - Do we have selection
        private class Selection : IExternalCommandAvailability
        {
            public bool IsCommandAvailable(UIApplication uiApp, CategorySet selectedCategories)
            {
                if (uiApp.ActiveUIDocument is UIDocument uiDoc)
                {
                    return selectedCategories.Size > 0;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
