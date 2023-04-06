using System.Dynamic;

namespace UntypedSharp
{
    public class Any
    {
        public readonly object Value;

        private Dictionary<string, object> addHocProperties = new Dictionary<string, object>();

        public Any(object value)
        {
            Value = value;
            addHocProperties.Add(value.ToString(), value);
        }
        public object this [string key]
        {
            get => addHocValue(key);
            set => setAddhocValue(key, value);
        }

        private object addHocValue(string key)
        {
            if(addHocProperties.ContainsKey(key))
            {
                return addHocProperties[key];
            }
            return null;
        }

        private void setAddhocValue(string key, object value)
        {
            if (addHocProperties.ContainsKey(key))
            {
                addHocProperties[key] = value;
            }
            else
            {
                addHocProperties.Add(key, value); 
            }
        }

        public long Length { get => calculateLength(); }

        private long calculateLength()
        {
            var type = this.Value.GetType();

            if(this.Value is Array array)
            {
                return array.Length;
            }
            return 0;
        }


        // Constructors for various primitive types and object type
        public Any(int value) : this((object)value) { }
        public Any(double value) : this((object)value) { }
        public Any(float value) : this((object)value) { }
        public Any(decimal value) : this((object)value) { }
        public Any(long value) : this((object)value) { }
        public Any(bool value) : this((object)value) { }
        public Any(string value) : this((object)value) { }

        public Any(DateTime value) : this((object)value) { }

        // Implicit operators for various primitive types and object type
        public static implicit operator Any(int value) => new Any(value);
        public static implicit operator Any(double value) => new Any(value);
        public static implicit operator Any(float value) => new Any(value);
        public static implicit operator Any(decimal value) => new Any(value);
        public static implicit operator Any(long value) => new Any(value);
        public static implicit operator Any(bool value) => new Any(value);
        public static implicit operator Any(string value) => new Any(value);
        public static implicit operator Any(DateTime value) => new Any(value);


        public static implicit operator int(Any any) => (int)any.Value;
        public static implicit operator double(Any any) => (double)any.Value;
        public static implicit operator float(Any any) => (float)any.Value;
        public static implicit operator decimal(Any any) => (decimal)any.Value;
        public static implicit operator long(Any any) => (long)any.Value;
        public static implicit operator string(Any any) => (string)any.Value;
        public static implicit operator DateTime(Any any) => (DateTime)any.Value;


        public static implicit operator bool (Any any) => IsFalsy(any);

        private static bool IsFalsy(Any any)
        {
            if (any == null) return true;

            var value = any.Value;

            if (value == null) return true;

            var isEmptyString =  any.Value is string && string.IsNullOrEmpty(any.Value.ToString());
            var isFalseString = any.Value is string && any.Value.ToString().Trim().ToLower() == "false";
            var isZeroString = value is string && value.ToString().Trim().ToLower() == "0";
            var isNumber = any.Value is int || any.Value is long || any.Value is double || any.Value is decimal || any.Value is float;
            var isZero = isNumber && (double) any.Value == 0;

            return  isEmptyString || isFalseString || isZeroString || isZero;

        }





        public T To<T>()
        {
            return (T)Value;
        }
    }
    public class Any<T>
    {
        public Any()
        {
        }

        public Any(T value)
        {
            Value = value;
        } 

        public T Value { get; set; }

        private Dictionary<string, object> addHocProperties = new Dictionary<string, object>();

        public object this[string key]
        {
            get => addHocValue(key);
            set => setAddhocValue(key, value);
        }

        private object addHocValue(string key)
        {
            if (addHocProperties.ContainsKey(key))
            {
                return addHocProperties[key];
            }
            return null;
        }

        private void setAddhocValue(string key, object value)
        {
            if (addHocProperties.ContainsKey(key))
            {
                addHocProperties[key] = value;
            }
            else
            {
                addHocProperties.Add(key, value);
            }
        }

        public long Length { get => calculateLength<T>(); }

        private long calculateLength<T>()
        {
            Type type = typeof(T);

            if (this.Value is Array array)
            {
                return array.Length;
            }

            return 0;
        }


        public static implicit operator Any<T>(T value) => new Any<T>(value);
        public static implicit operator bool (Any<T> any) => IsFalsy(any);

        private static bool IsFalsy(Any<T> any)
        {

            if (any == null) return true;

            var value = any.Value;

            if (value == null) return true;

            var isEmptyString = value is string && string.IsNullOrEmpty(value.ToString());
            var isFalseString =  value is string && value.ToString().Trim().ToLower() == "false";
            var isZeroString =  value is string && value.ToString().Trim().ToLower() == "0";
            var isNumber = value is int || value is long || value is double || value is decimal || value is float;

            double x = new Any(value);
            var isZero = isNumber && x == 0;

            return isEmptyString || isFalseString || isZeroString || isZero;

        }

    }
}