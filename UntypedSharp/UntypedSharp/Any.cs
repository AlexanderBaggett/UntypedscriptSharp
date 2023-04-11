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
    }
}