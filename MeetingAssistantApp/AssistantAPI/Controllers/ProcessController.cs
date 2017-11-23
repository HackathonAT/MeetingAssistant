using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AssistantAPI.Helpers;
using AssistantAPI.Models;

namespace AssistantAPI.Controllers
{
    public class ProcessController : ApiController
    {
        // POST api/Process
        public string Post([FromBody]object jsonString)
        {
            return TranscriptProcessor.ProcessVideoBreakDown(jsonString.ToString());

        }
    }
}
