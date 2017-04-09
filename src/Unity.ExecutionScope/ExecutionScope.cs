using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity.ExecutionScope
{
    public class ExecutionScope : IDisposable
    {
        private ExecutionScopeProvider _provider;

        internal event EventHandler Disposed;

        public ExecutionScope(ExecutionScopeProvider provider)
        {
            Key = Guid.NewGuid();
            _provider = provider;
            Disposed += delegate { };
        }

        public Guid Key { get; set; }


        public void Dispose()
        {
            _provider.End(this);
            Disposed(this, new EventArgs());
        }
    }
}
