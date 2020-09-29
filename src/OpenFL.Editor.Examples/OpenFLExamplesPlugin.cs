using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using OpenFL.Examples;

using PluginSystem.Core.Pointer;
using PluginSystem.Utility;

using Utility.IO.VirtualFS;

namespace OpenFL.Editor.Examples
{
    public class OpenFLExamplesPlugin: APlugin<FLEditorPluginHost>
    {

        public override void OnLoad(PluginAssemblyPointer ptr)
        {
            base.OnLoad(ptr);   

            ManifestReader.RegisterAssembly(typeof(ExampleInformation).Assembly);   
        }

    }
}
