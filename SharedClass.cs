using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class SharedClass
    {
        public static T CopyValues<T>(T target, T source)
        {
            Type t = typeof(T);
            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);
            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);
                if (value != null && prop.GetValue(source, null) != prop.GetValue(target, null))
                    prop.SetValue(target, value, null);
            }
            return target;
        }
    }
}
