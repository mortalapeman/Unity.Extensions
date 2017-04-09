using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity.ExecutionScope
{
    public class ExecutionScopeUnityContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Context.Registering += Context_Registering;
            Context.RegisteringInstance += Context_RegisteringInstance;
        }

        private void Context_RegisteringInstance(object sender, RegisterInstanceEventArgs e)
        {
            if (e.LifetimeManager is ExecutionScopeLifetimeManager)
            {
                var lm = e.LifetimeManager as ExecutionScopeLifetimeManager;
                var key = new NamedTypeBuildKey(e.RegisteredType, e.Name);
                var newKey = new NamedTypeBuildKey(e.RegisteredType, $"{e.Name}_Scope_{lm.Scope.Key}");
                lm.SetValue(e.Instance);
                Context.Policies.Clear<ILifetimePolicy>(key);
                Context.Policies.Set<ILifetimePolicy>(lm, newKey);
                Context.Policies.Set<IExecutionScopeLifetimePolicy>(lm, newKey);
                Context.Policies.Set<IBuildKeyMappingPolicy>(new BuildKeyMappingPolicy(newKey), key);
                lm.Scope.Disposed += (o, de) =>
                {
                    Context.Policies.Clear<ILifetimePolicy>(newKey);
                    Context.Policies.Clear<IBuildKeyMappingPolicy>(key);
                };
            }
        }

        private void Context_Registering(object sender, RegisterEventArgs e)
        {
            if (e.LifetimeManager is ExecutionScopeLifetimeManager)
            {
                var lm = e.LifetimeManager as ExecutionScopeLifetimeManager;
                var newKey = new NamedTypeBuildKey(e.TypeTo, $"{e.Name}_Scope_{lm.Scope.Key}");
                var key = new NamedTypeBuildKey(e.TypeTo, e.Name);
                Context.Policies.Clear<ILifetimePolicy>(key);
                Context.Policies.Set<ILifetimePolicy>(lm, newKey);
                Context.Policies.Set<IExecutionScopeLifetimePolicy>(lm, newKey);
                Context.Policies.Set<IBuildKeyMappingPolicy>(new BuildKeyMappingPolicy(newKey), key);
                lm.Scope.Disposed += (o, de) =>
                {
                    Context.Policies.Clear<ILifetimePolicy>(newKey);
                    Context.Policies.Clear<IBuildKeyMappingPolicy>(key);
                };
            }
        }
    }
}
