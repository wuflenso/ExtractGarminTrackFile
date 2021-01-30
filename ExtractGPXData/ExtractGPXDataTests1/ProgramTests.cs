using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExtractGPXData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ExtractGPXData.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void ImportGPXTest()
        {
            string _inputPath = "D:/temp/Semeru_2017.xml";
            XDocument result = ExtractGPXData.Program.ImportGPX(_inputPath);
            Assert.IsNotNull(result, "object is null");
        }
    }
}