// System
using Form = System.Windows.Forms.Form;
// guRoo
using gFil = guRoo.Utilities.File_Utils;

// The base form will belong to the forms namespace
namespace guRoo.Forms.Base
{
    public partial class BaseDropdown<T> : Form
    {
        // Form properties
        private List<string> Keys;
        private List<T> Values;
        private int DefaultIndex;

        /// <summary>
        /// Constructs a dropdown form.
        /// </summary>
        /// <param name="keys">Keys to display in the dropdown.</param>
        /// <param name="values">Values associated to the keys.</param>
        /// <param name="title">A title to display.</param>
        /// <param name="message">A message to display.</param>
        /// <param name="defaultIndex">An optional index to initialize at.</param>
        /// <returns>A BaseDropDown form.</returns>
        public BaseDropdown(List<string> keys, List<T> values, string title, string message, int defaultIndex = -1)
        {
            // Initialize form, set the icon
            InitializeComponent();
            gFil.SetFormIcon(this);

            // Set the properties
            this.Keys = keys;
            this.Values = values;
            this.DefaultIndex = defaultIndex;
            this.Text = title;
            this.labelMessage.Text = message;

            // Set cancel by default
            this.DialogResult = DialogResult.Cancel;
            this.Tag = null;

            // Load keys to form
            PopulateComboBox();
        }

        /// <summary>
        /// Loads keys to the combobox.
        /// </summary>
        private void PopulateComboBox()
        {
            // Clear the items
            this.comboBox.Items.Clear();

            // Add each key to the combobox
            foreach (var key in this.Keys)
            {
                this.comboBox.Items.Add(key);
            }

            // Set the selected index
            if (this.DefaultIndex >= 0 && this.DefaultIndex < this.comboBox.Items.Count)
            {
                this.comboBox.SelectedIndex = this.DefaultIndex;
            }
            else
            {
                try
                {
                    this.comboBox.SelectedIndex = 0;
                }
                catch
                {
                    this.comboBox.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// Runs when OK is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (this.comboBox.SelectedIndex >= 0)
            {
                var selectedValue = this.Values[this.comboBox.SelectedIndex];

                this.Tag = selectedValue;
                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// Runs when cancel is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
