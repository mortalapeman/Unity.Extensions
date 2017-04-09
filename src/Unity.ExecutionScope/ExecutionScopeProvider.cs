using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity.ExecutionScope
{
    public class ExecutionScopeProvider
    {
        private Dictionary<Guid, ExecutionScope> _Scopes;

        public IEnumerable<ExecutionScope> Scopes => _Scopes.Values;

        public ExecutionScopeProvider()
        {
            _Scopes = new Dictionary<Guid, ExecutionScope>();
        }
        public ExecutionScope Begin()
        {
            var scope = new ExecutionScope(this);
            _Scopes.Add(scope.Key, scope);
            return scope;
        }

        public bool IsActive(ExecutionScope scope)
        {
            return _Scopes.ContainsKey(scope.Key);
        }

        public void End(ExecutionScope scope)
        {
            _Scopes.Remove(scope.Key);
        }

        public static ExecutionScopeProvider Instance = new ExecutionScopeProvider();
    }
}
