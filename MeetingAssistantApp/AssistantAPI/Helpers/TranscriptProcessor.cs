using AssistantAPI.Classes;
using GemBox.Document;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssistantAPI.Helpers
{
    public static class TranscriptProcessor
    {
        /// <summary>
        /// Processes the video brakdown as json and returns a meeting protocol as string
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static string ProcessVideoBreakDown(string jsonString)
        {
            
            StringBuilder res= new StringBuilder();
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);
            string username = obj.userName;
            var br = obj.breakdowns;
            foreach (var bd in br)
            {
                var participants =  bd.insights.participants;
                
                    Dictionary<string, string> partList = new Dictionary<string, string>();
                    foreach (var participant in participants)
                    {
                        partList.Add(participant.id.ToString()
                            , participant.name.ToString());
                    }
                var parts = partList.ToArray();
                var tbs = bd.insights.transcriptBlocks;
                foreach (var tb in tbs)
                {
                    var lines = tb.lines;
                    foreach (var line in lines)
                    {
                        try
                        {
                            var part = parts[line.participantId];
                            var str = string.Format("{0} : {1} ", part, line.text);
                            res.AppendLine(str);
                        }
                        catch (Exception)
                        {

                            //ignore
                        }
                    }
                }



            }
            
           
           

            return res.ToString();
        }


        public static List<string> GetCommands(string jsonString)
        {
            throw new NotImplementedException();
        }
    }
}