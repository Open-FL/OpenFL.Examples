using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenFL.Editor.Forms;
using OpenFL.Editor.Utils.Plugins;
using OpenFL.Examples;

using PluginSystem.Core.Pointer;
using PluginSystem.Utility;

using ThemeEngine.Forms;

using Utility.IO.Callbacks;
using Utility.IO.VirtualFS;
using Utility.ProgressFeedback;
using Utility.WindowsForms.CustomControls;

namespace OpenFL.Editor.Examples
{
    public class OpenFLExamplesPlugin: APlugin<FLEditorPluginHost>
    {

        public override void OnLoad(PluginAssemblyPointer ptr)
        {
            base.OnLoad(ptr);   

            ManifestReader.RegisterAssembly(typeof(ExampleInformation).Assembly);   
        }

        [ToolbarItem("Examples")]
        private void Examples()
        {

        }

        [ToolbarItem("Examples/Unpack Examples")]
        private void UnpackExamples()
        {
            ProgressIndicator.RunTask(UnpackResources, Application.DoEvents);
            if (StyledMessageBox.Show(
                                      "Unpacking Finished.",
                                      "Do you want to load view the Unpacked Files?",
                                      MessageBoxButtons.YesNo, SystemIcons.Question
                                     ) ==
                DialogResult.Yes)
            {
                Process.Start(".\\examples\\");
            }
        }

        private void UnpackResources(IProgressIndicator indicator)
        {
            string workingDir = FLScriptEditor.Settings.WorkingDir ?? Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(Application.StartupPath);
            string[] files = IOManager.GetFiles("examples");

            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                indicator.SetProgress("Unpacking file: " + file, i, files.Length - 1);
                string dir = Path.GetDirectoryName(file);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                if (!File.Exists(file))
                {
                    Stream s = IOManager.GetStream(file);
                    Stream dst = File.Create(file);
                    s.CopyTo(dst);
                    s.Dispose();
                    dst.Dispose();
                }
            }

            Directory.SetCurrentDirectory(workingDir);
        }
        


    }
}
