using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AssistantAPI.Helpers;

namespace AssistantAPI.Controllers
{
    public class TranscriptController : ApiController
    {
        // POST api/Transcript
        public string Post([FromBody]string jsonString)
        {
            return TranscriptProcessor.ProcessVideoBreakDown(jsonString);

        }
    }
}
