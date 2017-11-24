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
    public class CommandsHelperTests
    {
        [TestMethod()]
        public void ScheduleEventTest()
        {
            CommandsHelper.ScheduleEvent("new meeting", DateTime.Now, "DONAU");

        }

        [TestMethod()]
        public void AddTaskTest()
        {
            CommandsHelper.AddTask("Daniela", DateTime.Now, "do something");
        }
    }
}