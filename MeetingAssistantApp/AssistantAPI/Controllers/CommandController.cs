using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AssistantAPI.Helpers;

namespace AssistantAPI.Controllers
{
    public class CommandController : ApiController
    {
        // POST api/Command
        public string Post([FromBody]string jsonString)
        {
          //
            var commands =jsonString.ToString();
            var cmdArray = commands.Split('\n');
            foreach (var cmd in cmdArray)
            {
               //var ret = LUISHelper.UnderstandCommand(cmd);
                var res= LUISHelper.ExecuteCommand(cmd);

            }
            return string.Empty;

        }
    }
}
