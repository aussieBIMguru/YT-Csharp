// System
using Form = System.Windows.Forms.Form;
// guRoo
using gFil = guRoo.Utilities.File_Utils;
using gDat = guRoo.Utilities.Data_Utils;

// The base form will belong to the forms namespace
namespace guRoo.Forms.Base
{
    public partial class BaseListView<T> : Form
    {
        // Form properties
        private bool MultiSelect;
        private List<gDat.KeyedValue<T>> KeyedValues;
        private List<int> VisibleIndices;
        private string FilterString;

        /// <summary>
        /// Constructs a listview form.
        /// </summary>
        /// <param name="keys">Keys to display in the listview.</param>
        /// <param name="values">Values associated to the keys.</param>
        /// <param name="title">A title to display.</param>
        /// <param name="multiSelect">Select one or more items.</param>
        /// <returns>A BaseListView form.</returns>
        public BaseListView(List<string> keys, List<T> values, string title, bool multiSelect = true)
        {
            // Initialize form, set the icon
            InitializeComponent();
            gFil.SetFormIcon(this);

            // Set the properties
            this.Text = title;
            this.KeyedValues = gDat.CreateKeyedValues<T>(keys, values);
            this.FilterString = "";

            // Multiselect behavior
            this.MultiSelect = multiSelect;
            this.listView.MultiSelect = multiSelect;
            this.listView.CheckBoxes = multiSelect;
            this.buttonCheckAll.Enabled = multiSelect;
            this.buttonUncheckAll.Enabled = multiSelect;

            // Set cancel by default
            this.DialogResult = DialogResult.Cancel;
            this.Tag = null;

            // Load keys to form
            PopulateListView();
        }

        /// <summary>
        /// Load the keys to the ListView.
        /// </summary>
        private void PopulateListView()
        {
            // Update visible indices
            UpdateVisibleIndices();

            // Clear the listview, add a column
            this.listView.Clear();
            this.listView.Columns.Add("Key", 380);

            // Add each key as an item
            foreach (var keyedValue in this.KeyedValues)
            {
                if (keyedValue.Visible)
                {
                    var listViewItem = new ListViewItem(keyedValue.ItemKey);
                    listViewItem.Checked = keyedValue.Checked;
                    this.listView.Items.Add(listViewItem);
                }
            }
        }

        /// <summary>
        /// Updates the visible indices list.
        /// </summary>
        private void UpdateVisibleIndices()
        {
            // Reset the list
            this.VisibleIndices = new List<int>();

            // Add each visible key's index
            for (int i = 0; i < this.KeyedValues.Count; i++)
            {
                if (this.KeyedValues[i].Visible)
                {
                    this.VisibleIndices.Add(i);
                }
            }
        }

        /// <summary>
        /// Runs when OK is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            // Update checked value state
            UpdateCheckedStates();

            // If multiselect...
            if (this.MultiSelect)
            {
                // Get checked values
                var checkedValues = this.KeyedValues
                    .Where(kv => kv.Checked)
                    .Select(kv => kv.ItemValue)
                    .ToList();

                // If we have any, tag and OK
                if (checkedValues.Count > 0)
                {
                    this.Tag = checkedValues;
                    this.DialogResult = DialogResult.OK;
                }
            }
            // Otherwise if we have a selectiom...
            else if (this.listView.SelectedItems.Count > 0)
            {
                // Get the selected index and the keyvalue index
                int listViewIndex = this.listView.SelectedItems[0].Index;
                int keyValueIndex = this.VisibleIndices[listViewIndex];

                // Tag the value and OK
                this.Tag = this.KeyedValues[keyValueIndex].ItemValue;
                this.DialogResult = DialogResult.OK;
            }

            // Close the form
            this.Close();
        }

        /// <summary>
        /// Runs when cancel is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // Close the form
            this.Close();
        }

        /// <summary>
        /// Runs when check all is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCheckAll_Click(object sender, EventArgs e)
        {
            // Stop if not multiselect
            if (!this.MultiSelect) { return; }

            // Check and select each item
            foreach (ListViewItem item in this.listView.Items)
            {
                item.Checked = true;
                item.Selected = true;
            }
        }

        /// <summary>
        /// Runs when uncheck all is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUncheckAll_Click(object sender, EventArgs e)
        {
            // Stop if not multiselect
            if (!this.MultiSelect) { return; }

            // Uncheck and unselect each item
            foreach (ListViewItem item in this.listView.Items)
            {
                item.Checked = false;
                item.Selected = false;
            }
        }

        /// <summary>
        /// Update keyedvalue checked properties.
        /// </summary>
        private void UpdateCheckedStates()
        {
            // For each listview item...
            for (int i = 0; i < this.listView.Items.Count; i++)
            {
                // Get its corresponding keyedvalue
                var keyedValueIndex = this.VisibleIndices[i];
                var listViewItem = this.listView.Items[i];

                // Set if it is checked
                this.KeyedValues[keyedValueIndex].Checked = listViewItem.Checked;
            }
        }

        /// <summary>
        /// Runs when the text filter changes.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void textFilter_TextChanged(object sender, EventArgs e)
        {
            // Update keyedvalue check states
            UpdateCheckedStates();

            // Update the filter string
            this.FilterString = this.textBoxFilter.Text.ToLower();

            // Set each keyedvalue visible based on passing filter string
            foreach (var keyedValue in this.KeyedValues)
            {
                keyedValue.Visible = PassesTextFilter(keyedValue.ItemKey);
            }

            // Repopulate listview
            PopulateListView();
        }

        /// <summary>
        /// Verify if a string passes the filter.
        /// </summary>
        /// <param name="text">The text to check.</param>
        /// <returns>A boolean.</returns>
        private bool PassesTextFilter(string text)
        {
            // If no filter string, it passes
            if (this.FilterString == "")
            {
                return true;
            }

            // Return if it contains the filter string
            return text.ToLower().Contains(this.FilterString);
        }
    }
}
