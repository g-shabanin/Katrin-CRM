using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Katrin.AppFramework
{
    public class PropertyValueChangedEventArgs : EventArgs
    {
        Properties properties;
        string key;
        object newValue;
        object oldValue;

        /// <returns>
        /// returns the changed property object
        /// </returns>
        public Properties Properties
        {
            get
            {
                return properties;
            }
        }

        /// <returns>
        /// The key of the changed property
        /// </returns>
        public string Key
        {
            get
            {
                return key;
            }
        }

        /// <returns>
        /// The new value of the property
        /// </returns>
        public object NewValue
        {
            get
            {
                return newValue;
            }
        }

        /// <returns>
        /// The new value of the property
        /// </returns>
        public object OldValue
        {
            get
            {
                return oldValue;
            }
        }

        public PropertyValueChangedEventArgs(Properties properties, string key, object oldValue, object newValue)
        {
            this.properties = properties;
            this.key = key;
            this.oldValue = oldValue;
            this.newValue = newValue;
        }
    }
}
