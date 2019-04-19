using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace GH_UnitTest
{
    public class GH_UnitTestInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "GHUnitTest";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("65552c67-aba7-4bc8-b2f2-dca28223fccf");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
