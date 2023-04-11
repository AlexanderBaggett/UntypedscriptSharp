using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UntypedSharp
{
    public partial class Any
    {
        
        private Dictionary<string, object> addHocProperties = new Dictionary<string, object>();

        public object this[string key]
        {
            get => getAdhocValue(key);
            set => setAdhocValue(key, value);
        }

        private object getAdhocValue(string key)
        {
            if (!string.IsNullOrWhiteSpace(key) && addHocProperties.ContainsKey(key))
            {
                return addHocProperties[key];
            }
            return null;
        }

        private void setAdhocValue(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key)) return;

            if (addHocProperties.ContainsKey(key))
            {
                addHocProperties[key] = value;
            }
            else
            {
                addHocProperties.Add(key, value);
            }
        }
    }

    public partial class Any<T>
    {
          public T Value { get; set; }

        private Dictionary<string, object> addHocProperties = new Dictionary<string, object>();

        public object this[string key]
        {
            get => getAdhocValue(key);
            set => setAdhocValue(key, value);
        }

        private object getAdhocValue(string key)
        {
            if (!string.IsNullOrWhiteSpace(key) && addHocProperties.ContainsKey(key))
            {
                return addHocProperties[key];
            }
            return null;
        }

        private void setAdhocValue(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key)) return;

            if (addHocProperties.ContainsKey(key))
            {
                addHocProperties[key] = value;
            }
            else
            {
                addHocProperties.Add(key, value);
            }
        }

    }
}
