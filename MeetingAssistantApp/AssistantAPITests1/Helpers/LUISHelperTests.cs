using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssistantAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistantAPI.Helpers.Tests
{
    [TestClass()]
    public class LUISHelperTests
    {
        [TestMethod()]
        public void UnderstandCommandTest()
        {
            var commandString = "Send email to Daniela";
            var jsonString = LUISHelper.UnderstandCommand(commandString);
            Assert.IsNotNull(jsonString);

        }

        [TestMethod()]
        public void ExecuteCommandTest()
        {
            var commandString = "Marian, please finish the business analysis today";
            var res= LUISHelper.ExecuteCommand(commandString);
            Assert.IsTrue(res);
        }
    }
}