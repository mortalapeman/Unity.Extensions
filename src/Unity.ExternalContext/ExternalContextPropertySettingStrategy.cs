using Microsoft.Practices.ObjectBuilder2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Unity.ExternalContext
{
    public class ExternalContextPropertySettingStrategy : BuilderStrategy
    {
        public override void PostBuildUp(IBuilderContext context)
        {
            var contextPolicy = context.Policies.Get<IExternalContextPolicy>(new NamedTypeBuildKey(typeof(IExternalContextObject), "props"));
            if (context.Existing != null && contextPolicy != null)
            {
                PropertyInfo[] properties;
                if (contextPolicy.IsRegistered(context.BuildKey.Type, out properties))
                {
                    foreach(var property in properties)
                    {
                        var obj = context.Existing;
                        var key = new NamedTypeBuildKey(property.PropertyType);
                        var mappedName = context.Policies.Get<IBuildKeyMappingPolicy>(key);
                        if (mappedName == null)
                            mappedName = new BuildKeyMappingPolicy(key);

                        var lifeTimePolicy = context.Policies.Get<ILifetimePolicy>(mappedName.Map(key, context));
                        if (lifeTimePolicy != null)
                        {
                            var result = lifeTimePolicy.GetValue();
                            property.SetValue(obj, result);
                        }
                    }
                }
            }
        }
    }
}
