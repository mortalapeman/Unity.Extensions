using Microsoft.Practices.ObjectBuilder2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Unity.ExternalContext
{
    public class ExternalContextReflectionStrategy : BuilderStrategy
    {
        public override void PreBuildUp(IBuilderContext context)
        {
            var key = new NamedTypeBuildKey(typeof(IExternalContextObject), "props");
            var policy = context.Policies.Get<IExternalContextPolicy>(key);
            if (policy == null)
            {
                policy = new ExternalContextPolicy();
                context.Policies.Set(policy, key);
            }

            foreach(var prop in context.BuildKey.Type.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public)
                                                     .Where(x => x.CanWrite && typeof(IExternalContextObject).IsAssignableFrom(x.PropertyType)))
            {
                policy.RegisterProperty(context.BuildKey.Type, prop);
            }
        }
    }
}
