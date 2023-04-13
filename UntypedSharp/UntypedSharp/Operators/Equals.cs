using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UntypedSharp.depracated;

namespace UntypedSharp
{
    public partial class AnyHelpers
    {
        public static bool operator ==(object left, AnyHelpers right)
        {
            if (IsFalsy(left) && IsFalsy(right))
            {
                return true;
            }

            //they are both numbers
            if (IsNumber(left) && IsNumber(right))
            {
                return AsNumber(left) == AsNumber(right);
            }
            //left is number right is string
            if (IsNumber(left) && IsString(right))
            {
                var anynumber = AsNumber(left);

                if (decimal.TryParse(AsString(right), out decimal number2))
                {
                    return anynumber == number2;
                }
                return false;
            }

            //right is number left is string
            if (IsString(left) && IsNumber(right))
            {
                var number2 = AsNumber(right);
                if (decimal.TryParse(AsString(left), out decimal number1))
                {
                    return number1 == number2;
                }
                return false;
            }

            //both are bools
            if (IsBool(left) && IsBool(right))
            {
                return AsBool(left) == AsBool(right);
            }
            //left is bool right is string
            if (IsBool(left) && IsString(right))
            {
                if (AsBool(left) == true && AsString(right) == "true")
                {
                    return true;
                }
                if (AsBool(left) == false && AsString(right) == "false")
                {
                    return true;
                }
                return false;
            }

            //right is bool left is string
            if (IsString(left) && IsBool(right))
            {
                if (AsBool(right) == true && AsString(left) == "true")
                {
                    return true;
                }
                if (AsBool(right) == false && AsString(left) == "false")
                {
                    return true;
                }
                return false;
            }

            //left is number and right is bool
            if (IsNumber(left) && IsBool(right))
            {
                if (AsBool(right) == true && AsNumber(left) == 1)
                {
                    return true;
                }
                if (AsBool(right) == false && AsNumber(left) == 0)
                {
                    return true;
                }
                return false;
            }


            //right is number and left is bool
            if (IsBool(left) && IsNumber(right))
            {
                if (AsBool(left) == true && AsNumber(right) == 1)
                {
                    return true;
                }
                if (AsBool(left) == false && AsNumber(right) == 0)
                {
                    return true;
                }
                return false;
            }
            if (IsBool(left) && IsNull(right))
            {
                return true;
            }
            if (IsNull(left) && IsBool(right))
            {
                return true;
            }

            //both are dates
            if (IsDate(left) && IsDate(right))
            {
                var date1 = AsDate(left);
                var date2 = AsDate(right);
                return date1 == date2;
            }
            //both are strings
            if (IsString(left) && IsString(right))
            {
                var string1 = AsString(left);
                var string2 = AsString(right);

                return string1.Equals(string2);
            }

            return false;
        }
    }
    public partial class Any<T> : AnyHelpers
    {

        public static bool operator ==(Any<T> left, AnyHelpers right)
        {
            if(IsFalsy(left) && IsFalsy(right))
            {
                return true;
            }

            //they are both numbers
            if (IsNumber(left) && IsNumber(right))
            {
                return AsNumber(left) == AsNumber(right);
            }
            //left is number right is string
            if (IsNumber(left) && IsString(right))
            {
                var anynumber = AsNumber(left);

                if (decimal.TryParse(AsString(right), out decimal number2))
                {
                    return anynumber == number2;
                }
                return false;
            }

            //right is number left is string
            if (IsString(left) && IsNumber(right))
            {
                var number2 = AsNumber(right);
                if (decimal.TryParse(AsString(left), out decimal number1))
                {
                    return number1 == number2;
                }
                return false;
            }

            //both are bools
            if (IsBool(left) && IsBool(right))
            {
                return AsBool(left) == AsBool(right);
            }
            //left is bool right is string
            if (IsBool(left) && IsString(right))
            {
                if (AsBool(left) == true && AsString(right) == "true")
                {
                    return true;
                }
                if (AsBool(left) == false && AsString(right) == "false")
                {
                    return true;
                }
                return false;
            }

            //right is bool left is string
            if (IsString(left) && IsBool(right))
            {
                if (AsBool(right) == true && AsString(left) == "true")
                {
                    return true;
                }
                if (AsBool(right) == false && AsString(left) == "false")
                {
                    return true;
                }
                return false;
            }

            //left is number and right is bool
            if (IsNumber(left) && IsBool(right))
            {
                if (AsBool(right) == true && AsNumber(left) == 1)
                {
                    return true;
                }
                if (AsBool(right) == false && AsNumber(left) == 0)
                {
                    return true;
                }
                return false;
            }


            //right is number and left is bool
            if (IsBool(left) && IsNumber(right))
            {
                if (AsBool(left) == true && AsNumber(right) == 1)
                {
                    return true;
                }
                if (AsBool(left) == false && AsNumber(right) == 0)
                {
                    return true;
                }
                return false;
            }
            if (IsBool(left) && IsNull(right))
            {
                return true;
            }
            if (IsNull(left) && IsBool(right))
            {
                return true;
            }
            //both are dates
            if (IsDate(left) && IsDate(right))
            {
                var date1 = AsDate(left);
                var date2 = AsDate(right);
                return date1 == date2;
            }
            //both are strings
            if (IsString(left) && IsString(right))
            {
                var string1 = AsString(left);
                var string2 = AsString(right);

                return string1.Equals(string2);
            }

            return false;
        }

        public static bool operator ==(Any<T> left, object right)
        {
            if (IsFalsy(left) && IsFalsy(right))
            {
                return true;
            }

            //they are both numbers
            if (IsNumber(left) && IsNumber(right))
            {
                return AsNumber(left) == AsNumber(right);
            }
            //left is number right is string
            if (IsNumber(left) && IsString(right))
            {
                var anynumber = AsNumber(left);

                if (decimal.TryParse(AsString(right), out decimal number2))
                {
                    return anynumber == number2;
                }
                return false;
            }

            //right is number left is string
            if (IsString(left) && IsNumber(right))
            {
                var number2 = AsNumber(right);
                if (decimal.TryParse(AsString(left), out decimal number1))
                {
                    return number1 == number2;
                }
                return false;
            }

            //both are bools
            if (IsBool(left) && IsBool(right))
            {
                return AsBool(left) == AsBool(right);
            }
            //left is bool right is string
            if (IsBool(left) && IsString(right))
            {
                if (AsBool(left) == true && AsString(right) == "true")
                {
                    return true;
                }
                if (AsBool(left) == false && AsString(right) == "false")
                {
                    return true;
                }
                return false;
            }

            //right is bool left is string
            if (IsString(left) && IsBool(right))
            {
                if (AsBool(right) == true && AsString(left) == "true")
                {
                    return true;
                }
                if (AsBool(right) == false && AsString(left) == "false")
                {
                    return true;
                }
                return false;
            }

            //left is number and right is bool
            if (IsNumber(left) && IsBool(right))
            {
                if (AsBool(right) == true && AsNumber(left) == 1)
                {
                    return true;
                }
                if (AsBool(right) == false && AsNumber(left) == 0)
                {
                    return true;
                }
                return false;
            }


            //right is number and left is bool
            if (IsBool(left) && IsNumber(right))
            {
                if (AsBool(left) == true && AsNumber(right) == 1)
                {
                    return true;
                }
                if (AsBool(left) == false && AsNumber(right) == 0)
                {
                    return true;
                }
                return false;
            }
            if(IsBool(left) && IsNull(right))
            {
                return true;
            }
            if (IsNull(left) && IsBool(right))
            {
                return true;
            }

            //both are dates
            if (IsDate(left) && IsDate(right))
            {
                var date1 = AsDate(left);
                var date2 = AsDate(right);
                return date1 == date2;
            }
            //both are strings
            if (IsString(left) && IsString(right))
            {
                var string1 = AsString(left);
                var string2 = AsString(right);

                return string1.Equals(string2);
            }

            return false;
        }

    }

}

