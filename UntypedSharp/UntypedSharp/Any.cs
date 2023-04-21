using System.Collections;
using System.Dynamic;
using System.Reflection;
using System.Runtime.CompilerServices;

//TODO unit tests, arrays, queue features
namespace UntypedSharp
{
    public partial class Any<T> :AnyHelpers
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
            if (IsEnumerable(this))
            {
                var x = this.Value as IEnumerable<object>;
                return x.LongCount();
            }

            return addHocProperties.LongCount();
        }

        public static implicit operator Any<T>(T value) => new Any<T>(value);
        public static implicit operator bool (Any<T> any) => !IsFalsy(any);

    }

    internal class Intermediary<T> 
    {

        internal T Value { get; set; }

        internal Intermediary(){}

        internal Intermediary(T value)
        {
            Value = value; 
        }
        public static implicit operator Any(Intermediary<T> intermediary) =>  new Any(intermediary.Value);
        public static implicit operator Any<T>(Intermediary<T> intermediary) => new Any<T>(intermediary.Value);

        public static implicit operator Intermediary<T>(Any <T> value) => new Intermediary<T>(value.Value);
        public static implicit operator Intermediary<T>(Any any) => new Intermediary<T>(any.Value);
    }

    public partial class Any : Any<dynamic>
    {

        public Any() { }
        public Any(dynamic value) : base ((object)value)
        { 
        
        }

    }


    internal partial class AnyIEnumberable<T> : List<Any<T>>

    {
        internal AnyIEnumberable()
        {

        }

        internal AnyIEnumberable(T value) : base(new List<Any<T>>() {value })
        {

        }

        internal AnyIEnumberable(IEnumerable<T> value) : base(value.Select(x => new Any<T>(x)))
        {

        }

    }
}