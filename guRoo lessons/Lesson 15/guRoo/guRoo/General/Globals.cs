// System
using Assembly = System.Reflection.Assembly;
// Autodesk
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
using System.Resources;
using System.Globalization;
using System.Collections;

// Associate to the root namespace
namespace guRoo
{
    // These class stores properties globally
    public static class Globals
    {
        #region Properties

        // Applications
        public static UIControlledApplication UiCtlApp { get; set; }
        public static ControlledApplication CtlApp { get; set; }
        public static UIApplication UiApp { get; set; }

        // Assembly
        public static Assembly Assembly { get; set; }
        public static string AssemblyPath { get; set; }

        // Revit versions
        public static string RevitVersion { get; set; }
        public static int RevitVersionInt { get; set; }

        // User names
        public static string UsernameRevit { get; set; }
        public static string UsernameWindows { get; set; }

        // Guids and versioning
        public static string AddinVersionNumber { get; set; }
        public static string AddinVersionName { get; set; }
        public static string AddinName { get; set; }
        public static string AddinGuid { get; set; }

        // Dictionaries for resources
        public static Dictionary<string, string> Tooltips { get; set; } = new Dictionary<string, string>();

        #endregion

        #region Register method

        /// <summary>
        /// Register global properties on startup.
        /// </summary>
        /// <param name="uiCtlApp">The UIControlledApplication</param>
        public static void RegisterProperties(UIControlledApplication uiCtlApp)
        {
            UiCtlApp = uiCtlApp;
            CtlApp = uiCtlApp.ControlledApplication;
            // UiApp set on idling

            Assembly = Assembly.GetExecutingAssembly();
            AssemblyPath = Assembly.Location;

            RevitVersion = CtlApp.VersionNumber;
            RevitVersionInt = Int32.Parse(RevitVersion);

            // Revit username set on idling
            UsernameWindows = Environment.UserName;

            AddinVersionNumber = "25.03.01";
            AddinVersionName = "WIP";
            AddinName = "guRoo";
            AddinGuid = "C3B0315F-A402-4384-9BB9-D3FE4400D4A6";
        }

        #endregion

        #region Register tooltips method

        /// <summary>
        /// Stores the tooltip resource as a dictionary to Globals.
        /// </summary>
        /// <param name="resourcePath">The path to the resource file.</param>
        public static void RegisterTooltips(string resourcePath)
        {
            // Get the resource set
            var resourceManager = new ResourceManager(resourcePath, Globals.Assembly);
            var resourceSet = resourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);

            // Store the entries to the dictionary
            foreach (DictionaryEntry entry in resourceSet)
            {
                var key = entry.Key.ToString();
                var value = entry.Value.ToString();

                Tooltips[key] = value;
            }
        }

        #endregion
    }
}