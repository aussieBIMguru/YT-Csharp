// System
using Autodesk.Revit.UI;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;

// Associate to the utility namespace
namespace guRoo.Utilities
{
    // These utilites relate to ribbon creation/management
    public static class Ribbon_Utils
    {
        #region Button data

        /// <summary>
        /// Create a PushButtonData object.
        /// </summary>
        /// <param name="buttonName">The name the user sees.</param>
        /// <returns>A PushButtonData object.</returns>
        public static PushButtonData NewPushButtonData<CommandClass>(string buttonName)
        {
            // Get our base name
            var className = typeof(CommandClass).FullName;
            var baseName = CommandToBaseName(className);

            // Create a data object
            var pushButtonData = new PushButtonData(baseName, buttonName, Globals.AssemblyPath, className);

            // Set the values
            pushButtonData.ToolTip = LookupTooltip(baseName);
            pushButtonData.Image = GetIcon(baseName, resolution: 16);
            pushButtonData.LargeImage = GetIcon(baseName, resolution: 32);

            // Return the object
            return pushButtonData;
        }

        /// <summary>
        /// Create a PulldownButtonData object.
        /// </summary>
        /// <param name="buttonName">The name the user sees.</param>
        /// <param name="nameSpace">The full class name the button runs.</param>
        /// <returns>A PulldownButtonData object.</returns>
        public static PulldownButtonData NewPulldownButtonData(string buttonName, string nameSpace)
        {
            // Get our base name
            var baseName = CommandToBaseName(nameSpace);

            // Create a data object
            var pulldownButtonData = new PulldownButtonData(baseName, buttonName);

            // Set the values
            pulldownButtonData.ToolTip = LookupTooltip(baseName);
            pulldownButtonData.Image = GetIcon(baseName, resolution: 16);
            pulldownButtonData.LargeImage = GetIcon(baseName, resolution: 32);

            // Return the object
            return pulldownButtonData;
        }

        #endregion

        #region Resource management

        /// <summary>
        /// Converts a commnand name to the base name standard.
        /// </summary>
        /// <param name="commandName">The command class name.</param>
        /// <returns>A string (the base name).</returns>
        public static string CommandToBaseName(string commandName)
        {
            return commandName.Replace("guRoo.Cmds_", "").Replace(".Cmd", "");
        }

        /// <summary>
        /// Looks up a tooltip by key from the Global tooltips.
        /// </summary>
        /// <param name="key">The key of the tooltip.</param>
        /// <param name="failValue">Default value to return.</param>
        /// <returns></returns>
        public static string LookupTooltip(string key, string failValue = null)
        {
            // Null coalesce the tooltip
            failValue ??= "No tooltip was found.";

            // Try to find the tooltip value
            if (Globals.Tooltips.TryGetValue(key, out string value))
            {
                return value;
            }

            // Return the default value if it failed
            return failValue;
        }

        /// <summary>
        /// Return the icon resource as an ImageSource.
        /// </summary>
        /// <param name="baseName">The base name of the icon resource.</param>
        /// <param name="resolution">The icon resolution to find.</param>
        /// <returns></returns>
        public static ImageSource GetIcon(string baseName, int resolution = 32)
        {
            // Get the path to the resource
            var resourcePath = $"{Globals.AddinName}.Resources.Icons{resolution}.{baseName}{resolution}.png";

            // Stream the resource
            using (var stream = Globals.Assembly.GetManifestResourceStream(resourcePath))
            {
                // Return null if no stream found
                if (stream is null) { return null; }

                // Convert the resource to a bitmap
                var decoder = new PngBitmapDecoder(
                    stream, 
                    BitmapCreateOptions.PreservePixelFormat,
                    BitmapCacheOption.Default);

                // Return the first frame if available
                if (decoder.Frames.Count > 0)
                {
                    return decoder.Frames.First();
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion
    }
}