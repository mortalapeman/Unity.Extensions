using Microsoft.Practices.ObjectBuilder2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Unity.ExternalContext
{
    public interface IExternalContextPolicy : IBuilderPolicy
    {
        void RegisterProperty(Type type, PropertyInfo info);
        bool IsRegistered(Type type, out PropertyInfo[] propertyType);
    }

    public class ExternalContextPolicy : IExternalContextPolicy
    {
        private Dictionary<Type, List<PropertyInfo>> _Map = new Dictionary<Type, List<PropertyInfo>>();

        public bool IsRegistered(Type type, out PropertyInfo[] propertyType)
        {
            propertyType = null;
             if (_Map.ContainsKey(type))
            {
                propertyType = _Map[type].ToArray();
                return true;
            }
            return false;
        }

        public void RegisterProperty(Type type, PropertyInfo info)
        {
            if (!_Map.ContainsKey(type))
            {
                _Map.Add(type, new List<PropertyInfo>());
            }
            _Map[type].Add(info);
        }
    }
}
