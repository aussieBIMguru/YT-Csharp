// System
using Autodesk.Revit.UI;
using System.IO;
using Form = System.Windows.Forms.Form;

// Associate to the utility namespace
namespace guRoo.Utilities
{
    public static class File_Utils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <param name="iconPath"></param>
        public static void SetFormIcon(Form form, string iconPath = null)
        {
            iconPath ??= "guRoo.Resources.Icons16.IconList16.ico";

            using (Stream stream = Globals.Assembly.GetManifestResourceStream(iconPath))
            {
                if (stream is not null)
                {
                    form.Icon = new Icon(stream);
                }
            }
        }

        /// <summary>
        /// Check if a file can be read.
        /// </summary>
        /// <param name="filePath">The file path to check.</param>
        /// <returns>A boolean</returns>
        public static bool IsFileReadable(string filePath)
        {
            // Catch if file does not exist
            if (!File.Exists(filePath))
            {
                return false;
            }

            // Try to read the file
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static Result OpenDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                return Result.Failed;
            }

            try
            {
                System.Diagnostics.Process.Start("explorer.exe", directoryPath);
                return Result.Succeeded;
            }
            catch
            {
                return Result.Failed;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hideCrop"></param>
        /// <returns></returns>
        public static PDFExportOptions DefaultPdfExportOptions(bool hideCrop = true)
        {
            // New options
            var options = new PDFExportOptions();

            // Configure the settings
            options.AlwaysUseRaster = false;
            options.ColorDepth = ColorDepthType.Color;
            options.ExportQuality = PDFExportQualityType.DPI300;
            options.HideCropBoundaries = hideCrop;
            options.HideReferencePlane = true;
            options.HideScopeBoxes = true;
            options.HideUnreferencedViewTags = true;
            options.MaskCoincidentLines = true;
            options.PaperFormat = ExportPaperFormat.Default;
            options.PaperOrientation = PageOrientationType.Auto;
            options.RasterQuality = RasterQualityType.High;
            options.ReplaceHalftoneWithThinLines = true;
            options.StopOnError = false;
            options.ViewLinksInBlue = false;
            options.ZoomPercentage = 100;
            options.ZoomType = ZoomType.Zoom;

            // Return the options
            return options;
        }
    }
}
