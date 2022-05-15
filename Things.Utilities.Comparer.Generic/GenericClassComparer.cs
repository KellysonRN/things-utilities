// Reference: https://medium.com/c-sharp-progarmming/c-generic-class-comparer-42489f77695

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Things.Utilities.Comparer.Generic
{

    public sealed class GenericClassComparer<T> : IEqualityComparer<T> where T : class
    {
        private string[] _props;

        private PropertyInfo[] _propertyInfos;

        private Type _type;

        public GenericClassComparer(params string[] props)
        {
            _props = props;
        }

        public bool Equals(T x, T y)
        {
            bool result = true;

            _type = x.GetType();
            _propertyInfos = _type.GetProperties();

            foreach (var iProp in _props)
            {
                var check = _propertyInfos.SingleOrDefault(t => t.Name == iProp);

                if (check.GetValue(x) != null && check.GetValue(y) != null
                    && !check.GetValue(x).Equals(check.GetValue(y)))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public int GetHashCode(T obj)
        {
            string code = string.Empty;

            _type = obj.GetType();
            _propertyInfos = _type.GetProperties();

            foreach (var iProp in _props)
            {
                var check = _propertyInfos.SingleOrDefault(t => t.Name == iProp);
                code += check.GetValue(obj)?.ToString();
            }

            return code.GetHashCode();
        }
    }
}