using Microsoft.VisualBasic;

namespace UntypedSharp
{
    public partial class Any<T>
    {
        internal static bool IsBool(Any<T> any) => any.Value is bool;
        internal static bool IsString(Any<T> any) =>  any.Value is string;

        internal static bool IsNumber(Any<T> any) => !(any.Value is string) && !(any.Value is bool) &&  Information.IsNumeric(any.Value);

        internal static bool IsDate(Any<T> any) => any.Value is DateTime || any.Value is DateTimeOffset;

        internal static DateTimeOffset AsDate(Any<T> any) => (DateTimeOffset)(object)any.Value;

        internal static decimal AsNumber(Any<T> any) => Convert.ToDecimal(any.Value);

        internal static bool AsBool(Any<T> any) =>  Convert.ToBoolean(any.Value);

        internal static bool IsNull(Any<T> any)
        {
            // Check if the value itself is null
            if (any.Value == null)
                return true;

            if (any == null) return true;


            // Additional checks to align more with JavaScript's notion of undefined
            // If Value is a string, check for "undefined"
            if (IsString(any) && any.Value.ToString().Trim().ToLower() == "undefined")
                return true;

            return false;
        }

        private static bool IsFalsy(Any<T> any)
        {

            var nullchecker = (object)any;

            //don't want to infinite loop with our == operator
            if (nullchecker == null) return true;

            var value = any.Value;

            if (value == null) return true;

            var isEmptyString = value is string && string.IsNullOrEmpty(value.ToString());
            var isFalseString = value is string && value.ToString().Trim().ToLower() == "false";
            var isFalse = value is bool tg && tg == false;
            var isZeroString =  value is string && value.ToString().Trim().ToLower() == "0";
            var isUndefinedString = value is string && value.ToString().Trim().ToLower() == "undefined";

            if (IsNumber(any))
            {
                //next-level bullshittery
                double x = Convert.ToDouble(value);
                var isZero = x == 0;

                if(isZero) return true;
            }

            return isFalse || isEmptyString || isFalseString || isZeroString || isUndefinedString;

        }
    }
}
