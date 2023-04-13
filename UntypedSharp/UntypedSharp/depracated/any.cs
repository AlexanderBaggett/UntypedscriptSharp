using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UntypedSharp.depracated
{
    public partial class Any
    {
        public readonly object Value;

        public Any(object value)
        {
            Value = value;
            if (value != null)
            {
                addHocProperties.Add(value.ToString(), value);
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



        public T To<T>()
        {
            return (T)Value;
        }
    }
}
