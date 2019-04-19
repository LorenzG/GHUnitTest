using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Utilities
{
    public struct Check
    {
        readonly string v;
        readonly string key;
        readonly Func<string, bool> check;

        public Check(string key, Func<string, bool> check)
        {
            this.key = key;
            this.check = check;
            this.v = "";
        }

        public Check(string key, Func<string, bool> check, string v)
            : this(key, check)
        {
            this.v = v;
        }

        public string Key => key;

        internal bool Run(Dictionary<string, string> result)
        {

            string value = result[Key];
            bool b = check(value);

            Assert.IsTrue(b, v);

            return b;
        }
    }
}
