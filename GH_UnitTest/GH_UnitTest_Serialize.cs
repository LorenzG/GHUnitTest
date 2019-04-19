using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Grasshopper.Kernel;
using Newtonsoft.Json;
using Rhino.Geometry;

namespace GH_UnitTest
{
    public class GH_UnitTest_Serialize : GH_Component
    {
        public GH_UnitTest_Serialize()
          : base("Serialize", "Serialize",
              "Serialize",
              "GH_UnitTest", "GH_UnitTest")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Variable Names", "N", "Names of the variables to check", GH_ParamAccess.list);
            pManager.AddTextParameter("Variable Values", "V", "Values of the variables to check", GH_ParamAccess.list);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<string> variables = new List<string>();
            List<string> values = new List<string>();
            if (!DA.GetDataList(0, variables))
                return;
            if (!DA.GetDataList(1, values))
                return;

            if (variables.Count != values.Count)
                throw new ArgumentException("Variables and Values should have the same length");

            Dictionary<string, string> dict = variables.Zip(values, (vr, vs) => new { vr, vs }).ToDictionary(a => a.vr, a => a.vs);

            string json = JsonConvert.SerializeObject(dict);

            string filePath = OnPingDocument().FilePath;
            string name = Path.GetFileNameWithoutExtension(filePath);
            string path = Path.GetDirectoryName(filePath);
            string jsonFilePath = string.Format("{0}//{1}.json", path, name);

            if (File.Exists(jsonFilePath))
                File.Delete(jsonFilePath);

            File.WriteAllText(jsonFilePath, json);
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("fe982ed2-9f1a-4a9a-be55-37f7a0145c31"); }
        }
    }
}
