// We will leave thiss alone for now...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Namespace for all things form related
namespace guRoo.Forms
{
    // This is a static class, we will use it later
    public static class Custom
    {
    }

    // This is a class to make FormResult objects
    public class FormResult
    {
        // Example of private field (best practice)
        private bool cancelled;

        public bool ExampleCancelled
        {
            get { return cancelled; }
            set { cancelled = value; }
        }

        // Public properties (not best practice)
        public object Object { get; set; }
        public List<object> Objects { get; set; }
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

        // Method
        public void SetToInvalid()
        {
            this.Cancelled = true;
            this.Valid = false;
            this.Affirmative = false;
        }
    }
}
