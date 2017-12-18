using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AssistantAPI.Helpers;

namespace AssistantAPI.Controllers
{
    public class NotifyController : ApiController
    {
        // POST api/values
        public void Post([FromUri]string id, [FromUri] string state)
        {
            // I get the notification that a video is indexed
            //get the video breakdown
            if (state.Equals("Processed", StringComparison.CurrentCultureIgnoreCase))
            {
                var videoBreakdownJson =
                    VideoHelper.GetVideoBreakdown(id);
                if (!String.IsNullOrEmpty(videoBreakdownJson))
                {
                    var transcript = TranscriptProcessor.ProcessVideoBreakDown(videoBreakdownJson);
                    // approve transcript
                    // send email to all participants
                    var commands = TranscriptProcessor.GetCommands(videoBreakdownJson);
                    foreach (var command in commands)
                    {
                        //send transcript to LUIS
                        //var jsonCmd = LUISHelper.UnderstandCommand(command);
                        var succeded = LUISHelper.ExecuteCommand(command);
                    }
                    
                }
            }
        }

    }
}
