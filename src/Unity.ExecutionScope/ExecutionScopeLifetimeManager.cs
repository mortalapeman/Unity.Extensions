using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity.ExecutionScope
{
    public interface IExecutionScopeLifetimePolicy : ILifetimePolicy
    {
        ExecutionScope Scope { get; }
    }

    public class ExecutionScopeLifetimeManager : LifetimeManager, IExecutionScopeLifetimePolicy
    {
        private object _Value;

        public ExecutionScope Scope { get; private set; }

        public ExecutionScopeLifetimeManager(ExecutionScope scope)
        {
            Scope = scope;
        }

        public override object GetValue()
        {
            if (!ExecutionScopeProvider.Instance.IsActive(Scope))
                throw new InvalidOperationException();
            return _Value;
        }

        public override void RemoveValue()
        {
            if (!ExecutionScopeProvider.Instance.IsActive(Scope))
                throw new InvalidOperationException();
            _Value = null;
        }

        public override void SetValue(object newValue)
        {
            if (!ExecutionScopeProvider.Instance.IsActive(Scope))
                throw new InvalidOperationException();
            _Value = newValue;
        }
    }
}
