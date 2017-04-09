using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity.ExecutionScope
{
    public static class UnityContainerExtensions
    {
        public static ExecutionScope BeginScope(this IUnityContainer container)
        {
            return ExecutionScopeProvider.Instance.Begin();
        }
    }
}
