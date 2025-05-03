// Associate to the utility namespace
namespace guRoo.Utilities
{
    public static class Data_Utils
    {
        /// <summary>
        /// Create a list of KeyedValues.
        /// </summary>
        /// <typeparam name="T">The type of values.</typeparam>
        /// <param name="keys">Keys to use.</param>
        /// <param name="values">Values to use.</param>
        /// <returns>A list of KeyedValues.</returns>
        public static List<KeyedValue<T>> CreateKeyedValues<T>(List<string> keys, List<T> values)
        {
            // New list of keyedvalues
            var keyedValues = new List<KeyedValue<T>>();

            // Handle if paircount is 0 or unequal (shortest lacing)
            var pairCount = keys.Count > values.Count ? values.Count : keys.Count;
            if (pairCount == 0) { return keyedValues; }

            // Construct and add keyed values
            for (int i = 0; i < pairCount; i++)
            {
                var keyedValue = new KeyedValue<T>()
                {
                    ItemKey = keys[i],
                    ItemValue = values[i],
                    ItemIndex = i
                };

                keyedValues.Add(keyedValue);
            }

            // Return keyed values
            return keyedValues;
        }

        /// <summary>
        /// Holds keys/values and various other data.
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        public class KeyedValue<T>
        {
            // General data properties
            public T ItemValue { get; set; }
            public string ItemKey { get; set; }
            public int ItemIndex { get; set; }

            // Form specific properties
            public bool Visible { get; set; }
            public bool Checked { get; set; }

            // Default constructor
            public KeyedValue()
            {
                this.Visible = true;
                this.Checked = false;
            }
        }
    }
}
