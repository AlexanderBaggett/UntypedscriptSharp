using System.Dynamic;
using System.Reflection;

//TODO unit tests, arrays, queue features
namespace UntypedSharp
{
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
    }
}