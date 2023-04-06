using System.Dynamic;
using System.Reflection;

//TODO unit tests, arrays, queue features
namespace UntypedSharp
{
    public partial class Any
    {
        public readonly object Value;

        public Any(object value)
        {
            Value = value;
            if(value != null)
            {
                addHocProperties.Add(value.ToString(), value);
            }
        }
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

        public long Length { get => calculateLength(); }

        private long calculateLength()
        {
            var type = this.Value.GetType();

            if (this.Value is Array array)
            {
                return array.Length;
            }
            return 0;
        }

        // Implicit operators for various primitive types and object type
        public static implicit operator Any(int value) => new Any(value);
        public static implicit operator Any(double value) => new Any(value);
        public static implicit operator Any(float value) => new Any(value);
        public static implicit operator Any(decimal value) => new Any(value);
        public static implicit operator Any(long value) => new Any(value);
        public static implicit operator Any(bool value) => new Any(value);
        public static implicit operator Any(string value) => new Any(value);
        public static implicit operator Any(DateTime value) => new Any(value);
        public static implicit operator Any(DateTimeOffset value) => new Any(value);
        public static implicit operator Any(DateTimeKind value) => new Any(value);
        public static implicit operator Any(Guid value) => new Any(value);


        public static implicit operator int(Any any) => (int)any.Value;
        public static implicit operator double(Any any) => (double)any.Value;
        public static implicit operator float(Any any) => (float)any.Value;
        public static implicit operator decimal(Any any) => (decimal)any.Value;
        public static implicit operator long(Any any) => (long)any.Value;
        public static implicit operator string(Any any) => (string)any.Value;
        public static implicit operator DateTime(Any any) => (DateTime)any.Value;


        public static implicit operator bool(Any any) => !IsFalsy(any);

        private static bool IsFalsy(Any any)
        {
            if (any == null) return true;

            var value = any.Value;

            if (value == null) return true;

            var isEmptyString = any.Value is string && string.IsNullOrEmpty(any.Value.ToString());
            var isFalseString = any.Value is string && any.Value.ToString().Trim().ToLower() == "false";
            var isFalse = value is bool tg && tg == false;
            var isZeroString = value is string && value.ToString().Trim().ToLower() == "0";
            var isNumber = any.Value is int || any.Value is long || any.Value is double || any.Value is decimal || any.Value is float;
            var isZero = isNumber && Convert.ToDouble(any.Value) == 0;

            return  isFalse || isEmptyString || isFalseString || isZeroString || isZero;

        }

        public T To<T>()
        {
            return (T)Value;
        }
    }
    public partial class Any<T>
    {
        public Any()
        {
        }

        public Any(T value)
        {
            Value = value;
            if (value != null)
            {
                addHocProperties.Add(value.ToString(), value);
            }
        } 

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

        public long Length { get => calculateLength(); }

        private long calculateLength()
        {
            if (Value is Array array)
            {
                return array.Length;
            }

            return 0;
        }

        public static implicit operator Any<T>(T value) => new Any<T>(value);
        public static implicit operator bool (Any<T> any) => !IsFalsy(any);
        public static implicit operator Any(Any<T> value) => new Any(value.Value);
        public static implicit operator Any<T>(Any value) => bullshittery(value.Value);

        private static Any<T> bullshittery(object value)
        {
            var Valuetype = value.GetType();


            Type AnyGenericType = typeof(Any<>);

            // Create the specific type by combining the generic type definition with the actual runtime type
            Type AnySpecificType = AnyGenericType.MakeGenericType(Valuetype);

            // Create an instance of the specific type
            Any<T> any = (Any<T>)Activator.CreateInstance(AnySpecificType,value);

            return any;
        }

        private static bool IsFalsy(Any<T> any)
        {

            if (any == null) return true;

            var value = any.Value;

            if (value == null) return true;

            var isEmptyString = value is string && string.IsNullOrEmpty(value.ToString());
            var isFalseString =  value is string && value.ToString().Trim().ToLower() == "false";
            var isFalse = value is bool tg && tg == false;
            var isZeroString =  value is string && value.ToString().Trim().ToLower() == "0";
            var isNumber = value is int || value is long || value is double || value is decimal || value is float;

            if(isNumber)
            {
                //next-level bullshittery
                double x = Convert.ToDouble(value);
                var isZero = x == 0;

                if(isZero) return true;
            }

            return isFalse || isEmptyString || isFalseString || isZeroString;

        }

    }
}