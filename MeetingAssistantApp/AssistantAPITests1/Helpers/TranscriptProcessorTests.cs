using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssistantAPI.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistantAPI.Helpers.Tests
{
    [TestClass()]
    public class TranscriptProcessorTests
    {
        [TestMethod()]
        [DeploymentItem("test.json")]
        public void ProcessVideoBreakDownTest()
        {
            string file = "test.json";
            Assert.IsTrue(File.Exists(file), "deployment failed: " + file +
               " did not get deployed");
            string json = File.ReadAllText(file);
            var res = TranscriptProcessor.ProcessVideoBreakDown(json);
            Assert.IsNotNull(res);
        }

       
    }
}

namespace AssistantAPITests1.Helpers
{
    class TranscriptProcessorTests
    {

    }
}
