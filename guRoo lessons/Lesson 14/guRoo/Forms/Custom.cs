// System
using System.Windows.Forms;
// Autodesk
using Autodesk.Revit.UI;

// Namespace for all things form related
namespace guRoo.Forms
{
    // This is a static class, we will use it later
    public static class Custom
    {
        #region Message and variants

        /// <summary>
        /// Processes a MessageBox form.
        /// </summary>
        /// <param name="title">Optional form title.</param>
        /// <param name="message">Optional form message.</param>
        /// <param name="yesNo">Show Yes/No buttons.</param>
        /// <param name="noCancel">Only provide an OK button.</param>
        /// <param name="icon">Optional MessageBoxIcon to use.</param>
        /// <returns>A FormResult.</returns>
        public static FormResult Message(string title = null, string message = null,
            bool yesNo = false, bool noCancel = false, MessageBoxIcon icon = MessageBoxIcon.None)
        {
            // Base form result
            var formResult = new FormResult(isValid: false);

            // Default values
            title ??= "Message";
            message ??= "No message was provided.";

            // Catch the question icon
            if (yesNo && icon == MessageBoxIcon.None)
            {
                icon = MessageBoxIcon.Question;
            }

            // Set buttons
            var buttons = MessageBoxButtons.OKCancel;

            if (noCancel)
            {
                buttons = MessageBoxButtons.OK;
            }
            else if (yesNo)
            {
                buttons = MessageBoxButtons.YesNo;
            }

            // Run the form
            var dialogResult = MessageBox.Show(message, title, buttons, icon);

            // Process the result
            if (dialogResult == DialogResult.Yes || dialogResult == DialogResult.OK)
            {
                formResult.Validate();
            }

            // Return the formresult
            return formResult;
        }

        /// <summary>
        /// Processes a Completed form.
        /// </summary>
        /// <param name="message">An optional message.</param>
        /// <returns>Result.Succeeded.</returns>
        public static Result Completed(string message = null)
        {
            // Default message
            message ??= "Task completed.";

            // Show the form
            Message(title: "Task completed",
                message: message,
                noCancel: true,
                icon: MessageBoxIcon.Information);

            // Return the result
            return Result.Succeeded;
        }

        /// <summary>
        /// Processes a Cancelled form.
        /// </summary>
        /// <param name="message">An optional message.</param>
        /// <returns>Result.Cancelled.</returns>
        public static Result Cancelled(string message = null)
        {
            // Default message
            message ??= "Task cancelled.";

            // Show the form
            Message(title: "Task cancelled",
                message: message,
                noCancel: true,
                icon: MessageBoxIcon.Warning);

            // Return the result
            return Result.Cancelled;
        }

        /// <summary>
        /// Processes an Error form.
        /// </summary>
        /// <param name="message">An optional message.</param>
        /// <returns>Result.Cancelled.</returns>
        public static Result Error(string message = null)
        {
            // Default message
            message ??= "Error encountered.";

            // Show the form
            Message(title: "Error encountered",
                message: message,
                noCancel: true,
                icon: MessageBoxIcon.Error);

            // Return the result
            return Result.Cancelled;
        }

        #endregion

        #region Dropdown form

        /// <summary>
        /// Processes a generic object from list.
        /// </summary>
        /// <param name="keys">A list of keys to display.</param>
        /// <param name="values">A list of values to pass.</param>
        /// <param name="title">An optional title to display.</param>
        /// <param name="message">An optional message to display.</param>
        /// <param name="defaultIndex">An optional index to initialize at.</param>
        /// <returns>A FormResult object.</returns>
        public static FormResult SelectFromDropdown(List<string> keys, List<object> values,
            string title = null, string message = null, int defaultIndex = -1)
        {
            // Make a new formResult
            var formResult = new FormResult(isValid: false);

            // Set default values
            title ??= "Select from dropdown";
            message ??= "Select an object from the dropdown:";

            // Process the form
            using (var form = new Base.BaseDropdown(keys, values, title, message, defaultIndex))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    formResult.Validate(form.Tag as object);
                }
            }

            // Return the result
            return formResult;

            #endregion
        }
    }

    #region FormResult class

    // This is a class to make FormResult objects
    public class FormResult
    {
        // Form object properties
        public object Object { get; set; }
        public List<object> Objects { get; set; }

        // Form condition properties
        public bool Cancelled { get; set; }
        public bool Valid { get; set; }
        public bool Affirmative { get; set; }

        // Constructor (default)
        public FormResult()
        {
            this.Object = null;
            this.Objects = new List<object>();
            this.Cancelled = true;
            this.Valid = false;
            this.Affirmative = false;
        }

        // Constructor (alternative)
        public FormResult(bool isValid)
        {
            this.Object = null;
            this.Objects = new List<object>();
            this.Cancelled = !isValid;
            this.Valid= isValid;
            this.Affirmative = isValid;
        }

        // Method to validate
        public void Validate()
        {
            this.Cancelled = false;
            this.Valid = true;
            this.Affirmative = true;
        }

        // Method to validate
        public void Validate(object obj)
        {
            this.Validate();
            this.Object = obj;
        }

        // Method to validate
        public void Validate(List<object> objs)
        {
            this.Validate();
            this.Objects = objs;
        }
    }

    #endregion
}