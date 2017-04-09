using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity.ExternalContext
{
    public class ExternalContextUnityContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Context.Strategies.Add(new ExternalContextReflectionStrategy(), UnityBuildStage.PreCreation);
            Context.Strategies.Add(new ExternalContextPropertySettingStrategy(), UnityBuildStage.Initialization);
        }
    }
}
