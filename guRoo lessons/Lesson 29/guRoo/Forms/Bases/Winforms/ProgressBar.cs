// System
using Form = System.Windows.Forms.Form;
using FormApplication = System.Windows.Forms.Application;

// Associate with the forms namespace
namespace guRoo.Forms
{
    public partial class ProgressBar : Form
    {
        // Properties
        private bool Cancelled;
        private int ProgressCount;
        private int ProgressTotal;
        private bool ShowProgress;
        private bool Cancellable;
        private string TaskName;
        
        /// <summary>
        /// Implements a progress bar form.
        /// </summary>
        /// <param name="taskName">Name of the task to run.</param>
        /// <param name="pbTotal">Total number of steps.</param>
        /// <param name="showProgress">Report progress to the user.</param>
        /// <param name="cancellable">Can the form be cancelled.</param>
        public ProgressBar(string taskName = null, int pbTotal = 100, bool showProgress = true, bool cancellable = true)
        {
            // Initialize form components
            InitializeComponent();

            // Set progress values
            this.ProgressCount = 1;
            this.ProgressTotal = pbTotal;
            this.progressBarObject.Minimum = 0;
            this.progressBarObject.Maximum = pbTotal;
            this.progressBarObject.Value = 1;

            // Set label properties
            taskName ??= "Processing task";
            this.TaskName = taskName;
            this.labelProgress.Text = taskName;
            this.ShowProgress = showProgress;

            // Cancellation properties
            this.Cancelled = false;
            this.Cancellable = cancellable;
            this.buttonCancel.Enabled = cancellable;

            // Show the form, do events
            Show();
            FormApplication.DoEvents();
        }

        /// <summary>
        /// Increase progress
        /// </summary>
        public void Increment()
        {
            // If we wont exceed the total, increase count by 1
            if (this.ProgressCount < this.ProgressTotal)
            {
                this.ProgressCount++;
            }
            
            // Set the value
            this.progressBarObject.Value = this.ProgressCount;

            // If we can decrement, refresh progress (fixes a bug)
            if (this.ProgressCount > 1)
            {
                this.progressBarObject.Value = this.ProgressCount - 1;
                this.progressBarObject.Value = this.ProgressCount;
            }

            // If we haven't cancelled and show progress, update the task description
            if (this.ShowProgress && !this.Cancelled)
            {
                this.labelProgress.Text = $"{this.TaskName} ({this.ProgressCount} of {this.ProgressTotal})";
            }

            // Do events to update form
            FormApplication.DoEvents();
        }

        /// <summary>
        /// Check for cancellation
        /// </summary>
        /// <param name="t">Related transaction to the task.</param>
        /// <returns>A boolean.</returns>
        public bool CancelCheck(Transaction t = null)
        {
            // If we cancelled and have a transaction, roll it back
            if (this.Cancelled && t is Transaction)
            {
                t.RollBack();
            }

            // Return if we have cancelled
            return this.Cancelled;
        }

        /// <summary>
        /// Commits a transaction if we didn't cancel.
        /// </summary>
        /// <param name="t">Related transaction to the task.</param>
        public void Commit(Transaction t)
        {
            // If we haven't cancelled, commit the task
            if (!this.Cancelled)
            {
                t.Commit();
            }
        }

        /// <summary>
        /// Cancel the progress bar.
        /// </summary>
        private void Cancel()
        {
            // If we haven't cancelled, run a cancellation
            if (!this.Cancelled)
            {
                this.Cancelled = true;
                this.labelProgress.Text = "Cancelling task...";
                this.buttonCancel.Enabled = false;
            }
        }

        /// <summary>
        /// Runs when the cancel button is clicked.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }
    }
}
