using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Dynamic;

namespace UntypedSharp
{

    public partial class AnyHelpers
    {
        internal static bool IsLessThan(string s1, string s2)
        {
            int len = Math.Min(s1.Length, s2.Length);

            for (int i = 0; i < len; i++)
            {
                if (s1[i] < s2[i])
                {
                    return true;
                }
                else if (s1[i] > s2[i])
                {
                    return false;
                }
                else if (s1[i] == s2[i])
                {
                    continue;
                }
            }

            return s1.Length < s2.Length;
        }

        internal static bool IsAnyGenericType(object obj)
        {
            var anytype = typeof(Any<>);
            var objType = obj.GetType();

            return objType.IsGenericType &&
            objType.GetGenericTypeDefinition() == anytype;

        }

        internal static bool IsNumber(object obj)
        {
            return !(obj is string) && Information.IsNumeric(obj);
        }
        internal static decimal AsNumber(object obj) => Convert.ToDecimal(obj);

        internal static bool IsString(object obj) => obj is string;

        internal object GetValue()
        {
            var x = this as dynamic;
            return x.Value;
        }


    }



    public partial class Any : AnyHelpers
    {
      

    }



    public partial class Any<T> :AnyHelpers
    {


        public static object operator -(Any<T> any, object obj)
        {
            if(obj is Any<T> objAny)
            {
                //they are both numbers
                if (IsNumber(any) && IsNumber(objAny))
                {
                    return AsNumber(any) - AsNumber(objAny);
                }
                return "NaN";
            }

            //they are both numbers
            if (IsNumber(any) && IsNumber(obj))
            {
                return AsNumber(any) - AsNumber(obj);
            }
            return "NaN";
        }

        public static bool operator <(Any<T> any, object obj)
        {
            if(obj is Any<T> objAny)
            {
                //they are both numbers
                if (IsNumber(any) && IsNumber(objAny))
                {
                    return AsNumber(any) < AsNumber(objAny);
                }
                //they are both bools
                if (IsBool(any) && IsBool(objAny))
                {
                    var b1 = AsBool(any);

                    if (b1)
                    {
                        return false;
                    }
                    return true; //true will always be > false and < true
                }
                //they are both dates
                if (IsDate(any) && IsDate(objAny))
                {
                    var date1 = (DateTimeOffset)(object)any.Value;
                    var date2 = (DateTimeOffset)(object)objAny.Value;
                    return date1 < date2;
                }
                //they are both strings
                if (IsString(any) && IsString(objAny))
                {
                    var string1 = any.Value.ToString();
                    var s2 = objAny.Value.ToString();

                    return AnyHelpers.IsLessThan(string1, s2);

                }
                //use falsey check by default
                return any < objAny;
            }

            //they are both numbers
            if (IsNumber(any) && Information.IsNumeric(obj))
            {
                return AsNumber(any) < AsNumber(obj);
            }
            if (IsBool(any) && obj is bool)
            {
                var b1 = AsBool(any);

                if (b1)
                {
                    return false;
                }
                return true; //true will always be > false and < true
            }
            if (IsDate(any) && IsDate(new Any(obj)))
            {
                var date1 = (DateTimeOffset) (object)any.Value;
                var date2 = (DateTimeOffset)obj;
                return date1 < date2;
            }
            if (IsString(any) && obj is string string2)
            {
                var string1 = any.Value.ToString();

                return AnyHelpers.IsLessThan(string1, string2);

            }
            return false;
        }

        public static bool operator >(Any<T> any, object obj)
        {
            if(obj is Any<T> objAny)
            {
                //they are both numbers
                if (IsNumber(any) && IsNumber(objAny))
                {
                    return AsNumber(any) > AsNumber(objAny);
                }
                if (IsBool(any) && IsBool(objAny))
                {
                    var b2 = AsBool(objAny);
                    if (b2)
                    {
                        return false;
                    }
                    return true; //true will always be > false and < true
                }
                if (IsDate(any) && IsDate(objAny))
                {
                    var date1 = (DateTimeOffset)(object)any.Value;
                    var date2 = (DateTimeOffset)(object)objAny.Value;
                    return date1 > date2;
                }
                if (IsString(any) && IsString(objAny))
                {
                    var string1 = any.Value.ToString();
                    var s2 = objAny.Value.ToString();

                    return !AnyHelpers.IsLessThan(string1, s2);

                }
                return any > obj;
            }

            //they are both numbers
            if (IsNumber(any) &&  IsNumber(obj))
            {
                return AsNumber(any) >AsNumber(obj);
            }
            if (IsBool(any) && obj is bool b)
            {
                if (b)
                {
                    return false;
                }
                return true; //true will always be > false and < true
            }
            if (IsDate(any) && IsDate(new Any(obj)))
            {
                var date1 = (DateTimeOffset)(object)any.Value;
                var date2 = (DateTimeOffset)obj;
                return date1 > date2;
            }
            if (IsString(any) && obj is string string2)
            {
                var string1 = any.Value.ToString();

                return !AnyHelpers.IsLessThan(string1, string2);

            }
            return true;
        }

        public static bool operator ==(Any<T> any, object obj)
        {
            if(obj is Any<T> objAny)
            {
                //they are both numbers
                if (IsNumber(any) && IsNumber(objAny))
                {
                    return AsNumber(any) == AsNumber(objAny);
                }
                //left is number right is string
                if (IsNumber(any) && IsString(objAny))
                {
                    var anynumber = AsNumber(any);

                    if (decimal.TryParse(objAny.Value.ToString(), out decimal number2))
                    {
                        return anynumber == number2;
                    }
                    return false;
                }

                //right is number left is string
                if (IsString(any) && IsNumber(objAny))
                {
                    var number2 = AsNumber(objAny);
                    if (decimal.TryParse(any.Value.ToString(), out decimal number1))
                    {
                        return number1 == number2;
                    }
                    return false;
                }

                //both are bools
                if (IsBool(any) && IsBool(objAny))
                {
                    return AsBool(any) == AsBool(objAny);
                }
                //left is bool right is string
                if (IsBool(any) && IsString(objAny))
                {
                    if (AsBool(any) == true && objAny.Value.ToString() == "true")
                    {
                        return true;
                    }
                    if (AsBool(any) == false && objAny.Value.ToString() == "false")
                    {
                        return true;
                    }
                    return false;
                }

                //right is bool left is string
                if (IsString(any) && IsBool(objAny))
                {
                    if (AsBool(objAny) == true && any.Value.ToString() == "true")
                    {
                        return true;
                    }
                    if (AsBool(objAny) == false && any.Value.ToString() == "false")
                    {
                        return true;
                    }
                    return false;
                }

                //left is number and right is bool
                if (IsNumber(any) && IsBool(objAny))
                {
                    if (AsBool(objAny) == true && AsNumber(any) == 1)
                    {
                        return true;
                    }
                    if (AsBool(objAny) == false && AsNumber(any) == 0)
                    {
                        return true;
                    }
                    return false;
                }


                //right is number and left is bool
                if (IsBool(any) && IsNumber(objAny))
                {
                    if (AsBool(any) == true && AsNumber(objAny) == 1)
                    {
                        return true;
                    }
                    if (AsBool(any) == false && AsNumber(objAny) == 0)
                    {
                        return true;
                    }
                    return false;
                }
                //both are dates
                if (IsDate(any) && IsDate(objAny))
                {
                    var date1 = (DateTimeOffset)(object)any.Value;
                    var date2 = (DateTimeOffset)(object)objAny.Value;
                    return date1 == date2;
                }
                //both are strings
                if (IsString(any) && IsString(objAny))
                {
                    var string1 = any.Value.ToString();
                    var string2 = objAny.Value.ToString();

                    return string1.Equals(string2);
                }

                return false;
            }


            //they are both numbers
            if (IsNumber(any) && IsNumber(obj))
            {
                return AsNumber(any) == AsNumber(obj);
            }
            //left is number right is string
            if (IsNumber(any) && obj is string s)
            {
                var anynumber = AsNumber(any);

                if (decimal.TryParse(obj.ToString(), out decimal number2))
                {
                    return anynumber == number2;
                }
                return false;
            }

            //right is number left is string
            if (IsString(any) && Information.IsNumeric(obj))
            {
                var number2 = Convert.ToDouble(obj);
                if (double.TryParse(any.Value.ToString(), out double number1))
                {
                    return number1 == number2;
                }
                return false;
            }

            //both are bools
            if (IsBool(any) && obj is bool b)
            {
                return AsBool(any) == b;
            }
            if (IsDate(any) && IsDate(new Any(obj)))
            {
                var date1 = (DateTimeOffset)(object)any.Value;
                var date2 = (DateTimeOffset)obj;
                return date1 == date2;
            }
            if (IsString(any) && IsString(new Any(obj)))
            {
                var string1 = any.Value.ToString();
                var string2 = obj.ToString();

                return string1.Equals(string2);
            }

            return false;
        }

        public static bool operator !=(Any<T> any, object obj)
        {
            return !(any == obj);
        }

        public static bool operator <=(Any<T> any, object obj)
        {
            return (any < obj) || any == obj;
        }
        public static bool operator >=(Any<T> any, object obj)
        {

            return (any < obj) || any == obj;
        }

    }
}
