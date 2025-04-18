// System
using Form = System.Windows.Forms.Form;
// guRoo
using gFil = guRoo.Utilities.File_Utils;

// The base form will belong to the forms namespace
namespace guRoo.Forms.Base
{
    public partial class BaseListView : Form
    {
        // Form properties
        private bool MultiSelect;
        private List<string> Keys;
        private List<object> Values;

        /// <summary>
        /// Constructs a listview form.
        /// </summary>
        /// <param name="keys">Keys to display in the listview.</param>
        /// <param name="values">Values associated to the keys.</param>
        /// <param name="title">A title to display.</param>
        /// <param name="multiSelect">Select one or more items.</param>
        /// <returns>A BaseListView form.</returns>
        public BaseListView(List<string> keys, List<object> values, string title, bool multiSelect = true)
        {
            // Initialize form, set the icon
            InitializeComponent();
            gFil.SetFormIcon(this);

            // Set the properties
            this.Text = title;
            this.Keys = keys;
            this.Values = values;

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
            // Clear the listview, add a column
            this.listView.Clear();
            this.listView.Columns.Add("Key", 380);

            // Add each key as an item
            foreach (var key in this.Keys)
            {
                var listViewItem = new ListViewItem(key);
                listViewItem.Checked = false;
                this.listView.Items.Add(listViewItem);
            }
        }

        /// <summary>
        /// Runs when OK is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            // Process multiselect
            if (this.MultiSelect && this.listView.CheckedItems.Count > 0)
            {
                this.Tag = this.listView.CheckedItems
                    .Cast<ListViewItem>()
                    .Select(i => this.Values[i.Index])
                    .ToList();

                this.DialogResult = DialogResult.OK;
            }

            // Process single select
            if (!this.MultiSelect && this.listView.SelectedItems.Count > 0)
            {
                int ind = this.listView.SelectedItems[0].Index;
                this.Tag = this.Values[ind];
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
    }
}
