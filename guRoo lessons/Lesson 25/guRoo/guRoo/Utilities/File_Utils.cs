// System
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
    }
}
