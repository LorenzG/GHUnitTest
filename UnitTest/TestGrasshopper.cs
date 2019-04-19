using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using UnitTest.Utilities;

namespace UnitTest
{
    [TestClass]
    public class TestGrasshopper
    {
        const string dp = @"C:\dev\GHUnitTest\GH_UnitTest\DebuggingDefinitions";

        [TestMethod]
        public void Sum()
        {
            string fileName = "sum";
            string fullPath = dp + "\\" + fileName;

            Func<string, bool> check = (value) => double.Parse(value) == 3;

            Check c0 = new Check("Sum", check, "Doesn't add up");

            CheckGHFile checkGH = new CheckGHFile(fullPath, c0);

            checkGH.RunChecks();
        }


    }
}
