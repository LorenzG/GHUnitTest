using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTest.Utilities
{
    public class CheckGHFile
    {
        static readonly string temp = Path.GetTempPath();
        readonly Check[] checks;
        readonly string fullPathGH;
        readonly string fullPathRES;

        public CheckGHFile(string fullPath, params Check[] checks)
        {
            this.fullPathGH = fullPath + ".gh";
            this.fullPathRES = fullPath + ".json";
            this.checks = checks;

            Trace.WriteLine("gh file path: " + fullPathGH);
            Assert.IsTrue(File.Exists(fullPathGH), "File does not exist");
        }

        public void RunChecks(bool runGH = true)
        {
            if (runGH)
            {
                if (File.Exists(fullPathRES))
                    File.Delete(fullPathRES);


                //Process.Start(fullPathGH);
            }

            Process rhinoProcess = null;
            try
            {
                rhinoProcess = RunRhino(ghFile: fullPathGH, waitForExit: false);

                Dictionary<string, string> result = GetResults(fullPathRES);

                Assert.IsNotNull(result, "Dictionary is null");

                for (int i = 0; i < checks.Length; i++)
                {
                    Check c = checks[i];
                    Trace.WriteLine("Running " + c.Key);

                    if (checks[i].Run(result))
                        Trace.WriteLine(c.Key + " passed");
                }

            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
            finally
            {
                rhinoProcess?.Kill();
            }
        }

        internal static Process RunRhino(bool guardian = true, string ghFile = "", string rhinoFile = "", string script = "", bool waitForExit = true, bool rhino6 = true)
        {
            //string args = "/nosplash /runscript = \"-grasshopper editor load document open " + ghFile + " _enter\" \"" + rhinoFile + "\"";
            string args = "/nosplash /runscript=\"";

            if (!string.IsNullOrEmpty(rhinoFile))
            {
                // if another file is open and not saved: args += "-Open N " + "" + rhinoFile + " _enter";
                args += "-Open " + rhinoFile + " _enter";

                if (!string.IsNullOrEmpty(ghFile))
                    args += " ";
            }

            if (!string.IsNullOrEmpty(ghFile))
            {                
                if(rhino6)
                    args += "_GrasshopperOpen " + ghFile + " _enter";
                else
                    args += "-grasshopper document open " + ghFile + " _enter";

                if (!string.IsNullOrEmpty(script))
                    args += " ";
            }

            if (!string.IsNullOrEmpty(script))
            {
                args += script + " _enter";
            }



            args += "\"";

            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = rhino6 ? @"C:\Program Files\Rhino 6\System\Rhino.exe" : @"C:\Program Files (x86)\Rhinoceros 5\System\Rhino4.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = args;


            Process exeProcess = null;
            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                exeProcess = Process.Start(startInfo);
                {
                    if (waitForExit)
                        exeProcess.WaitForExit();

                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            return exeProcess;
        }
        
        static Dictionary<string, string> GetResults(string fileName)
        {
            string fullPath = Path.Combine(temp, fileName);

            Dictionary<string, string> result;

            while (true)
            {
                if (!File.Exists(fullPath))
                {
                    Thread.Sleep(1000);
                    continue;
                }

                string json = File.ReadAllText(fullPath);

                result = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                break;
            }

            return result;
        }
    }
}
