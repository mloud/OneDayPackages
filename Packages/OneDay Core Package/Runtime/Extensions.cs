using System;

namespace OneDay.Core
{
    public static class Extensions
    {
        public static TR Method<TR>(this Type t, string method, object obj = null, params object[] parameters) 
            => (TR)t.GetMethod(method)?.Invoke(obj, parameters);
    }
}