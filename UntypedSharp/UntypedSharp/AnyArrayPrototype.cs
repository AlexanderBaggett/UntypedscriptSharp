using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace UntypedSharp
{
    public partial class Any<T> : AnyHelpers
    {
        public bool some<TT>(Func<TT, bool> predicate)
        {
            bool found = false;

            Iterate(this, (item, index, length) =>
            {
                var tt = (TT)item;
                found = predicate.Invoke(tt);
                return found;
            }); 

            return found;
        }

        public object at(long searchIndex)
        {
            object tt = default(object); 
            bool found = false;
            if(long.IsPositive(searchIndex))
            {
                Iterate(this, (item, index, length) =>
                {
                    if(searchIndex == index)
                    {
                        tt = item;
                        found = true;
                        return true;
                    }
                    return false;

                });
            }else if(long.IsNegative(searchIndex))
            {
                ReverseIterate(this, (item, index, length) =>
                {
                    if (length + searchIndex == index)
                    {
                        tt = item;
                        found = true;
                        return true;
                    }
                    return false;
                });
            }
            if (found) return tt;
            else return null;
        }
        public dynamic concat(params object[] others)
        {
            if(IsString(this))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(this.Value);
                foreach(var item in others)
                {
                    sb.Append(item.ToString());
                    sb.Append(",");
                }
                string s = sb.ToString();
                s = s.Remove(s.Length-1, s.Length);
                return s;
            }
            else 
            {
                var result = new List<object>();
                Iterate(this, (item, index, length) =>
                {
                    result.Add(item);
                    return false;
                });
                result.AddRange(others);
                return result.ToArray();
            }
        }
        public dynamic concat(object other)
        {
            if (IsString(this))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(this.Value);
                sb.Append(other.ToString());
                return sb.ToString();
            }
            else
            {
                var result = new List<object>();
                Iterate(this, (item, index, length) =>
                {
                    result.Add(item);
                    return false;
                });
                result.Add(other);
                return result.ToArray();
            }
        }

        public dynamic [] map<TSource, TResult>(Func<TSource, TResult> selector)
        {

            if(IsEnumerable(this))
            {
                var asEnumerable = this.Value as IEnumerable;
               return asEnumerable.Cast<TSource>().Select(selector).Select(x=> (object)x).ToArray(); 
            }
            else 
            {
                return this.addHocProperties.Cast<TSource>().Select(selector).Select(x => (object)x).ToArray(); 
            }
        }
        public void forEach<TSource>(Action<TSource> action)
        {

            if (IsEnumerable(this))
            {
                var asEnumerable = this.Value as IEnumerable;
                asEnumerable.Cast<TSource>().ToList().ForEach(action);
            }
            else
            {
                this.addHocProperties.Cast<TSource>().ToList().ForEach(action);
            }
            return;
        }

        public int push(object value)
        {
            if(IsNull(this))
            {
                dynamic list = new List<object>();
                list.Add(value);
                this.Value = list.ToArray();
                return 1;
            }

            if (IsEnumerable(this))
            {
                var asArray = this.Value as IEnumerable;
                dynamic list = asArray.Cast<object>().ToList();
                list.Add(value);
                this.Value = list.ToArray();
                return list.Count;
            }
            else
            {
                addHocProperties.Add(value.ToString(), value);
                return addHocProperties.Count;
            }
        }
        public dynamic pop()
        {
            if (IsEnumerable(this))
            {
                var asArray = this.Value as IEnumerable;
                var list = asArray.Cast<object>().ToList();
                var item = list[list.Count - 1];
               
                list.Remove(item);

                return item;

            }
            else
            {
                var list = addHocProperties.ToList();
                var item = list[list.Count-1];
                list.Remove(item);

                addHocProperties = list.ToDictionary(x=> x.Key, x => x.Value);

                return item;
            }
        }

        public void reverse()
        {
            if (IsEnumerable(this))
            {
                var asArray = this.Value as IEnumerable;
                dynamic list = asArray.Cast<object>().ToList();
                list.Reverse();
                this.Value = (T)list.ToArray();
            }
            else
            {
                addHocProperties = addHocProperties.Reverse().ToDictionary(x=> x.Key,x => x.Value);
            }
        }



        internal static bool IsEnumerable(Any<T> any)
        {

            Type type = any.Value.GetType();

            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                Type[] interfaces = type.GetInterfaces();
                foreach (Type i in interfaces)
                {
                    Console.WriteLine(i.Name);
                    if (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// index counts down in This version
        /// </summary>
        /// <param name="any"></param>
        /// <param name="action"></param>
        internal static void ReverseIterate(Any<T> any, Func<object, long, long, bool> action)
        {
            int index = 0;
            if (IsEnumerable(any))
            {
                dynamic val = any.Value;

                for (index = val.Length; index > -1; index--)
                {

                    var item = val[index];
                    bool exitLoop = action(item, index, any.Length);

                    if (exitLoop)
                    {
                        break;
                    }
                }
            }
            else
            {
                for (index = any.addHocProperties.Count; index > -1; index--)
                {
                    var item = any.addHocProperties.ElementAt(index);
                    bool exitLoop = action(item, index, any.addHocProperties.Count);

                    if (exitLoop)
                    {
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// This gives you the iteration item, the current index and the length in that order
        /// The bool represents the exit coniditon
        /// </summary>
        /// <param name="any"></param>
        /// <param name="action"></param>
        internal static void Iterate(Any<T> any, Func<object, long, long, bool> action)
        {
            int index = 0;
            if (IsEnumerable(any))
            {
                dynamic val = any.Value;

                for (index = 0; index < val.Length; index++)
                {

                    var item = val[index];
                    bool exitLoop= action(item, index, any.Length);

                    if (exitLoop)
                    {
                        break;
                    }
                }
            }
            else
            {
                foreach (var prop in any.addHocProperties)
                {
                    bool exitLoop = action(prop, index, any.Length);
                    if (exitLoop)
                    {
                        break;
                    }
                    index++;
                }
            }
        }
        /// <summary>
        /// gives you the iteration item, the index, and the length
        /// </summary>
        /// <param name="any"></param>
        /// <param name="action"></param>
        /// <param name="exitLoop"></param>
        internal static void Iterate(Any<T> any, Action<object, long, long> action, ref bool exitLoop)
        {
            int index = 0;
            if (IsEnumerable(any))
            {
                dynamic val = any.Value;

                for (index = 0; index < val.Length; index++)
                {
                    if (exitLoop)
                    {
                        break;
                    }
                    var item = val[index];
                    action(item, index, any.Length);
                }
            }
            else
            {
                foreach (var prop in any.addHocProperties)
                {
                    if (exitLoop)
                    {
                        break;
                    }
                    action(prop, index, any.Length);
                    index++;
                }
            }
        }
    }

}