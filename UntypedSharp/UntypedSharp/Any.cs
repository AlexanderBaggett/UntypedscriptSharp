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

    public partial class Any : Any<object>
    {
        public Any() { }
        public Any(object value) : base (value)
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

        internal AnyIEnumberable(IEnumerable<Any<T>> value) : base(value)
        {

        }

    }

    ///// <summary>
    ///// This is not actually needed 
    ///// </summary>
    ///// <typeparam name = "T" ></ typeparam >
    //internal class Intermediary<T,T2>
    //{
    //    internal T Value { get; set; }

    //    internal Intermediary() { }

    //    internal Intermediary(T value)
    //    {
    //        Value = value;
    //    }
    //    public static implicit operator Any(Intermediary<T,T2> intermediary) => new Any(intermediary.Value);
    //    public static implicit operator Any<T>(Intermediary<T, T2> intermediary) => new Any<T>(intermediary.Value);

    //    public static implicit operator Intermediary<T, T2>(Any<T> value) => new Intermediary<T, T2>(value.Value);
    //    public static implicit operator Intermediary<T, T2>(Any any) => new Intermediary<T, T2>((T)any.Value);

    //    public static implicit operator AnyIEnumberable<T> (Intermediary<T,T2> intermediary)
    //    {
    //        ///this shouldn't even be possible
    //        if(intermediary.Value is IEnumerable<T2> stuff)
    //        {
    //           var any =  stuff.Select(x => new Any(x));

    //            ///probably throw an invalid cast exception here or something
    //            var anyT = any.Select(x => new Any<T>((T)x.Value));
    //            return new AnyIEnumberable<T>(anyT);
    //        }
    //        //should never be the case
    //        if (intermediary.Value is IEnumerable<T> stuff2)
    //        {
    //            var any = stuff2.Select(x => new Any<T>(x));
    //            return new AnyIEnumberable<T>(any);
    //        }
    //        //this is not very helpful
    //        return new AnyIEnumberable<T>(intermediary.Value);

    //    }

    //}
}